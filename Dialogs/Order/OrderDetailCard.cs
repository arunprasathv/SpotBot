using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using SpotBot.Dialogs.Order.Resources;


 namespace Hackathon.SpotBot
{
    public class CheckOrderStatusDialog : ComponentDialog
    {
        private IServiceClient _client;
        private IBotServices _services;
        // private IStatePropertyAccessor<CustomerSupportTemplateState> _stateAccessor;
        private OrderResponses _responder = new OrderResponses();
        private readonly BotStateService _botStateService;

        public CheckOrderStatusDialog(string dialogId, BotStateService botStateService,
            IBotServices services
       //     IStatePropertyAccessor<CustomerSupportTemplateState> stateAccessor
            ) :  base(dialogId)
        // : base(services, nameof(CheckOrderStatusDialog), telemetryClient)
        {
            _client = new DemoServiceClient();
            _services = services;
           // _stateAccessor = stateAccessor;
            _botStateService = botStateService ?? throw new System.ArgumentNullException(nameof(botStateService));

            var checkOrderStatus = new WaterfallStep[]
            {
                PromptForOrderNumber,
                PromptForPhoneNumber,
                ShowOrderStatus,
            };

            InitialDialogId = $"{nameof(CheckOrderStatusDialog)}.mainFlow";
            AddDialog(new WaterfallDialog($"{nameof(CheckOrderStatusDialog)}.mainFlow", checkOrderStatus) );
            AddDialog(new TextPrompt($"{nameof(CheckOrderStatusDialog)}.orderNumber"));
            AddDialog(new TextPrompt($"{nameof(CheckOrderStatusDialog)}.phoneNumber"));
        }

        private async Task<DialogTurnResult> PromptForOrderNumber(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync($"{nameof(CheckOrderStatusDialog)}.orderNumber", new PromptOptions
            {
                Prompt = await _responder.RenderTemplate(stepContext.Context, stepContext.Context.Activity.Locale, OrderResponses.ResponseIds.OrderNumberPrompt),
                RetryPrompt = await _responder.RenderTemplate(stepContext.Context, stepContext.Context.Activity.Locale, OrderResponses.ResponseIds.OrderNumberReprompt)
            }, cancellationToken);
        }

        private async Task<DialogTurnResult> PromptForPhoneNumber(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var state = await _botStateService.OrderDataAccessor.GetAsync(stepContext.Context, () => new Order());

            var orderNumber = (string)stepContext.Result;
            var order = _client.GetOrderByNumber(orderNumber);

            return await stepContext.PromptAsync($"{nameof(CheckOrderStatusDialog)}.phoneNumber", new PromptOptions
            {
                Prompt = await _responder.RenderTemplate(stepContext.Context, stepContext.Context.Activity.Locale, OrderResponses.ResponseIds.PhoneNumberPrompt),
                RetryPrompt = await _responder.RenderTemplate(stepContext.Context, stepContext.Context.Activity.Locale, OrderResponses.ResponseIds.PhoneNumberPrompt)
            }, cancellationToken);
        }

        private async Task<DialogTurnResult> ShowOrderStatus(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var order = await _botStateService.OrderDataAccessor.GetAsync(stepContext.Context, () => new Order());
            //var order = state;

            await _responder.ReplyWith(stepContext.Context, "Thank you. Here is your order status:");
            await _responder.ReplyWith(stepContext.Context, OrderResponses.ResponseIds.OrderStatusCard, order);
            return await stepContext.EndDialogAsync();
        }

        private class DialogIds
        {
            public const string OrderNumberPrompt = "orderNumberPrompt";
            public const string PhoneNumberPrompt = "phoneNumberPrompt";
        }
    }
}
