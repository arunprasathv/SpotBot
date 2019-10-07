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
    public class GoodbyeDialog : ComponentDialog
    {
        private IServiceClient _client;
        private IBotServices _services;
        private readonly BotStateService _botStateService;
        private MainResponses _responder = new MainResponses();

        public GoodbyeDialog(string dialogId, BotStateService botStateService, IBotServices services) : base(dialogId)
        {
            _client = new ServiceClient();
            _services = services;
            _botStateService = botStateService ?? throw new ArgumentNullException(nameof(botStateService));

            var goodbyeSteps = new WaterfallStep[]
            {
                 ShowGGoodbye
            };

            InitialDialogId = $"{nameof(GreetingDialog)}.mainFlow";
            AddDialog(new WaterfallDialog($"{nameof(GreetingDialog)}.mainFlow", goodbyeSteps));
        }

        private async Task<DialogTurnResult> ShowGGoodbye(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            await _responder.ReplyWith(stepContext.Context, MainResponses.Goodbye);

            return await stepContext.EndDialogAsync();
        }
    }
}