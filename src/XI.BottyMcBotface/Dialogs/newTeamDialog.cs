using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Recognizers.Text.DataTypes.TimexExpression;
using XI.BottyMcBotface.Services;

namespace XI.BottyMcBotface.Dialogs
{
    public class NewTeamDialog : CancelAndHelpDialog
    {
        private const string TeamNameStepMsgText = "What's the name of the new Team?";
        private const string OwnerEmailStepMsgText = "Who is the owner of the team? (please insert the email address)";
        private const string AskMembersStepMsgText = "Do you want to add some members now?";
        private const string MembersEmailStepMsgText = "Who are the members of the team? Write 'anyone' if you want add them later (please insert the email addresses separated by comma)";
        private const string PrivateStepMsgText = "The team is private?";

        public IDataService _dataService;

        public NewTeamDialog(IDataService dataService)
            : base(nameof(NewTeamDialog))
        {
            _dataService = dataService;

            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                NameNewTeamStepAsync,
                OwnerStepAsync,
                AskMembersStepAsync,
                MembersStepAsync,
                PrivateStepAsync,
                ConfirmStepAsync,
                FinalStepAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> NameNewTeamStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var newTeamDetails = (NewTeamDetails)stepContext.Options;

            if (newTeamDetails.TeamName == null)
            {
                var promptMessage = MessageFactory.Text(TeamNameStepMsgText, TeamNameStepMsgText, InputHints.ExpectingInput);
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
            }

            return await stepContext.NextAsync(newTeamDetails.TeamName, cancellationToken);
        }

        private async Task<DialogTurnResult> OwnerStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var newTeamDetails = (NewTeamDetails)stepContext.Options;

            newTeamDetails.TeamName = (string)stepContext.Result;

            if (newTeamDetails.Owner == null)
            {
                var promptMessage = MessageFactory.Text(OwnerEmailStepMsgText, OwnerEmailStepMsgText, InputHints.ExpectingInput);
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
            }

            return await stepContext.NextAsync(newTeamDetails.Owner, cancellationToken);
        }

        private async Task<DialogTurnResult> AskMembersStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var newTeamDetails = (NewTeamDetails)stepContext.Options;

            newTeamDetails.Owner = (string)stepContext.Result;

            var promptMessage = MessageFactory.Text(AskMembersStepMsgText, AskMembersStepMsgText, InputHints.ExpectingInput);
            return await stepContext.PromptAsync(nameof(ConfirmPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);

        }

        private async Task<DialogTurnResult> MembersStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var newTeamDetails = (NewTeamDetails)stepContext.Options;

            newTeamDetails.MemberAnswer = (bool)stepContext.Result;

            if (newTeamDetails.MemberAnswer)
            {
                var promptMessage = MessageFactory.Text(MembersEmailStepMsgText, MembersEmailStepMsgText, InputHints.ExpectingInput);
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
            }

            newTeamDetails.Members = string.Empty;

            return await stepContext.NextAsync(newTeamDetails.Members, cancellationToken);
        }

        private async Task<DialogTurnResult> PrivateStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var newTeamDetails = (NewTeamDetails)stepContext.Options;

            newTeamDetails.Members = (string)stepContext.Result;

            var promptMessage = MessageFactory.Text(PrivateStepMsgText, PrivateStepMsgText, InputHints.ExpectingInput);
            return await stepContext.PromptAsync(nameof(ConfirmPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
        }

        private async Task<DialogTurnResult> ConfirmStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var newTeamDetails = (NewTeamDetails)stepContext.Options;

            newTeamDetails.Private = (bool)stepContext.Result;

            var messageText = $"Please confirm, I will create a team called {newTeamDetails.TeamName} with the ownership of {newTeamDetails.Owner}. Is this correct?";
            var promptMessage = MessageFactory.Text(messageText, messageText, InputHints.ExpectingInput);

            return await stepContext.PromptAsync(nameof(ConfirmPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((bool)stepContext.Result)
            {
                var newTeamDetails = (NewTeamDetails)stepContext.Options;

                await _dataService.CreateTeam(newTeamDetails.TeamName, newTeamDetails.Owner, newTeamDetails.Members, newTeamDetails.Private);

                return await stepContext.EndDialogAsync(newTeamDetails, cancellationToken);
            }

            return await stepContext.EndDialogAsync(null, cancellationToken);
        }
    }
}
