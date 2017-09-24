using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PydgnBot.Dialogs
{
    [Serializable]
    public class EnrollDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            PromptDialog.Text(
                context,
                this.resume,
                "Please enter a new user name");
        }

        private async Task resume(IDialogContext context, IAwaitable<string> result)
        {
            var userName = await result;

            if (userName.Contains("@") || userName.Contains(" ") || userName.ToUpper().Equals("PYDGN"))
            {
                await context.PostAsync("Your username is Invalid.Please enter one without spaces or the @ symbol");
                StartAsync(context);
            }
            else if (await Program.MakeUser(userName, context.Activity.Conversation.Id, "messenger") == false)
                {
                //username is already taken
                await context.PostAsync("The username is already taken. Please enter a different username.");
                StartAsync(context);
            }
            //
            // context.Activity.Conversation.Id

            // check with the backend if unique
            else {        
                context.PostAsync($"Cool! Your new username is '@{userName}'. When you send a message, format it with  \"@username message...\""); 
            }
            context.Done(context);
        }
    }
}
