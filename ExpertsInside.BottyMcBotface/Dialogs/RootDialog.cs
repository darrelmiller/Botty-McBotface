using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Scorables;
using Microsoft.Bot.Connector;

namespace ExpertsInside.BottyMcBotface.Dialogs
{
    [Serializable]
    public class RootDialog : DispatchDialog
    {
        public Task EndDialog(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(null);
            return Task.CompletedTask;
        }

        [RegexPattern(DialogMessages.CreateNewTeam)]
        [ScorableGroup(1)]
        public void CreateNewTeam(IDialogContext context, IActivity activity)
        {
            context.Call(new CreateNewTeamDialogPurpose(), this.EndDialog);
        }
    }
}