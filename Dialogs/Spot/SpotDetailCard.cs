using Hackathon.SpotBot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hackathon.SpotBot
{
    public class CheckSpotDataDialog : ComponentDialog
    {
        private IServiceClient _client;
        private IBotServices _services;
        private readonly BotStateService _botStateService;
        private SpotResponses _responder = new SpotResponses();
        private readonly string[] _cards =
        {
            Path.Combine(".", "Resources", "spot.json"),
        };

        public CheckSpotDataDialog(string dialogId, BotStateService botStateService, IBotServices services) : base(dialogId)
        {
            _client = new DemoServiceClient();
            _services = services;
            _botStateService = botStateService ?? throw new System.ArgumentNullException(nameof(botStateService));

            var checkSpots = new WaterfallStep[]
            {
                PromptForOrderID,
                ShowSpotDetails
            };

            InitialDialogId = $"{nameof(CheckSpotDataDialog)}.mainFlow";
            AddDialog(new WaterfallDialog($"{nameof(CheckSpotDataDialog)}.mainFlow", checkSpots));
            AddDialog(new TextPrompt($"{nameof(CheckSpotDataDialog)}.orderNumber"));
        }

        private async Task<DialogTurnResult> PromptForOrderID(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync($"{nameof(CheckSpotDataDialog)}.orderNumber", new PromptOptions
            {
                Prompt = MessageFactory.Text("I am happy to help. Could you please provide the OneTim Order#?")
            }, cancellationToken);
        }

        private async Task<DialogTurnResult> ShowSpotDetails(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string oneTimOrderId = (string)stepContext.Result;

            // Returns all successfully aired spots
            //var spotData = new List<Spot>();
            var spotData = _client.GetSpotData(oneTimOrderId);
                              //.SelectMany(n => n.SpotDetails)
                              //.Where(c => c.AiredSuccessful);
            var cardAttachment = CreateAdaptiveCardAttachment(_cards[0], spotData);
            await stepContext.Context.SendActivityAsync(MessageFactory.Attachment(cardAttachment), cancellationToken);

            await _responder.ReplyWith(stepContext.Context, "Thank you. Here are the spots played with your order:");
            //await _responder.ReplyWith(stepContext.Context, SpotResponses.ResponseIds.SpotsAiredStatusCard, spotData);

            return await stepContext.EndDialogAsync();
        }

        private static Attachment CreateAdaptiveCardAttachment(string filePath, List<Spot> spots)
        {
            filePath = @".\Dialogs\Spot\spot.json";
            var adaptiveCardJson = File.ReadAllText(filePath);
            //adaptiveCardJson = adaptiveCardJson.Replace("{{orderNumber}}", order.orderNumber.ToString());
            //adaptiveCardJson = adaptiveCardJson.Replace("{{orderStatus}}", order.orderStatus);
            int i = 1;
            spots.ForEach(x =>
            {
                if (i == 1)
                {
                    adaptiveCardJson = adaptiveCardJson.Replace("{{spotId1}}", x.SpotID.ToString());
                    adaptiveCardJson = adaptiveCardJson.Replace("{{spotName1}}", x.Description);
                }
                if (i == 2)
                {
                    adaptiveCardJson = adaptiveCardJson.Replace("{{spotId2}}", x.SpotID.ToString());
                    adaptiveCardJson = adaptiveCardJson.Replace("{{spotName2}}", x.Description);
                }
                i++;
                //adaptiveCardJson = adaptiveCardJson.Replace("{{advertiser}}", x.Advertiser);
                //adaptiveCardJson = adaptiveCardJson.Replace("{{oneTimOrderId}}", x.OneTimOrderId.ToString());

                int j = 1;
                x.SpotDetails.ForEach(s =>
                {
                    if (j == 1)
                    {
                        adaptiveCardJson = adaptiveCardJson.Replace("{{zone1}}", s.Zone);
                        adaptiveCardJson = adaptiveCardJson.Replace("{{network1}}", s.Network);
                        adaptiveCardJson = adaptiveCardJson.Replace("{{airedAt1}}", s.AiredAt.ToString());
                        adaptiveCardJson = adaptiveCardJson.Replace("{{isSuccess1}}", s.ErrorStatus.ToString());
                    }
                    if (j == 2)
                    {
                        adaptiveCardJson = adaptiveCardJson.Replace("{{zone2}}", s.Zone);
                        adaptiveCardJson = adaptiveCardJson.Replace("{{network2}}", s.Network);
                        adaptiveCardJson = adaptiveCardJson.Replace("{{airedAt2}}", s.AiredAt.ToString());
                        adaptiveCardJson = adaptiveCardJson.Replace("{{isSuccess2}}", s.ErrorStatus.ToString());
                    }
                    if (j == 3)
                    {
                        adaptiveCardJson = adaptiveCardJson.Replace("{{zone3}}", s.Zone);
                        adaptiveCardJson = adaptiveCardJson.Replace("{{network3}}", s.Network);
                        adaptiveCardJson = adaptiveCardJson.Replace("{{airedAt3}}", s.AiredAt.ToString());
                        adaptiveCardJson = adaptiveCardJson.Replace("{{isSuccess3}}", s.ErrorStatus.ToString());
                    }
                    if (j == 4)
                    {
                        adaptiveCardJson = adaptiveCardJson.Replace("{{zone4}}", s.Zone);
                        adaptiveCardJson = adaptiveCardJson.Replace("{{network4}}", s.Network);
                        adaptiveCardJson = adaptiveCardJson.Replace("{{airedAt4}}", s.AiredAt.ToString());
                        adaptiveCardJson = adaptiveCardJson.Replace("{{isSuccess4}}", s.ErrorStatus.ToString());
                    }
                    j++;
                });
            });

            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };

            return adaptiveCardAttachment;
        }

    }
}
