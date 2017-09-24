using Microsoft.Bot.Builder.Dialogs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PydgnBot.Dialogs
{
    [Serializable]
    public class ConversationDialog: IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            PromptDialog.Text(
                context,
                this.resume,
                "Enter a username and message to send");
        }

        private async Task resume(IDialogContext context, IAwaitable<string> result)
        {
            var Message = await result;

            JObject resp = await Program.SendMessage(Message, context.Activity.Conversation.Id, "Craig");

            var fromUser 

            // context.Activity.Conversation.Id
            if (resp.HasValues == false)
            {
                await context.PostAsync("Sorry, there's a problem with the message. Please try again.");
                StartAsync(context);
            }
            else
            {
                await context.PostAsync("Message sent");
                context.Done(context);
                // backend will send the message to that username
            }

            context.Done(context);
            throw new NotImplementedException();
        }
    }
}
