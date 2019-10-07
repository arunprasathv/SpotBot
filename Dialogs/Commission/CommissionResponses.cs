using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.TemplateManager;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotBot.Models;

namespace Hackathon.SpotBot
{
    public class CommissionResponses : TemplateManager
    {
        private static LanguageTemplateDictionary _responseTemplates = new LanguageTemplateDictionary
        {
            ["default"] = new TemplateIdMap
            {
                {ResponseIds.CommissionCard,(context,data)=>CreateCommissionCard(context,data) }
            },
            ["en"] = new TemplateIdMap { },
        };

        public CommissionResponses()
        {
            Register(new DictionaryRenderer(_responseTemplates));
        }

        private static IMessageActivity CreateCommissionCard(ITurnContext context, Commission commissionDetails)
        {
            var response = context.Activity.CreateReply();
            var imageUrl = "C:\\SpotBot\\Images\\commission.jpg";

            var card = new AdaptiveCard
            {
                
                Body = new List<AdaptiveElement>()
            };


            card.Body.Add(new AdaptiveColumnSet()
            {
                Columns = new List<AdaptiveColumn>()
                {
                                    new AdaptiveColumn()
                       {
                           Width = "15",
                           Items = new List<AdaptiveElement>()
                {
                    new AdaptiveTextBlock() { Text = "Commission Summary", Size = AdaptiveTextSize.Medium, Weight = AdaptiveTextWeight.Bolder },
                },
                   },
                   new AdaptiveColumn()
                       {
                           Width = "15",
                           Items = new List<AdaptiveElement>()
                           {
                               new AdaptiveImage()
                               {

                                   Url = new Uri(imageUrl),
                                  Size = AdaptiveImageSize.Small
                               }
                           }
                   }
                }
            });
            
            card.Body.Add(new AdaptiveFactSet()
            {
                Facts = new List<AdaptiveFact>()
                {
                    new AdaptiveFact("Total Monthly Payment:", commissionDetails.TotalMonthlyPayout.ToString("C")),
                    new AdaptiveFact("YTD Commission Paid:", commissionDetails.YTDCommissionsPaid.ToString("C")),
                    new AdaptiveFact("Revenue Adjustments:", commissionDetails.RevenueAdjustments.ToString("C")),
                    new AdaptiveFact("Charge Backs:",commissionDetails.ChargeBacks.ToString("C"))
                }
            });

            response.Attachments.Add(new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = card,
            });
            
            return response;
        }

        public class ResponseIds
        {
            public const string CommissionCard = "commissionCard";
        }
    }
}
