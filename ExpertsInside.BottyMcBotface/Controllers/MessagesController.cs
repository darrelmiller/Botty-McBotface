using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;

namespace ExpertsInside.BottyMcBotface
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity, CancellationToken cancellationToken)
        {
            var connectorClient = new ConnectorClient(new Uri(activity.ServiceUrl));

            if (activity.GetActivityType() == ActivityTypes.Message)
            {
                // Special handling for a command to simulate a reset of the bot chat
                if (!(activity.Conversation.IsGroup ?? false) && (activity.Text == "/restart"))
                {
                    return await HandleResetBotChatAsync(activity, cancellationToken);
                }

                //Set the Locale for Bot
                activity.Locale = TemplateUtility.GetLocale(activity);

                //Convert incoming activity text to lower case, to match the intent irrespective of incoming text case
                activity = TemplateUtility.ConvertActivityTextToLower(activity);

                await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
            }
            else
            {
                await HandleSystemMessageAsync(activity, connectorClient, cancellationToken);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private async Task HandleSystemMessageAsync(Activity message, ConnectorClient connectorClient, CancellationToken cancellationToken)
        {
            string messageType = message.GetActivityType();
            if (messageType == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (messageType == ActivityTypes.ConversationUpdate)
            {
                Activity welcomeMessage = message.CreateReply();

                welcomeMessage.Attachments = new List<Attachment>
                {
                    new HeroCard("Hello!")
                    {
                        Text = "Hello, I am Botty McBotFace, the bot assistant from Experts Inside Planet. How can I help you?",
                        Images = new List<CardImage>
                        {
                            new CardImage { Url = "https://cdn.dribbble.com/users/37530/screenshots/2937858/drib_blink_bot.gif"}
                        },
                        Buttons = new List<CardAction>
                        {
                            new CardAction(ActionTypes.ImBack, "New Team", value: DialogMessages.CreateNewTeam),
                            new CardAction(ActionTypes.ImBack, "Search a document on SharePoint", value: "cmdSearch"),
                            new CardAction(ActionTypes.ImBack, "New Support Ticket", value: "cmdSupport"),
                            new CardAction(ActionTypes.ImBack, "Read something about our products", value: "cmdReadProducts"),
                            new CardAction(ActionTypes.ImBack, "Contact an Expert Guy", value: "cmdContact"),
                            new CardAction(ActionTypes.ImBack, "Talk with one of us", value: "cmdTalk"),
                        }
                    }.ToAttachment()
                };

                await connectorClient.Conversations.ReplyToActivityAsync(welcomeMessage, cancellationToken);
            }
            else if (messageType == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (messageType == ActivityTypes.Typing)
            {
                // Handle knowing that the user is typing
            }
            else if (messageType == ActivityTypes.Ping)
            {
            }
        }

        private async Task<HttpResponseMessage> HandleResetBotChatAsync(Activity message, CancellationToken cancellationToken)
        {
            // Forget everything we know about the user
            using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, message))
            {
                var address = Address.FromActivity(message);
                var botDataStore = scope.Resolve<IBotDataStore<BotData>>();
                await botDataStore.SaveAsync(address, BotStoreType.BotUserData, new BotData("*"), cancellationToken);
                await botDataStore.SaveAsync(address, BotStoreType.BotConversationData, new BotData("*"), cancellationToken);
                await botDataStore.SaveAsync(address, BotStoreType.BotPrivateConversationData, new BotData("*"), cancellationToken);
            }

            // If you need to reset the user state in other services your app uses, do it here.

            // Synthesize a conversation update event and simulate the bot receiving it
            // Note that this is a fake event, as Teams does not support deleting a 1:1 conversation and re-creating it
            var conversationUpdateMessage = new Activity
            {
                Type = ActivityTypes.ConversationUpdate,
                Id = message.Id,
                ServiceUrl = message.ServiceUrl,
                From = message.From,
                Recipient = message.Recipient,
                Conversation = message.Conversation,
                ChannelData = message.ChannelData,
                ChannelId = message.ChannelId,
                Timestamp = message.Timestamp,
                MembersAdded = new List<ChannelAccount> { message.From, message.Recipient },
            };
            return await this.Post(conversationUpdateMessage, cancellationToken);
        }
    }
}