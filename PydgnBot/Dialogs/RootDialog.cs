using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace PydgnBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private string uniqueID = "";
        private int taskID = 0;
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            switch (taskID)
            {
                case 0:
                    await context.PostAsync("Welcome to Pydgn! We see this is your first time here. Please choose the username you'd like people to contact you at.");
                    taskID = 1;
                    break;
                case 1:
                    if (activity.Text.Contains("@") || activity.Text.Contains(" ") || activity.Text.Contains("pydgn"))
                    {
                        await context.PostAsync($"Your username is Invalid. Please enter one without spaces or the @ symbol");                 
                    }
                    else
                    {
                        await context.PostAsync($"Your username is @{activity.Text} \n To send a message, it needs to be formatted as \"@Username message goes here...\" ");
                        taskID = 2;
                    }
                    break;
                case 2:
                    await context.PostAsync("To send a message, it needs to be formatted as \"@Username message goes here...\" ");
                    break;
                default:
                    await context.PostAsync("Shoot, it broke");
                    break;
            }
            context.Wait(MessageReceivedAsync);
        }
    }
}