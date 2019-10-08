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
    public class CommissionDialog : ComponentDialog
    {
        private IServiceClient _client;
        private IBotServices _services;
        private readonly BotStateService _botStateService;
        private CommissionResponses _responder = new CommissionResponses();

        public CommissionDialog(string dialogId, BotStateService botStateService, IBotServices services) : base(dialogId)
        {
            _client = new ServiceClient();
            _services = services;
            _botStateService = botStateService ?? throw new ArgumentNullException(nameof(botStateService));

            var checkCommission = new WaterfallStep[]
            {
                ShowCommissionDetails
            };

            InitialDialogId = $"{nameof(CommissionDialog)}.mainFlow";
            AddDialog(new WaterfallDialog($"{nameof(CommissionDialog)}.mainFlow", checkCommission));
        }

        private async Task<DialogTurnResult> ShowCommissionDetails(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        { 
            var commissionDetails = _client.GetCommissionDetails();

            await _responder.ReplyWith(stepContext.Context, "Thank you. Here is your Commission summary:");
            await _responder.ReplyWith(stepContext.Context, CommissionResponses.ResponseIds.CommissionCard, commissionDetails);

            return await stepContext.EndDialogAsync();
        }


    }
}