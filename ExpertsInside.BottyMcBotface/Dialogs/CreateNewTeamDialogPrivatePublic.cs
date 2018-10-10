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
    public class CreateNewTeamDialogPrivatePublic : IDialog<object>
    {
        private IEnumerable<string> Options = null;

        public CreateNewTeamDialogPrivatePublic()
        {
            Options = new string[] { "Private", "Public" };
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
                Strings.PrivateOrPublicQuestion);
        }

        private async Task ChooseOptions(IDialogContext context, IAwaitable<string> result)
        {
            var selctedChoice = await result;

            context.UserData.SetValue("PrivateOrPublic", selctedChoice);

            context.Call(new CreateNewTeamDialogNameOfTheTeam(), ResumeAfterCreateNewTeamPrivatePublic);
        }

        private async Task ResumeAfterCreateNewTeamPrivatePublic(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(null);
        }
    }
}