using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.TemplateManager;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.SpotBot
{
    public class SpotResponses : TemplateManager
    {
        private static LanguageTemplateDictionary _responseTemplates = new LanguageTemplateDictionary
        {
            ["default"] = new TemplateIdMap
            {
                {ResponseIds.SpotsAiredStatusCard,(context,data)=>CreateSpotsAiredStatusCard(context,data) }
            },
            ["en"] = new TemplateIdMap { },
        };

        public SpotResponses()
        {
            Register(new DictionaryRenderer(_responseTemplates));
        }

        private static IMessageActivity CreateSpotsAiredStatusCard(ITurnContext context, List<SpotBot.Spot> spotData)
        {
            var response = context.Activity.CreateReply();

            var card = new AdaptiveCard()
            {
                Body = new List<AdaptiveElement>()
            };


            card.Body.Add(new AdaptiveContainer()
            {
                Items = new List<AdaptiveElement>()
                {
                    new AdaptiveTextBlock() { Text = "Spots Played", Size = AdaptiveTextSize.Medium, Weight = AdaptiveTextWeight.Bolder },
                },
            });

            foreach (var spot in spotData)
            {
                card.Body.Add(new AdaptiveFactSet()
                {

                    Facts = new List<AdaptiveFact>()
                {
                    new AdaptiveFact("Spot #:", spot.SpotID.ToString()),
                    new AdaptiveFact("Spot Description:", spot.Description),
                    new AdaptiveFact("Advertiser:", spot.Advertiser)
                }
                });

                card.Body.Add(new AdaptiveContainer()
                {
                    Spacing = AdaptiveSpacing.Large,
                    Style = AdaptiveContainerStyle.Emphasis,
                    Items = new List<AdaptiveElement>()
                {
                   new AdaptiveColumnSet()
                   {
                       Columns = new List<AdaptiveColumn>()
                       {
                           new AdaptiveColumn()
                           {
                               Items = new List<AdaptiveElement>()
                               {
                                   new AdaptiveTextBlock()
                                   {
                                       Weight = AdaptiveTextWeight.Bolder,
                                       Text = "Zone"
                                   }
                               },
                               Width = AdaptiveColumnWidth.Auto
                           },

                           new AdaptiveColumn()
                           {
                               Items = new List<AdaptiveElement>()
                               {
                                   new AdaptiveTextBlock()
                                   {
                                       Weight = AdaptiveTextWeight.Bolder,
                                       Text = "Network"
                                   }
                               },
                               Width = AdaptiveColumnWidth.Auto
                           },

                           new AdaptiveColumn()
                           {
                               Items = new List<AdaptiveElement>()
                               {
                                   new AdaptiveTextBlock()
                                   {
                                       Weight = AdaptiveTextWeight.Bolder,
                                       Text = "Aired At"
                                   }
                               },
                               Width = AdaptiveColumnWidth.Auto
                           },

                           new AdaptiveColumn()
                           {
                               Items = new List<AdaptiveElement>()
                               {
                                   new AdaptiveTextBlock()
                                   {
                                       Weight = AdaptiveTextWeight.Bolder,
                                       Text = "Aired At"
                                   }
                               },
                               Width = AdaptiveColumnWidth.Auto
                           },

                       }
                   }
                }
                });
            }

            response.Attachments.Add(new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = card,
            });

            return response;
        }

        public class ResponseIds
        {
            public const string SpotsAiredStatusCard = "spotsAiredStatusCard";
        }
    }
}
