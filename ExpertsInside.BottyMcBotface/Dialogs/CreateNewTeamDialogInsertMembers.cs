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
    public class CreateNewTeamDialogInsertMembers : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            PromptDialog.Text(
                context: context,
                resume: ResumeGetMembers,
                prompt: "Insert the members, separated by comma",
                retry: "Insert valids members"
            );
        }

        public virtual async Task ResumeGetMembers(IDialogContext context, IAwaitable<string> result)
        {
            string response = await result;

            context.UserData.SetValue("Members", response);

            context.Call(new CreateNewTeamDialogReview(), ResumeAfterCreateNewTeamDialogInsertMembers);
        }

        private async Task ResumeAfterCreateNewTeamDialogInsertMembers(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(null);
        }
    }
}