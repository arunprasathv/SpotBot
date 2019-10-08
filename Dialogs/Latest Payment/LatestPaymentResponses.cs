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
    public class LatestPaymentResponses : TemplateManager
    {
        private static LanguageTemplateDictionary _responseTemplates = new LanguageTemplateDictionary
        {
            ["default"] = new TemplateIdMap
            {
                {ResponseIds.LatestPaymentCard,(context,data) => CreateLatestPaymentCard(context,data) }
            },
            ["en"] = new TemplateIdMap { },
        };

        public LatestPaymentResponses()
        {
            Register(new DictionaryRenderer(_responseTemplates));
        }

        public class ResponseIds
        {
            public const string LatestPaymentCard = "latestPaymentCard";
        }

        private static IMessageActivity CreateLatestPaymentCard(ITurnContext context, LatestPayemntInfo latestPayemntInfo)
        {
            var response = context.Activity.CreateReply();

            var card = new AdaptiveCard
            {
                Body = new List<AdaptiveElement>()
            };

            if (latestPayemntInfo == null)
            {
                card.Body.Add(new AdaptiveContainer()
                {
                    Items = new List<AdaptiveElement>()
                    {
                        new AdaptiveTextBlock() { Text = "No payment data found for given advertiser id", Size = AdaptiveTextSize.Small, Weight = AdaptiveTextWeight.Bolder, Color = AdaptiveTextColor.Attention },
                    },
                });
            }
            else
            {
                card.Body.Add(new AdaptiveContainer()
                {
                    Items = new List<AdaptiveElement>()
                    {
                        new AdaptiveTextBlock() { Text = "CAP Payment Information", Size = AdaptiveTextSize.Medium, Weight = AdaptiveTextWeight.Bolder, Color = AdaptiveTextColor.Accent },
                    },
                });

                card.Body.Add(new AdaptiveFactSet()
                {
                    Facts = new List<AdaptiveFact>()
                    {
                        new AdaptiveFact("Division:", latestPayemntInfo.Division),
                        new AdaptiveFact("Region:", latestPayemntInfo.Region),
                        new AdaptiveFact("Advertiser ID:", latestPayemntInfo.AdvertiserCode),
                        new AdaptiveFact("Advertiser Name:", latestPayemntInfo.AdvertiserName),
                        new AdaptiveFact("Payment Type:", latestPayemntInfo.PaymentType),
                        new AdaptiveFact("Tender #:", latestPayemntInfo.Tender),
                        new AdaptiveFact("Payment Amount:", latestPayemntInfo.PaymentAmount),
                        new AdaptiveFact("Payment Status:", latestPayemntInfo.PaymentStatus),
                        new AdaptiveFact("Confirmation #:", latestPayemntInfo.ConfirmationNumber),
                        new AdaptiveFact("Paid By:", latestPayemntInfo.UserPaid),
                    }
                });

                card.Body.Add(new AdaptiveColumnSet()
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
                                    Text = "Invoice #",
                                    Color = AdaptiveTextColor.Accent,
                                    Wrap = true
                                }
                            },
                            Width = "50"
                         },
                         new AdaptiveColumn()
                         {
                            Items = new List<AdaptiveElement>()
                            {
                                new AdaptiveTextBlock()
                                {
                                    Weight = AdaptiveTextWeight.Bolder,
                                    Text = "Amount",
                                    Color = AdaptiveTextColor.Accent,
                                    Wrap = true
                                }
                            },
                            Width = "50"
                         }
                    }
                });

                foreach(var invoice in latestPayemntInfo.Invoices)
                {
                    card.Body.Add(new AdaptiveColumnSet()
                    {
                        Columns = new List<AdaptiveColumn>()
                        {
                             new AdaptiveColumn()
                             {
                                Items = new List<AdaptiveElement>()
                                {
                                    new AdaptiveTextBlock()
                                    {
                                        Text = invoice.InvoiceNumber,
                                        Wrap = true
                                    }
                                },
                                Width = "50"
                             },
                             new AdaptiveColumn()
                             {
                                Items = new List<AdaptiveElement>()
                                {
                                    new AdaptiveTextBlock()
                                    {
                                        Text = invoice.InvoiceAmount,
                                        Wrap = true
                                    }
                                },
                                Width = "50"
                             }
                        }
                    });
                }
            }

            response.Attachments.Add(new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = card,
            });

            return response;
        }

    }
}
