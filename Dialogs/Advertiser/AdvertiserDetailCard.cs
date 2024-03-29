﻿using Hackathon.SpotBot;
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
                PromptForAccountId,
                ShowAdvertiserDetails
            };

            InitialDialogId = $"{nameof(AdvertiserDialog)}.mainFlow";
            AddDialog(new WaterfallDialog($"{nameof(AdvertiserDialog)}.mainFlow", checkAdvertiser));
            AddDialog(new TextPrompt($"{nameof(AdvertiserDialog)}.accountId"));
        }

        private async Task<DialogTurnResult> PromptForAccountId(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync($"{nameof(AdvertiserDialog)}.accountId", new PromptOptions
            {
                Prompt = MessageFactory.Text("I am happy to help. Could you please provide Account ID?")
            }, cancellationToken);
        }

        private async Task<DialogTurnResult> ShowAdvertiserDetails(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string accountId = (string) stepContext.Result;
            var advertiserDetails = _client.GetAdvertiserDetails(accountId);


            await _responder.ReplyWith(stepContext.Context, "Thank you. Here is your Advertiser Detail:");
            await _responder.ReplyWith(stepContext.Context, AdvertiserResponse.ResponseIds.AdvertiserCard, advertiserDetails);

            return await stepContext.EndDialogAsync();
        }


    }
}