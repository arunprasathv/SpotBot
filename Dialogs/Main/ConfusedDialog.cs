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
    public class ConfusedDialog : ComponentDialog
    {
        private IServiceClient _client;
        private IBotServices _services;
        private readonly BotStateService _botStateService;
        private MainResponses _responder = new MainResponses();

        public ConfusedDialog(string dialogId, BotStateService botStateService, IBotServices services) : base(dialogId)
        {
            _client = new ServiceClient();
            _services = services;
            _botStateService = botStateService ?? throw new ArgumentNullException(nameof(botStateService));

            var greetingSteps = new WaterfallStep[]
            {
                 ShowConfused
            };

            InitialDialogId = $"{nameof(CheckPaymentDialog)}.mainFlow";
            AddDialog(new WaterfallDialog($"{nameof(CheckPaymentDialog)}.mainFlow", greetingSteps));
        }

        private async Task<DialogTurnResult> ShowConfused(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            await _responder.ReplyWith(stepContext.Context, MainResponses.Confused);

            return await stepContext.EndDialogAsync();
        }
    }
}