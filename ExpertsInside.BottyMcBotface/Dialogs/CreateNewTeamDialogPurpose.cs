using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ExpertsInside.BottyMcBotface.Dialogs
{
    [Serializable]
    public class CreateNewTeamDialogPurpose : IDialog<object>
    {
        private IEnumerable<string> Options = null;

        public CreateNewTeamDialogPurpose()
        {
            Options = new string[] { "Marketing", "Project" , "Generic" };
        }

        public async Task StartAsync(IDialogContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            PromptDialog.Choice<string>(
                context,
                this.ChooseOptions,
                Options,
                "What's the purpose of the team?");
        }

        private async Task ChooseOptions(IDialogContext context, IAwaitable<string> result)
        {
            var selctedChoice = await result;

            context.UserData.SetValue("Purpose", selctedChoice);

            context.Call(new CreateNewTeamDialogPrivatePublic(), ResumeAfterCreateNewTeamDialogPurpose);
        }

        private async Task ResumeAfterCreateNewTeamDialogPurpose(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(null);
        }
    }
}