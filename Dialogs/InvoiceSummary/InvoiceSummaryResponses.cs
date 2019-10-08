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
    public class InvoiceSummaryResponses: TemplateManager
    {
        private static LanguageTemplateDictionary _responseTemplates = new LanguageTemplateDictionary
        {
            ["default"] = new TemplateIdMap
            {
                {ResponseIds.InvoiceSummaryCard,(context,data) => CreateInvoiceSummaryCard(context,data) }
            },
            ["en"] = new TemplateIdMap { },
        };

        public InvoiceSummaryResponses()
        {
            Register(new DictionaryRenderer(_responseTemplates));
        }

        public class ResponseIds
        {
            public const string InvoiceSummaryCard = "invoiceSummaryCard";
        }

        private static IMessageActivity CreateInvoiceSummaryCard(ITurnContext context, List<InvoiceSummary> invoiceSummary)
        {
            var response = context.Activity.CreateReply();

            var card = new AdaptiveCard
            {
                Body = new List<AdaptiveElement>()
            };

            if (invoiceSummary.Count == 0)
            {
                card.Body.Add(new AdaptiveContainer()
                {
                    Items = new List<AdaptiveElement>()
                    {
                        new AdaptiveTextBlock() { Text = "No invoice data found for given advertiser id and broadcast month", Size = AdaptiveTextSize.Small, Weight = AdaptiveTextWeight.Bolder, Color = AdaptiveTextColor.Attention },
                    },
                });
            }
            else
            {
                card.Body.Add(new AdaptiveContainer()
                {
                    Items = new List<AdaptiveElement>()
                    {
                        new AdaptiveTextBlock() { Text = "Invoice Summary", Size = AdaptiveTextSize.Medium, Weight = AdaptiveTextWeight.Bolder, Color = AdaptiveTextColor.Accent },
                    },
                });

                card.Body.Add(new AdaptiveFactSet()
                {
                    Facts = new List<AdaptiveFact>()
                    {
                        new AdaptiveFact("Advertiser ID:", invoiceSummary.First().AdvertiserCode),
                        new AdaptiveFact("Advertiser Name:", invoiceSummary.First().AdvertiserName),
                        new AdaptiveFact("Broadcast Month:", invoiceSummary.First().FormattedBroadcaseMonth),
                        new AdaptiveFact("Total Invoices:", invoiceSummary.Count().ToString()),
                        new AdaptiveFact("Total Invoice Amount:", invoiceSummary.Sum(i=>i.InvoiceActualBalance).Value.ToString("C")),
                        new AdaptiveFact("Total Invoice Amount Due:",invoiceSummary.Sum(i=>i.InvoiceCurrentBalance).Value.ToString("C")),
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
                                    Text = "Division",
                                    Color = AdaptiveTextColor.Accent,
                                    Wrap = true
                                }
                            },
                            Width = "20"
                         },
                         new AdaptiveColumn()
                         {
                            Items = new List<AdaptiveElement>()
                            {
                                new AdaptiveTextBlock()
                                {
                                    Weight = AdaptiveTextWeight.Bolder,
                                    Text = "Region",
                                    Color = AdaptiveTextColor.Accent,
                                    Wrap = true
                                }
                            },
                            Width = "20"
                         },
                         new AdaptiveColumn()
                         {
                            Items = new List<AdaptiveElement>()
                            {
                                new AdaptiveTextBlock()
                                {
                                    Weight = AdaptiveTextWeight.Bolder,
                                    Text = "Invoice#",
                                    Color = AdaptiveTextColor.Accent,
                                    Wrap = true
                                }
                            },
                            Width = "20"
                         },
                         new AdaptiveColumn()
                         {
                            Items = new List<AdaptiveElement>()
                            {
                                new AdaptiveTextBlock()
                                {
                                    Weight = AdaptiveTextWeight.Bolder,
                                    Text = "Invoice Amount",
                                    Color = AdaptiveTextColor.Accent,
                                    Wrap = true
                                }
                            },
                            Width = "20"
                         },
                         new AdaptiveColumn()
                         {
                            Items = new List<AdaptiveElement>()
                            {
                                new AdaptiveTextBlock()
                                {
                                    Weight = AdaptiveTextWeight.Bolder,
                                    Text = "Invoice Due",
                                    Color = AdaptiveTextColor.Accent,
                                    Wrap = true
                                }
                            },
                            Width = "20"
                         }
                    }
                });

                foreach (var invoice in invoiceSummary)
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
                                        Text = invoice.Division,
                                        Wrap = true
                                    }
                                },
                                Width = "20"
                             },
                             new AdaptiveColumn()
                             {
                                Items = new List<AdaptiveElement>()
                                {
                                    new AdaptiveTextBlock()
                                    {
                                        Text = invoice.Region,
                                        Wrap = true
                                    }
                                },
                                Width = "20"
                             },
                             new AdaptiveColumn()
                             {
                                Items = new List<AdaptiveElement>()
                                {
                                    new AdaptiveTextBlock()
                                    {
                                        Text = invoice.InvoiceId,
                                        Wrap = true
                                    }
                                },
                                Width = "20"
                             },
                             new AdaptiveColumn()
                             {
                                Items = new List<AdaptiveElement>()
                                {
                                    new AdaptiveTextBlock()
                                    {
                                        Text = invoice.InvoiceActualBalance.Value.ToString("C"),
                                        Wrap = true
                                    }
                                },
                                Width = "20"
                             },
                             new AdaptiveColumn()
                             {
                                Items = new List<AdaptiveElement>()
                                {
                                    new AdaptiveTextBlock()
                                    {
                                        Text = invoice.InvoiceCurrentBalance.Value.ToString("C"),
                                        Wrap = true
                                    }
                                },
                                Width = "20"
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
