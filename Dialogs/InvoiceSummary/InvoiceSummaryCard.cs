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
    public class InvoiceSummaryDialog : ComponentDialog
    {
        private IServiceClient _client;
        private IBotServices _services;
        private readonly BotStateService _botStateService;

        InvoiceSummaryResponses _responder = new InvoiceSummaryResponses();

        public InvoiceSummaryDialog(string dialogId, BotStateService botStateService, IBotServices services, PortalContext _portalContext) : base(dialogId)
        {
            _client = new ServiceClient(_portalContext);
            _services = services;
            _botStateService = botStateService ?? throw new ArgumentNullException(nameof(botStateService));

            var invoiceSummary = new WaterfallStep[]
            {
                PromptForAdvertiserCode,
                PromptForBroadcastMonth,
                ShowInvoiceSummary
            };

            InitialDialogId = $"{nameof(InvoiceSummaryDialog)}.mainFlow";
            AddDialog(new WaterfallDialog($"{nameof(InvoiceSummaryDialog)}.mainFlow", invoiceSummary));
            AddDialog(new TextPrompt($"{nameof(InvoiceSummaryDialog)}.advertisercode"));
            AddDialog(new TextPrompt($"{nameof(InvoiceSummaryDialog)}.broadcastmonth"));
        }

        private async Task<DialogTurnResult> PromptForAdvertiserCode(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync($"{nameof(InvoiceSummaryDialog)}.advertisercode", new PromptOptions
            {
                Prompt = MessageFactory.Text("I am happy to help. Could you please provide the advertiser id?")
                //RetryPrompt = MessageFactory.Text("The value entered must be between the hours of 9 am and 5 pm.")
            }, cancellationToken);
        }

        private async Task<DialogTurnResult> PromptForBroadcastMonth(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["advertiserCode"] = (string)stepContext.Result;

            return await stepContext.PromptAsync($"{nameof(InvoiceSummaryDialog)}.broadcastmonth", new PromptOptions
            {
                Prompt = MessageFactory.Text("Could you please provide the broadcast month (in YYYYMM format)?")
                //RetryPrompt = MessageFactory.Text("The value entered must be between the hours of 9 am and 5 pm.")
            }, cancellationToken);
        }

        private async Task<DialogTurnResult> ShowInvoiceSummary(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var advertiserCode = (string)stepContext.Values["advertiserCode"];
            var broadcastMonth = (string)stepContext.Result;

            var invoiceSummary = _client.GetInvoiceSummary(advertiserCode, broadcastMonth);
            await _responder.ReplyWith(stepContext.Context, "Thank you. Here is your invoice summary:");
            await _responder.ReplyWith(stepContext.Context, InvoiceSummaryResponses.ResponseIds.InvoiceSummaryCard, invoiceSummary);

            return await stepContext.EndDialogAsync();
        }
    }
}
