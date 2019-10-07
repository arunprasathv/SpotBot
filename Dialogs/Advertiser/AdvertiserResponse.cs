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
    public class AdvertiserResponse : TemplateManager
    {
        private static LanguageTemplateDictionary _responseTemplates = new LanguageTemplateDictionary
        {
            ["default"] = new TemplateIdMap
            {
                {ResponseIds.AdvertiserCard,(context,data)=>CreateAdvertiserCard(context,data) }
            },
            ["en"] = new TemplateIdMap { },
        };

        public AdvertiserResponse()
        {
            Register(new DictionaryRenderer(_responseTemplates));
        }

        private static IMessageActivity CreateAdvertiserCard(ITurnContext context, CAMAdvertiser advertiserDetails)
        {
            var response = context.Activity.CreateReply();

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
                           Width = "50",
                           Items = new List<AdaptiveElement>()
                           {
                                new AdaptiveTextBlock() { Text = "Advertiser Detail", Size = AdaptiveTextSize.Medium, Weight = AdaptiveTextWeight.Bolder },
                           },
                       },
                }
            });
            
            card.Body.Add(new AdaptiveFactSet()
            {
                Facts = new List<AdaptiveFact>()
                {
                    new AdaptiveFact("Account Id:", advertiserDetails.AccountID.ToString()),
                    new AdaptiveFact("Account Name:", advertiserDetails.AccountName.ToString()),
                    new AdaptiveFact("Account Status:", advertiserDetails.AccountStatus.ToString()),
                    //new AdaptiveFact(" ", string.Empty),
                    //new AdaptiveFact("Account Billing Information:",string.Empty),
                    //new AdaptiveFact("---------------------------", string.Empty),
                    //new AdaptiveFact(" ", string.Empty),
                    new AdaptiveFact("Contact Name:", advertiserDetails.ContactName.ToString()),
                    new AdaptiveFact("Contact Phone:", advertiserDetails.Phone.ToString()),
                    new AdaptiveFact("Contact Email:", advertiserDetails.Email.ToString()),
                    new AdaptiveFact("Address:", advertiserDetails.Address.ToString()),
                }
            });

            card.Body.Add(new AdaptiveContainer()
            {
               // Type = "ActionSet",
                Items = new List<AdaptiveElement>()
                {
                    new AdaptiveActionSet
                    {
                           Actions = new List<AdaptiveAction>()
                           {
                               new AdaptiveOpenUrlAction()
                               {
                                 Title = "View Billing Profile",
                                 Url = new Uri("https://omsspotlightqa.comcast.com/CAM/v3/#/traffic-id-requests/271310")
                               }
                           }
                    }
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
            public const string AdvertiserCard = "advertiserCard";
        }
    }
}
