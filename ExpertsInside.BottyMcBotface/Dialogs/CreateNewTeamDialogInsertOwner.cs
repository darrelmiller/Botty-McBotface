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
    public class CreateNewTeamDialogInsertOwner : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            PromptDialog.Text(
                context: context,
                resume: ResumeGetOwner,
                prompt: "Insert the mail of the owner",
                retry: "Insert a valid mail for the owner"
            );
        }

        public virtual async Task ResumeGetOwner(IDialogContext context, IAwaitable<string> result)
        {
            string response = await result;

            context.UserData.SetValue("Owner", response);

            context.Call(new CreateNewTeamDialogInsertMembers(), ResumeAfterCreateNewTeamPrivatePublic);
        }

        private async Task ResumeAfterCreateNewTeamPrivatePublic(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(null);
        }
    }
}