using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace PydgnBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        //private string uniqueID = "";
        //private int taskID = 0;

        private string EnrollOption = "Enroll new user";
        private string ChatOption = "Message a user";

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            PromptDialog.Choice(
                context,
                this.onMenuSelect,
                new List<String> { EnrollOption, ChatOption },
                "Select an option to start");

            //switch (taskID)
            //{
            //    //first time user prompt
            //    case 0: 
            //        await context.PostAsync("Welcome to Pydgn! We see this is your first time here. Please choose the username you'd like people to contact you at.");
            //        taskID = 1;
            //        break;
            //    //this is where the user creates their username
            //    case 1:
            //        if (activity.Text.Contains("@") || activity.Text.Contains(" "))
            //        {
            //            await context.PostAsync($"Your username is Invalid. Please enter one without spaces or the @ symbol");                 
            //        }
            //        else if (activity.Text.Contains("pydgn")) //|| dataBase.contains(activity.Text)) //this checks to see if the username is already taken
            //        {
            //            await context.PostAsync("The username you entered is already taken. Please enter a different username");
            //        }
            //        else
            //        {
            //            await context.PostAsync($"Your username is @{activity.Text} \n To send a message, it needs to be formatted as \"@Username message goes here...\" ");
            //            //this is the username stored in activity.Name
            //            activity.Name = activity.Text;
            //            //the username needs to be added to the database
            //            //this line sends the username to the server
                        
            //            taskID = 2;
            //        }
            //        break;
            //    case 2:
            //        if (activity.Text.StartsWith("@") == false )
            //        {
            //            await context.PostAsync("To send a message, it needs to be formatted as \"@Username message goes here...\" If you need help, type \"HELP\" ");
            //            context.Wait(MessageReceivedAsync);
            //            //if (activity.Text.Starts)
            //        }
            //        else
            //        {

            //        }

            //        break;
            //    default:
            //        await context.PostAsync("Shoot, it broke");
            //        break;
            //}

            //    Help:
            //{

            //}
            //context.Wait(MessageReceivedAsync);
        }

        private async Task onMenuSelect(IDialogContext context, IAwaitable<object> result)
        {
            var choice = await result;

            if (choice == EnrollOption)
            {
                context.Call(new EnrollDialog(), this.onResume);
            } else
            {
                context.Call(new ConversationDialog(), this.onResume);
            }
        }

        private async Task onResume(IDialogContext context, IAwaitable<object> result)
        {
           context.Wait(MessageReceivedAsync);
        }
    }
}