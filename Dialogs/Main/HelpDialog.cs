using Hackathon.SpotBot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
 using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System;
using Newtonsoft.Json;

namespace Hackathon.SpotBot
{
    public class HelpDialog : ComponentDialog
    {
        private IServiceClient _client;
        private IBotServices _services;
        private readonly BotStateService _botStateService;
        private MainResponses _responder = new MainResponses();
     
        public HelpDialog(string dialogId, BotStateService botStateService, IBotServices services) : base(dialogId)
        {
            _client = new ServiceClient();
            _services = services;
            _botStateService = botStateService ?? throw new ArgumentNullException(nameof(botStateService));

            var greetingSteps = new WaterfallStep[]
            {
                 ShowHelp
            };

            InitialDialogId = $"{nameof(CheckPaymentDialog)}.mainFlow";
            AddDialog(new WaterfallDialog($"{nameof(CheckPaymentDialog)}.mainFlow", greetingSteps));
        }

        private async Task<DialogTurnResult> ShowHelp(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            var cardAttachment = CreateAdaptiveCardAttachment();
            //turnContext.Activity.Attachments = new List<Attachment>() { cardAttachment };
            await stepContext.Context.SendActivityAsync(MessageFactory.Attachment(cardAttachment), cancellationToken);


            return await stepContext.EndDialogAsync();
        }
        private static Attachment CreateAdaptiveCardAttachment()
        {
            var filePath = @".\Dialogs\Main\Resources\HelpCard.json";
            var adaptiveCardJson = File.ReadAllText(filePath);
            
            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };
            // adaptiveCardAttachment.Content = adaptiveCardAttachment.Content.ToString().Replace("{{orderNumber}}", "12333");

            return adaptiveCardAttachment;
        }
    }
}