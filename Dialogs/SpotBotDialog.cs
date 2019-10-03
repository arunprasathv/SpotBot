 

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using System.IO;
using Newtonsoft.Json;
 using Microsoft.Bot.Schema;

namespace Hackathon.SpotBot
{
    public class SpotBotDialog : ComponentDialog
    {
        private readonly IStatePropertyAccessor<CustomerSupportTemplateState> _userProfileAccessor;
        private readonly string[] _cards =
 {
            Path.Combine(".", "Resources", "order.json"),
        };
        public SpotBotDialog(IStatePropertyAccessor<CustomerSupportTemplateState> _stateAccessor
)
            : base(nameof(SpotBotDialog))
        {
            _userProfileAccessor = _stateAccessor;//  userState.CreateProperty<SupportProfile>("SupportProfile");

            // This array defines how the Waterfall will execute.
            var waterfallSteps = new WaterfallStep[]
            {
                StartStepAsync,
                OptionsStepAsync,
                OptionConfirmStepAsync,
                MoreOrderStepAsync,
                OptionConfirmStepAsync,
                //ConfirmStepAsync,
                SummaryStepAsync,
            };

            // Add named dialogs to the DialogSet. These names are saved in the dialog state.
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new NumberPrompt<int>(nameof(NumberPrompt<int>), PromptValidatorAsync));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private static async Task<DialogTurnResult> StartStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // WaterfallStep always finishes with the end of the Waterfall or with another dialog; here it is a Prompt Dialog.
            // Running a prompt here means the next WaterfallStep will be run when the users response is received.
            return await stepContext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt =  MessageFactory.Text("Not sure where to start? Select from these commonly asked questions to start a conversation:"),
                    Choices = ChoiceFactory.ToChoices(new List<string> { "About Order", "Payment", "Order Performance" }),
                }, cancellationToken);
        }

        private static async Task<DialogTurnResult> OptionsStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["selectedOption"] = ((FoundChoice)stepContext.Result).Value;

            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("Please enter advertiser name.") }, cancellationToken);
        }

        private async Task<DialogTurnResult> OptionConfirmStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["advname"] = (string)stepContext.Result;

            // We can send messages to the user at any point in the WaterfallStep.
            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"Please hold on, while i find recent order for this advertiser"), cancellationToken);
            var cardAttachment = CreateAdaptiveCardAttachment(_cards[0]);
            //turnContext.Activity.Attachments = new List<Attachment>() { cardAttachment };
            await stepContext.Context.SendActivityAsync(MessageFactory.Attachment(cardAttachment), cancellationToken);
            // WaterfallStep always finishes with the end of the Waterfall or with another dialog; here it is a Prompt Dialog.
            return await stepContext.PromptAsync(nameof(ConfirmPrompt), new PromptOptions { Prompt = MessageFactory.Text("Was the information I provided helpful?") }, cancellationToken);
        }

        private async Task<DialogTurnResult> MoreOrderStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((bool)stepContext.Result)
            {
                //return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("Good! I'm glad I was able to help.") }, cancellationToken);
                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"Good! I'm glad I was able to help"), cancellationToken);
                return await stepContext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Do you have more queries ?"),
                    Choices = ChoiceFactory.ToChoices(new List<string> { "About Order", "Payment", "Order Performance" }),
                }, cancellationToken);
            }
            else
            {
                return await stepContext.PromptAsync(nameof(ChoicePrompt),
                 new PromptOptions
                 {
                     Prompt = MessageFactory.Text("Sorry, I couldn't help you, please select the options below again"),
                     Choices = ChoiceFactory.ToChoices(new List<string> { "About Order", "Payment", "Order Performance" }),
                 }, cancellationToken);
               
            }
        }

        //private async Task<DialogTurnResult> ConfirmStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        //{

        //}

        private async Task<DialogTurnResult> SummaryStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((bool)stepContext.Result)
            {
                 var userProfile = await _userProfileAccessor.GetAsync(stepContext.Context, () => new CustomerSupportTemplateState(), cancellationToken);

                userProfile.SelectedOption = (string)stepContext.Values["selectedOption"];
                userProfile.AdvertiserName = (string)stepContext.Values["advname"];

                var msg = "";
                await stepContext.Context.SendActivityAsync(MessageFactory.Text(msg), cancellationToken);
            }
            else
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Thanks. Your profile will not be kept."), cancellationToken);
            }

            // WaterfallStep always finishes with the end of the Waterfall or with another dialog, here it is the end.
            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }

        private static Task<bool> PromptValidatorAsync(PromptValidatorContext<int> promptContext, CancellationToken cancellationToken)
        {
            // This condition is our validation rule. You can also change the value at this point.
            return Task.FromResult(promptContext.Recognized.Succeeded && promptContext.Recognized.Value > 0 && promptContext.Recognized.Value < 150);
        }

        private static Attachment CreateAdaptiveCardAttachment(string filePath)
        {
            var adaptiveCardJson = File.ReadAllText(filePath);
            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };
            return adaptiveCardAttachment;
        }
    }
}
