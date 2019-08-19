using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace XI.BottyMcBotface.Dialogs
{
    public class MainDialog : ComponentDialog
    {
        protected readonly ILogger Logger;

        public MainDialog(NewTeamDialog newTeamDialog, ILogger<MainDialog> logger)
            : base(nameof(MainDialog))
        {
            Logger = logger;

            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(newTeamDialog);
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                ActStepAsync,
                FinalStepAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> ActStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.BeginDialogAsync(nameof(NewTeamDialog), new NewTeamDetails(), cancellationToken);
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Result is NewTeamDetails result)
            {
                var messageText = $"I have created a team called {result.TeamName} for the owner {result.Owner}. \r It appears on Teams in a few seconds.";
                var message = MessageFactory.Text(messageText, messageText, InputHints.IgnoringInput);
                await stepContext.Context.SendActivityAsync(message, cancellationToken);
            }

            // Restart the main dialog with a different message the second time around
            var promptMessage = "Well done. Do you want create another new team?";
            return await stepContext.ReplaceDialogAsync(InitialDialogId, promptMessage, cancellationToken);
        }
    }
}
