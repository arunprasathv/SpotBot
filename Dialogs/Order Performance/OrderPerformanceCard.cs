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
    public class CheckOrderPerformanceDialog : ComponentDialog
    {
        private IServiceClient _client;
        private IBotServices _services;
        private readonly BotStateService _botStateService;

        OrderPerformanceResponses _responder = new OrderPerformanceResponses();
        string orderId = string.Empty;
        string selfServiceId = string.Empty;

        public CheckOrderPerformanceDialog(string dialogId, BotStateService botStateService, IBotServices services) : base(dialogId)
        {
            _client = new ServiceClient();
            _services = services;
            _botStateService = botStateService ?? throw new ArgumentNullException(nameof(botStateService));

            var checkPayment = new WaterfallStep[]
            {
                PromptForOrderNumber,
                PromptForSelfServiceId,
                ShowOrderPerformance
            };

            InitialDialogId = $"{nameof(CheckOrderPerformanceDialog)}.mainFlow";
            AddDialog(new WaterfallDialog($"{nameof(CheckOrderPerformanceDialog)}.mainFlow", checkPayment));
            AddDialog(new TextPrompt($"{nameof(CheckOrderPerformanceDialog)}.orderNumber"));
        }

        private async Task<DialogTurnResult> PromptForOrderNumber(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync($"{nameof(CheckOrderPerformanceDialog)}.orderNumber", new PromptOptions
            {
                Prompt = MessageFactory.Text("I am happy to help. Could you please provide the order id?")
                //RetryPrompt = MessageFactory.Text("The value entered must be between the hours of 9 am and 5 pm.")
            }, cancellationToken);
        }

        private async Task<DialogTurnResult> PromptForSelfServiceId(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            orderId = (string)stepContext.Result;

            return await stepContext.PromptAsync($"{nameof(CheckOrderPerformanceDialog)}.orderNumber", new PromptOptions
            {
                Prompt = MessageFactory.Text("Could you please provide the selfservice id?")
                //RetryPrompt = MessageFactory.Text("The value entered must be between the hours of 9 am and 5 pm.")
            }, cancellationToken);
        }

        private async Task<DialogTurnResult> ShowOrderPerformance(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            selfServiceId = (string)stepContext.Result;

            var orderPerformance = _client.GetOrderPerformance(orderId, selfServiceId);

            await _responder.ReplyWith(stepContext.Context, "Thank you. Here is your order performance:");
            await _responder.ReplyWith(stepContext.Context, OrderPerformanceResponses.ResponseIds.OrderPerformanceCard, orderPerformance);

            return await stepContext.EndDialogAsync();
        }
    }
}
