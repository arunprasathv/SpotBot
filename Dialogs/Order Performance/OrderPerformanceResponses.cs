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
                    new AdaptiveTextBlock() { Text = "Order Performance", Size = AdaptiveTextSize.Medium, Weight = AdaptiveTextWeight.Bolder },
                },
            });

            card.Body.Add(new AdaptiveFactSet()
            {
                Facts = new List<AdaptiveFact>()
                {
                    new AdaptiveFact("Order #:", orderPerformance.OrderId.ToString()),
                    new AdaptiveFact("Reach:", orderPerformance.Reach.ToString()),
                    new AdaptiveFact("Frequency:", orderPerformance.Frequency.ToString()),
                    new AdaptiveFact("AverageAdCompletionRate", orderPerformance.AverageAdCompletionRate.ToString()),
                    new AdaptiveFact("CumulativeImpressionCount", orderPerformance.CumulativeImpressionCount.ToString()),
                    new AdaptiveFact("SpotCount",orderPerformance.SpotCount.ToString()),
                    new AdaptiveFact("SpotCountAired",orderPerformance.SpotCountAired.ToString())                    
                }
            });

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
                                       "cardContent1",
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
                                       "cardContent1",
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
                                       Text = "Network Abbreviation"
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
                                       Text = "Network Impression Percentage"
                                   }
                               },
                               Width = AdaptiveColumnWidth.Auto
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
    }
}