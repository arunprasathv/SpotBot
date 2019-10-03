using Hackathon.SpotBot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hackathon.SpotBot
{
    public class CheckPaymentDialog : ComponentDialog
    {
        private IServiceClient _client;
        private IBotServices _services;
        private readonly BotStateService _botStateService;
        private PaymentResponses _responder = new PaymentResponses();

        public CheckPaymentDialog(string dialogId, BotStateService botStateService, IBotServices services) : base(dialogId)
        {
            _client = new ServiceClient();
            _services = services;
            _botStateService = botStateService ?? throw new System.ArgumentNullException(nameof(botStateService));

            var checkPayment = new WaterfallStep[]
            {
                PromptForOrderNumber,
                ShowPaymentDetails
            };

            InitialDialogId = $"{nameof(CheckOrderStatusDialog)}.mainFlow";
            AddDialog(new WaterfallDialog($"{nameof(CheckOrderStatusDialog)}.mainFlow", checkPayment));
            AddDialog(new TextPrompt($"{nameof(CheckOrderStatusDialog)}.orderNumber"));
        }

        private async Task<DialogTurnResult> PromptForOrderNumber(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync($"{nameof(CheckOrderStatusDialog)}.orderNumber", new PromptOptions
            {
                Prompt = MessageFactory.Text("I am happy to help. Could you please provide the order id?")
                //RetryPrompt = MessageFactory.Text("The value entered must be between the hours of 9 am and 5 pm.")
            }, cancellationToken);
        }

        private async Task<DialogTurnResult> ShowPaymentDetails(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string orderId = (string)stepContext.Result;

            var paymentDetails = _client.GetPaymentDetails(orderId);

            await _responder.ReplyWith(stepContext.Context, "Thank you. Here is your order status:");
            await _responder.ReplyWith(stepContext.Context, PaymentResponses.ResponseIds.PaymentStatusCard, paymentDetails);

            return await stepContext.EndDialogAsync();
        }
    }
}