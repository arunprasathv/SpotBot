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
    public class AdvertiserDialog : ComponentDialog
    {
        private IServiceClient _client;
        private IBotServices _services;
        private readonly BotStateService _botStateService;
        private AdvertiserResponse _responder = new AdvertiserResponse();

        public AdvertiserDialog(string dialogId, BotStateService botStateService, IBotServices services) : base(dialogId)
        {
            _client = new ServiceClient();
            _services = services;
            _botStateService = botStateService ?? throw new ArgumentNullException(nameof(botStateService));

            var checkAdvertiser = new WaterfallStep[]
            {
                PromptForAdvertiserName,
                ShowAdvertiserDetails
            };

            InitialDialogId = $"{nameof(AdvertiserDialog)}.mainFlow";
            AddDialog(new WaterfallDialog($"{nameof(AdvertiserDialog)}.mainFlow", checkAdvertiser));
            AddDialog(new TextPrompt($"{nameof(AdvertiserDialog)}.advertiserName"));
        }

        private async Task<DialogTurnResult> PromptForAdvertiserName(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync($"{nameof(AdvertiserDialog)}.advertiserName", new PromptOptions
            {
                Prompt = MessageFactory.Text("I am happy to help. Could you please provide Advertiser Name?")
            }, cancellationToken);
        }

        private async Task<DialogTurnResult> ShowAdvertiserDetails(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        { 
            var advertiserDetails = _client.GetAdvertiserDetails();


            await _responder.ReplyWith(stepContext.Context, "Thank you. Here is your Advertiser Detail:");
            await _responder.ReplyWith(stepContext.Context, AdvertiserResponse.ResponseIds.AdvertiserCard, advertiserDetails);

            return await stepContext.EndDialogAsync();
        }


    }
}