using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using SpotBot.Dialogs.Order.Resources;
using Newtonsoft.Json;
using System.IO;
using System;

namespace Hackathon.SpotBot
{
    public class CheckOrderStatusDialog : ComponentDialog
    {
        private IServiceClient _client;
        private IBotServices _services;
        private readonly string[] _cards =
{
            Path.Combine(".", "Resources", "order.json"),
        };
        // private IStatePropertyAccessor<CustomerSupportTemplateState> _stateAccessor;
        private OrderResponses _responder = new OrderResponses();
        private readonly BotStateService _botStateService;

        public CheckOrderStatusDialog(string dialogId, BotStateService botStateService,
            IBotServices services
       //     IStatePropertyAccessor<CustomerSupportTemplateState> stateAccessor
            ) :  base(dialogId)
        // : base(services, nameof(CheckOrderStatusDialog), telemetryClient)
        {
            _client = new ServiceClient();
            _services = services;
           // _stateAccessor = stateAccessor;
            _botStateService = botStateService ?? throw new System.ArgumentNullException(nameof(botStateService));

            var checkOrderStatus = new WaterfallStep[]
            {
                PromptForSelfServiceId,
                PromptForOrderNumber,
                ShowOrderStatus,
            };

            InitialDialogId = $"{nameof(CheckOrderStatusDialog)}.mainFlow";
            AddDialog(new WaterfallDialog($"{nameof(CheckOrderStatusDialog)}.mainFlow", checkOrderStatus) );
            AddDialog(new TextPrompt($"{nameof(CheckOrderStatusDialog)}.orderNumber", GuidValidatorAsync));
            AddDialog(new TextPrompt($"{nameof(CheckOrderStatusDialog)}.ssid", GuidValidatorAsync));
        }

        private async Task<DialogTurnResult> PromptForSelfServiceId(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var state = await _botStateService.OrderDataAccessor.GetAsync(stepContext.Context, () => new Order());
            return await stepContext.PromptAsync($"{nameof(CheckOrderStatusDialog)}.ssid", new PromptOptions
            {
                Prompt = MessageFactory.Text("I am happy to help. Could you please provide the Self Service Id?"),
                RetryPrompt = MessageFactory.Text("The value entered must be a valid GUID."),
            }, cancellationToken);
           
        }
        private async Task<DialogTurnResult> PromptForOrderNumber(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["ssId"] = (string)stepContext.Result;
            return await stepContext.PromptAsync($"{nameof(CheckOrderStatusDialog)}.orderNumber", new PromptOptions
            {
                Prompt = MessageFactory.Text("Could you please provide the Order Id?")
            }, cancellationToken);

            
        }
        private async Task<DialogTurnResult> ShowOrderStatus(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["orderId"] = (string)stepContext.Result;
            var ssId = (string)stepContext.Values["ssId"];
           var orderId=(string)stepContext.Values["orderId"];

            var order = _client.GetOrderByNumber(ssId, orderId);
            var cardAttachment = CreateAdaptiveCardAttachment(_cards[0], order);
            await stepContext.Context.SendActivityAsync(MessageFactory.Attachment(cardAttachment), cancellationToken);
            return await stepContext.EndDialogAsync();
        }
        private static Attachment CreateAdaptiveCardAttachment(string filePath,Order order)
        {
            filePath = @".\Dialogs\Order\Resources\order.json";
            var adaptiveCardJson = File.ReadAllText(filePath);
            adaptiveCardJson=adaptiveCardJson.Replace("{{orderNumber}}", order.orderNumber.ToString());
            adaptiveCardJson = adaptiveCardJson.Replace("{{orderStatus}}", order.orderStatus);
            adaptiveCardJson = adaptiveCardJson.Replace("{{firstName}}", order.advertiser.firstName??string.Empty);
            adaptiveCardJson = adaptiveCardJson.Replace("{{lastName}}", order.advertiser.lastName ?? string.Empty);
            adaptiveCardJson = adaptiveCardJson.Replace("{{emailAddress}}", order.advertiser.emailAddress ?? string.Empty);
            adaptiveCardJson = adaptiveCardJson.Replace("{{startDate}}", order.startDate.ToShortDateString() ?? string.Empty);
            adaptiveCardJson = adaptiveCardJson.Replace("{{endDate}}", order.endDate.ToShortDateString() ?? string.Empty);
            adaptiveCardJson = adaptiveCardJson.Replace("{{creationDateTime}}", order.creationDateTime.ToShortDateString() ?? string.Empty);
            adaptiveCardJson = adaptiveCardJson.Replace("{{amount}}", order.amount.ToString() ?? string.Empty);
            adaptiveCardJson = adaptiveCardJson.Replace("{{Demographics}}", order.demo.longDescription ?? string.Empty);

            int i = 1;
            var desc = "description";
            order.zones.ForEach(   x => {
                if (i == 1)
                {
                    adaptiveCardJson = adaptiveCardJson.Replace("{{description1}}", x.description ?? string.Empty);
                    adaptiveCardJson = adaptiveCardJson.Replace("{{marketName1}}", x.marketName ?? string.Empty);
                }
                if (i == 2)
                {
                    adaptiveCardJson = adaptiveCardJson.Replace("{{description2}}", x.description ?? string.Empty);
                    adaptiveCardJson = adaptiveCardJson.Replace("{{marketName2}}", x.marketName ?? string.Empty);
                }
                if (i == 3)
                {
                    adaptiveCardJson = adaptiveCardJson.Replace("{{description3}}", x.description ?? string.Empty);
                    adaptiveCardJson = adaptiveCardJson.Replace("{{marketName3}}", x.marketName ?? string.Empty);
                }
                if (i == 4 &&  !string.IsNullOrEmpty(x.description))
                {
                    adaptiveCardJson = adaptiveCardJson.Replace("{{description4}}", x.description ?? string.Empty);
                    adaptiveCardJson = adaptiveCardJson.Replace("{{marketName4}}", x.marketName ?? string.Empty);
                }
                i++;
            }
);
            if (order.creative.url.Contains("mp4"))
            {
                adaptiveCardJson = adaptiveCardJson.Replace("{{adVideoURL}}", order.creative.url);
            }
            else
            {
                adaptiveCardJson = adaptiveCardJson.Replace("{{adVideoURL}}", "https://ssbdevassetlibrary.blob.core.windows.net/users/138477ff-1341-5ec6-d350-f1185db9b5f6/68F43936-E1E9-42BB-9089-A498AD8A2E32_UploadedCreative.mp4?sv=2018-03-28&sig=Fj%2F0tyMc7qM%2F%2FJ87aK5EOTB83d1ia6W6NvgZRv3Wv2g%3D&spr=https&se=2019-10-07T15%3A13%3A58Z&srt=co&ss=b&sp=rcwdl");
            }

           var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };
           // adaptiveCardAttachment.Content = adaptiveCardAttachment.Content.ToString().Replace("{{orderNumber}}", "12333");
            
            return adaptiveCardAttachment;
        }

        private Task<bool> GuidValidatorAsync(PromptValidatorContext<string> promptContext, CancellationToken cancellationToken)
        {
            var valid = true;
            System.Guid yourGuid;
            if (promptContext.Recognized.Succeeded)
            {
                if (!System.Guid.TryParse(promptContext.Recognized.Value, out yourGuid))
                {
                    valid = false;
                }
            }
            return Task.FromResult(valid);
        }
      

    private class DialogIds
        {
            public const string OrderNumberPrompt = "orderNumberPrompt";
            public const string PhoneNumberPrompt = "phoneNumberPrompt";
        }
    }
}
