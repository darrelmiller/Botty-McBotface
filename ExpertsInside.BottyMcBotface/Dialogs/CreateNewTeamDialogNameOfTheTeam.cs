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
    public class CreateNewTeamDialogNameOfTheTeam : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            PromptDialog.Text(
                context: context,
                resume: ResumeGetName,
                prompt: Strings.InsertTheNameOfTheTeam,
                retry: Strings.RetryInsertTheNameOfTheTeam
            );
        }

        public virtual async Task ResumeGetName(IDialogContext context, IAwaitable<string> result)
        {
            string response = await result;

            context.UserData.SetValue("NameOfTheTeam", response);

            context.Call(new CreateNewTeamDialogOwner(), ResumeAfterCreateNewTeamPrivatePublic);
        }

        private async Task ResumeAfterCreateNewTeamPrivatePublic(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(null);
        }
    }
}