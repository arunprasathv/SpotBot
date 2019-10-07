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
    public class PaymentResponses : TemplateManager
    {
        private static LanguageTemplateDictionary _responseTemplates = new LanguageTemplateDictionary
        {
            ["default"] = new TemplateIdMap
            {
                {ResponseIds.PaymentStatusCard,(context,data)=>CreatePaymentStatusCard(context,data) }
            },
            ["en"] = new TemplateIdMap { },
        };

        public PaymentResponses()
        {
            Register(new DictionaryRenderer(_responseTemplates));
        }

        private static IMessageActivity CreatePaymentStatusCard(ITurnContext context, Payment paymentDetails)
        {
            var response = context.Activity.CreateReply();
            var imageUrl = "";
            if (paymentDetails.CardName == "Visa") {
                imageUrl = "C:\\SpotBot\\Images\\Visa.png";
            }
            else if(paymentDetails.CardName.Contains("Amex"))
            {
                imageUrl = "C:\\SpotBot\\Images\\Amex.jpg";
            }
            else if (paymentDetails.CardName.Contains("Discover"))
            {
                imageUrl = "C:\\SpotBot\\Images\\Discover.png";
            }
            var card = new AdaptiveCard
            {
                
                Body = new List<AdaptiveElement>()
            };

            //card.Body.Add(new AdaptiveContainer()
            //{
                
            //    Items = new List<AdaptiveElement>()
            //    {
            //        new AdaptiveTextBlock() { Text = "Payment Details", Size = AdaptiveTextSize.Medium, Weight = AdaptiveTextWeight.Bolder },
            //    },
          //  });
            card.Body.Add(new AdaptiveColumnSet()
            {
                Columns = new List<AdaptiveColumn>()
                {
                                    new AdaptiveColumn()
                       {
                           Width = "10",
                           Items = new List<AdaptiveElement>()
                {
                    new AdaptiveTextBlock() { Text = "Payment Details", Color= AdaptiveTextColor.Accent , Size = AdaptiveTextSize.Medium, Weight = AdaptiveTextWeight.Bolder },
                },
                   },
                   new AdaptiveColumn()
                       {
                           Width = "10",
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
            }
            
             );
            card.Body.Add(new AdaptiveFactSet()
            {
                Facts = new List<AdaptiveFact>()
                {
                    new AdaptiveFact("Order #:", paymentDetails.OrderNumber.ToString()),
                    new AdaptiveFact("Order GUID:", paymentDetails.OrderGuid),
                    new AdaptiveFact("Payment Amount:", paymentDetails.Amount.ToString("C")),
                    new AdaptiveFact("Payment Status", paymentDetails.Decision),
                   // new AdaptiveFact("Card Name", paymentDetails.CardName),
                    new AdaptiveFact("Card Number",paymentDetails.CardNumber??"1234"),
                    new AdaptiveFact("Payment Date",paymentDetails.DateTimeUtc.Date.ToString()),
                    new AdaptiveFact("Message",paymentDetails.Message)
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
            public const string PaymentStatusCard = "paymentStatusCard";
        }
    }
}
