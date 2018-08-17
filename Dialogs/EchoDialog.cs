using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;


namespace Microsoft.Bot.Sample.SimpleEchoBot
{
    [Serializable]
    public class EchoDialog : IDialog<object>
    {
        protected int count = 1;

        protected string askedAboutPancakes = null;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            string response = null;
            
            switch (message.Text)
            {
                case "version":
                    response = $"{count++}: Version 0.0.0.1-alpha";
                    break;
                case "pancakes":
                    if (askedAboutPancakes != null)
                    {
                        response = $"{count++}: You've asked about pancakes {askedAboutPancakes}.";
                    }
                    else
                    {
                        PromptDialog.Confirm(
                            context,
                            AfterPancakeAsync,
                            "do you like pancakes?",
                            "What?!",
                            promptStyle: PromptStyle.Auto);
                        askedAboutPancakes = DateTime.Now.ToString();
                    }
                    break;
                
                case "reset":
                    PromptDialog.Confirm(
                        context,
                        AfterResetAsync,
                        "Are you sure you want to reset the count?",
                        "Didn't get that!",
                        promptStyle: PromptStyle.Auto);
                    break;
                default:
                    response = $"{count++}: You said {message.Text}";
                    break;
            }
            // Send standard response
            if (response != null)
            {
                await context.PostAsync($"{count++}: You said {message.Text}");
                context.Wait(MessageReceivedAsync);
            }
        }

        private async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirm = await argument;
            if (confirm)
            {
                count = 1;
                await context.PostAsync("Reset count.");
            }
            else
            {
                await context.PostAsync("Did not reset count.");
            }
            context.Wait(MessageReceivedAsync);
        }

        private async Task AfterPancakeAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirm = await argument;
            if (confirm)
            {
                count = 1;
                await context.PostAsync("I also like them.");
            }
            else
            {
                await context.PostAsync("Still, I like them.");
            }
            context.Wait(MessageReceivedAsync);
        }
    }
}