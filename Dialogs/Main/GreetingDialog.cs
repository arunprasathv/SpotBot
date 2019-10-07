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
    public class GreetingDialog : ComponentDialog
    {
        private IServiceClient _client;
        private IBotServices _services;
        private readonly BotStateService _botStateService;
        private MainResponses _responder = new MainResponses();

        public GreetingDialog(string dialogId, BotStateService botStateService, IBotServices services) : base(dialogId)
        {
            _client = new ServiceClient();
            _services = services;
            _botStateService = botStateService ?? throw new ArgumentNullException(nameof(botStateService));

            var greetingSteps = new WaterfallStep[]
            {
                 ShowGreetings
            };

            InitialDialogId = $"{nameof(GreetingDialog)}.mainFlow";
            AddDialog(new WaterfallDialog($"{nameof(GreetingDialog)}.mainFlow", greetingSteps));
         }

        private async Task<DialogTurnResult> ShowGreetings(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            await _responder.ReplyWith(stepContext.Context, MainResponses.Greeting);
 
            return await stepContext.EndDialogAsync();
        }
    }
}