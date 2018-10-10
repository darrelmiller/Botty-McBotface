using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ExpertsInside.BottyMcBotface.Dialogs
{
    [Serializable]
    public class CreateNewTeamDialogOwner : IDialog<object>
    {
        private IEnumerable<string> Options = null;

        public CreateNewTeamDialogOwner()
        {
            Options = new string[] { "Yes", "No" };
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
                "Are you the owner of the team?");
        }

        private async Task ChooseOptions(IDialogContext context, IAwaitable<string> result)
        {
            var selectedChoice = await result;

            context.UserData.SetValue("Owner", selectedChoice);

            if (selectedChoice.ToLower() == "yes")
            {
                var connector = new ConnectorClient(new Uri(context.Activity.ServiceUrl));
                var members = await connector.Conversations.GetConversationMembersAsync(context.Activity.Conversation.Id);

                foreach (var member in members.AsTeamsChannelAccounts())
                {
                    context.UserData.SetValue("Owner", member.Email);
                }

                context.Call(new CreateNewTeamDialogInsertMembers(), ResumeAfterCreateNewTeamDialogOwner);
            }
            else if (selectedChoice.ToLower() == "no")
            {
                context.Call(new CreateNewTeamDialogInsertOwner(), ResumeAfterCreateNewTeamDialogOwner);
            }
        }

        private async Task ResumeAfterCreateNewTeamDialogOwner(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(null);
        }
    }
}