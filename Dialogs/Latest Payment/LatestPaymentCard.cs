using Hackathon.SpotBot.Data;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hackathon.SpotBot
{
    public class LatestPaymentDialog : ComponentDialog
    {
        private IServiceClient _client;
        private IBotServices _services;
        private readonly BotStateService _botStateService;

        LatestPaymentResponses _responder = new LatestPaymentResponses();

        public LatestPaymentDialog(string dialogId, BotStateService botStateService, IBotServices services, PortalContext _portalContext) : base(dialogId)
        {
            _client = new ServiceClient(_portalContext);
            _services = services;
            _botStateService = botStateService ?? throw new ArgumentNullException(nameof(botStateService));

            var latestPayment = new WaterfallStep[]
            {
                PromptForAdvertiserCode,
                ShowLatestPayment
            };

            InitialDialogId = $"{nameof(LatestPaymentDialog)}.mainFlow";
            AddDialog(new WaterfallDialog($"{nameof(LatestPaymentDialog)}.mainFlow", latestPayment));
            AddDialog(new TextPrompt($"{nameof(LatestPaymentDialog)}.advertisercode"));
        }

        private async Task<DialogTurnResult> PromptForAdvertiserCode(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync($"{nameof(LatestPaymentDialog)}.advertisercode", new PromptOptions
            {
                Prompt = MessageFactory.Text("I am happy to help. Could you please provide the advertiser id?")
                //RetryPrompt = MessageFactory.Text("The value entered must be between the hours of 9 am and 5 pm.")
            }, cancellationToken);
        }

        private async Task<DialogTurnResult> ShowLatestPayment(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var advertiserCode = (string)stepContext.Result;

            var latestPayemntInfo = _client.GetLatestPayemntInfo(advertiserCode);
            await _responder.ReplyWith(stepContext.Context, "Thank you. Here is your CAP payment information:");
            await _responder.ReplyWith(stepContext.Context, LatestPaymentResponses.ResponseIds.LatestPaymentCard, latestPayemntInfo);

            return await stepContext.EndDialogAsync();
        }
    }
}
