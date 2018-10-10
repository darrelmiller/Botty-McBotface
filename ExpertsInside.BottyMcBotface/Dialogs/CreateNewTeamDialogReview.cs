using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ExpertsInside.BottyMcBotface.Dialogs
{
    [Serializable]
    public class CreateNewTeamDialogReview : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(new { Purpose = context.UserData.GetValue<string>("Purpose"),  DisplayName = context.UserData.GetValue<string>("NameOfTheTeam"), InternalName = context.UserData.GetValue<string>("NameOfTheTeam").Replace(" ", ""), Owner = context.UserData.GetValue<string>("Owner"), Members = context.UserData.GetValue<string>("Members") }), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://s2events.azure-automation.net/webhooks?token=Igck5fKJzSe9nwB2yFpR6TkSBGpJD3x984UGXQ0nfWs%3d", content);
                if (response.IsSuccessStatusCode)
                {
                }
            }

            var message = context.MakeMessage();
            var attachment = GetThumbnailCard(context.UserData.GetValue<string>("NameOfTheTeam"));

            message.Attachments.Add(attachment);

            await context.PostAsync(message);

            context.Done<object>(null);
        }

        private static Attachment GetThumbnailCard(string teamDisplayName)
        {
            var thumbnailCard = new ThumbnailCard
            {
                Title = "Team Created",
                Subtitle = "I just created a new team for you",
                Text = $"I created a team called {teamDisplayName}. It's available in a few seconds on your Team client.",
                Images = new List<CardImage> { new CardImage("https://cdn.dribbble.com/users/37530/screenshots/2937858/drib_blink_bot.gif") },
            };

            return thumbnailCard.ToAttachment();
        }
    }
}