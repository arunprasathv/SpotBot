﻿using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.TemplateManager;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.SpotBot
{
    public class OrderPerformanceResponses : TemplateManager
    {
        private static LanguageTemplateDictionary _responseTemplates = new LanguageTemplateDictionary
        {
            ["default"] = new TemplateIdMap
            {
                {ResponseIds.OrderPerformanceCard,(context,data)=>CreateOrderPerformanceCard(context,data) }
            },
            ["en"] = new TemplateIdMap { },
        };

        public OrderPerformanceResponses()
        {
            Register(new DictionaryRenderer(_responseTemplates));
        }

        public class ResponseIds
        {
            public const string OrderPerformanceCard = "orderPerformanceCard";
        }

        private static IMessageActivity CreateOrderPerformanceCard(ITurnContext context, OrderPerformance orderPerformance)
        {   
            var response = context.Activity.CreateReply();

            var card = new AdaptiveCard
            {
                Body = new List<AdaptiveElement>()
            };
            
            card.Body.Add(new AdaptiveContainer()
            {
                Items = new List<AdaptiveElement>()
                {
                    new AdaptiveTextBlock() { Text = "Order Performance", Size = AdaptiveTextSize.Medium, Weight = AdaptiveTextWeight.Bolder, Color = AdaptiveTextColor.Accent },
                },
            });

            card.Body.Add(new AdaptiveFactSet()
            {
                Facts = new List<AdaptiveFact>()
                {
                    new AdaptiveFact("Order #:", orderPerformance.OrderId.ToString()),
                    new AdaptiveFact("Reach:", orderPerformance.Reach.ToString()),
                    new AdaptiveFact("Frequency:", orderPerformance.Frequency.ToString()),
                    new AdaptiveFact("Average Ad Completion Rate:", orderPerformance.AverageAdCompletionRate.ToString()),
                    new AdaptiveFact("Cumulative Impression Count:", orderPerformance.CumulativeImpressionCount.ToString()),
                    new AdaptiveFact("Spot Count:",orderPerformance.SpotCount.ToString()),
                    new AdaptiveFact("Spot Count Aired:",orderPerformance.SpotCountAired.ToString())                    
                }
            });

            //Network Performance Information
            card.Body.Add(new AdaptiveColumnSet()
            {
                Columns = new List<AdaptiveColumn>()
                {
                    new AdaptiveColumn()
                    {
                        Spacing = AdaptiveSpacing.Medium,
                        Items = new List<AdaptiveElement>
                        {
                            new AdaptiveTextBlock()
                            {
                                Text = "Network Performance Information",
                                Weight = AdaptiveTextWeight.Bolder,
                                Wrap = true
                            }
                        },
                        Width = AdaptiveColumnWidth.Stretch
                    },

                    new AdaptiveColumn()
                    {
                        Id = "chevronDown1",
                        Spacing = AdaptiveSpacing.Small,
                        VerticalContentAlignment = AdaptiveVerticalContentAlignment.Center,
                        Items = new List<AdaptiveElement>
                        {
                            new AdaptiveImage()
                            {
                                Url = new Uri("https://adaptivecards.io/content/down.png"),
                                PixelWidth = 20,
                                AltText = "collapsed",
                                SelectAction = new AdaptiveToggleVisibilityAction()
                                {
                                   Title = "Collapase",
                                   TargetElements = new List<AdaptiveTargetElement>()
                                   {                                       
                                       "npContent1",
                                       "npContent2",
                                       "npContent3",
                                       "npContent4",
                                       "chevronUp1",
                                       "chevronDown1"
                                   }
                                }
                            }
                        },
                        Width = AdaptiveColumnWidth.Auto
                    },

                    new AdaptiveColumn()
                    {
                        Id = "chevronUp1",
                        IsVisible = false,
                        Spacing = AdaptiveSpacing.Small,
                        VerticalContentAlignment = AdaptiveVerticalContentAlignment.Center,
                        Items = new List<AdaptiveElement>
                        {
                            new AdaptiveImage()
                            {
                                Url = new Uri("https://adaptivecards.io/content/up.png"),
                                PixelWidth = 20,
                                AltText = "expanded",
                                SelectAction = new AdaptiveToggleVisibilityAction()
                                {
                                   Title = "expand",
                                   TargetElements = new List<AdaptiveTargetElement>()
                                   {
                                       "npContent1",
                                       "npContent2",
                                       "npContent3",
                                       "npContent4",
                                       "chevronUp1",
                                       "chevronDown1"
                                   }
                                }
                            }
                        },
                        Width = AdaptiveColumnWidth.Auto
                    }
                }                                
            });

            card.Body.Add(new AdaptiveContainer()
            {
                Id = "npContent1",
                IsVisible = false,                
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
                                       Text = "Abbreviation",
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
                                       Text = "Impression %",
                                       Color = AdaptiveTextColor.Accent,
                                       Wrap = true
                                   }
                               },
                               Width = "50"
                           }
                       }
                   }
                }
            });

            int i = 2;
            foreach (var item in orderPerformance.NetworkPerformance)
            {
                card.Body.Add(new AdaptiveContainer()
                {
                    Id = $"npContent{i++}",                              
                    IsVisible = false,                    
                    Items = new List<AdaptiveElement>()
                    {
                        new AdaptiveColumnSet()
                        {
                            Columns = new List<AdaptiveColumn>()
                            {
                                new AdaptiveColumn()
                                {
                                    Width = "5"
                                },

                                new AdaptiveColumn()
                                {
                                    Items = new List<AdaptiveElement>
                                    {
                                        new AdaptiveTextBlock()
                                        {
                                            Text = item.NetworkAbbreviation
                                        }
                                    },
                                    Width = "50"
                                },

                                new AdaptiveColumn()
                                {
                                    Items = new List<AdaptiveElement>
                                    {
                                        new AdaptiveTextBlock()
                                        {
                                            Text = item.NetworkImpressionPercentage.ToString()
                                        }
                                    },
                                    Width = "45"
                                }
                            }
                        }
                    }
                });
            }

            //Impressions Delivered Information
            card.Body.Add(new AdaptiveColumnSet()
            {
                Columns = new List<AdaptiveColumn>()
                {
                    new AdaptiveColumn()
                    {
                        Spacing = AdaptiveSpacing.Medium,
                        Items = new List<AdaptiveElement>
                        {
                            new AdaptiveTextBlock()
                            {
                                Text = "Impressions Delivered Information",
                                Weight = AdaptiveTextWeight.Bolder,
                                Wrap = true
                            }
                        },
                        Width = AdaptiveColumnWidth.Stretch
                    },

                    new AdaptiveColumn()
                    {
                        Id = "idDown1",
                        Spacing = AdaptiveSpacing.Small,
                        VerticalContentAlignment = AdaptiveVerticalContentAlignment.Center,
                        Items = new List<AdaptiveElement>
                        {
                            new AdaptiveImage()
                            {
                                Url = new Uri("https://adaptivecards.io/content/down.png"),
                                PixelWidth = 20,
                                AltText = "collapsed",
                                SelectAction = new AdaptiveToggleVisibilityAction()
                                {
                                   Title = "Collapase",
                                   TargetElements = new List<AdaptiveTargetElement>()
                                   {
                                       "idContent1",
                                       "idContent2",
                                       "idContent3",
                                       "idContent4",
                                       "idUp1",
                                       "idDown1"
                                   }
                                }
                            }
                        },
                        Width = AdaptiveColumnWidth.Auto
                    },

                    new AdaptiveColumn()
                    {
                        Id = "idUp1",
                        IsVisible = false,
                        Spacing = AdaptiveSpacing.Small,
                        VerticalContentAlignment = AdaptiveVerticalContentAlignment.Center,
                        Items = new List<AdaptiveElement>
                        {
                            new AdaptiveImage()
                            {
                                Url = new Uri("https://adaptivecards.io/content/up.png"),
                                PixelWidth = 20,
                                AltText = "expanded",
                                SelectAction = new AdaptiveToggleVisibilityAction()
                                {
                                   Title = "expand",
                                   TargetElements = new List<AdaptiveTargetElement>()
                                   {
                                       "idContent1",
                                       "idContent2",
                                       "idContent3",
                                       "idContent4",
                                       "idUp1",
                                       "idDown1"
                                   }
                                }
                            }
                        },
                        Width = AdaptiveColumnWidth.Auto
                    }
                }
            });

            card.Body.Add(new AdaptiveContainer()
            {
                Id = "idContent1",
                IsVisible = false,
                Style = AdaptiveContainerStyle.Emphasis,
                Items = new List<AdaptiveElement>()
                {
                   new AdaptiveColumnSet()
                   {
                       Columns = new List<AdaptiveColumn>()
                       {
                           new AdaptiveColumn()
                           {

                           },

                           new AdaptiveColumn()
                           {
                               Items = new List<AdaptiveElement>()
                               {
                                   new AdaptiveTextBlock()
                                   {
                                       Weight = AdaptiveTextWeight.Bolder,
                                       Text = "Air Date",
                                       Color = AdaptiveTextColor.Accent,
                                       Wrap = true
                                   }
                               },
                               Width = "40"
                           },

                           new AdaptiveColumn()
                           {
                               Items = new List<AdaptiveElement>()
                               {
                                   new AdaptiveTextBlock()
                                   {
                                       Weight = AdaptiveTextWeight.Bolder,
                                       Text = "Daily Count",
                                       Color = AdaptiveTextColor.Accent,
                                       Wrap = true
                                   }
                               },
                               Width = "30"
                           },

                           new AdaptiveColumn()
                           {
                               Items = new List<AdaptiveElement>()
                               {
                                   new AdaptiveTextBlock()
                                   {
                                       Weight = AdaptiveTextWeight.Bolder,
                                       Text = "Cumulative Count",
                                       Color = AdaptiveTextColor.Accent,
                                       Wrap = true
                                   }
                               },
                               Width = "30"
                           }

                       }
                   }
                }
            });

            int j = 2;
            foreach (var item in orderPerformance.ImpressionsDelivered)
            {
                card.Body.Add(new AdaptiveContainer()
                {
                    Id = $"idContent{j++}",
                    IsVisible = false,
                    Style = AdaptiveContainerStyle.Default,
                    Items = new List<AdaptiveElement>()
                    {
                        new AdaptiveColumnSet()
                        {
                            Columns = new List<AdaptiveColumn>()
                            {
                                new AdaptiveColumn()
                                {
                                    Width = "5"
                                },

                                new AdaptiveColumn()
                                {
                                    Items = new List<AdaptiveElement>
                                    {
                                        new AdaptiveTextBlock()
                                        {
                                            Text = item.AirDate.ToShortDateString()
                                        }
                                    },
                                    Width = "40"
                                },

                                new AdaptiveColumn()
                                {
                                    Items = new List<AdaptiveElement>
                                    {
                                        new AdaptiveTextBlock()
                                        {
                                            Text = item.DailyImpressionCount.ToString()
                                        }
                                    },
                                    Width = "30"
                                },

                                new AdaptiveColumn()
                                {
                                    Items = new List<AdaptiveElement>
                                    {
                                        new AdaptiveTextBlock()
                                        {
                                            Text = item.CumulativeImpressionCount.ToString()
                                        }
                                    },
                                    Width = "30"
                                }
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
    }
}
