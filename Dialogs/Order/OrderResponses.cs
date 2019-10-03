using AdaptiveCards;
//using .Dialogs.Orders.Resources;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.TemplateManager;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SpotBot.Dialogs.Order.Resources;

namespace Hackathon.SpotBot
{
    public class OrderResponses : TemplateManager
    {
        private static LanguageTemplateDictionary _responseTemplates = new LanguageTemplateDictionary
        {
            ["default"] = new TemplateIdMap
            {
                { ResponseIds.OrderNumberPrompt, (context, data) => OrderStrings.OrderNumberPrompt },
                { ResponseIds.OrderNumberReprompt, (context, data) => OrderStrings.OrderNumberReprompt },
                { ResponseIds.PhoneNumberPrompt, (context, data) => OrderStrings.PhoneNumberPrompt },
                { ResponseIds.PhoneNumberReprompt, (context, data) => OrderStrings.PhoneNumberReprompt },
                { ResponseIds.OrderStatusCard, (context, data) => CreateOrderStatusCard(context, data) }
               
            },
            ["en"] = new TemplateIdMap { },
        };

        public OrderResponses()
        {
            Register(new DictionaryRenderer(_responseTemplates));
        }

      

        private static IMessageActivity CreateOrderStatusCard(ITurnContext context, dynamic data)
        {
            
            var order = data as Order;
            order.Id = "3423$";
            order.Items = new List<Product>()
                {
                    new Product()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Xbox One X 1TB Console",
                        Url = "https://www.microsoft.com/en-us/p/xbox-one-x-1tb-console/8mp3mpj68b7v",
                        ImageUrl = "https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RWbGIz?ver=8530&q=90&m=6&h=423&w=752&b=%23FF171717&f=jpg&o=f&aim=true",
                        Price = 499.00
                    },
                    new Product()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Xbox Wireless Controller - Black",
                        Url = "https://www.microsoft.com/en-us/p/xbox-wireless-controller/8vcw8gln9vrf/ljvk?cid=msft_web_collection&activetab=pivot%3aoverviewtab",
                        ImageUrl = "https://compass-ssl.xbox.com/assets/2b/aa/2baa2c8b-4d4d-4f07-bba0-cebcd06de801.jpg?n=X1-Wireless-Controller-Black_Content-Placement-0_Accessory-Hub_740x417.jpg",
                        Price = 49.99
                    },
                };
            order.Status = OrderStatus.Shipped;
            order.ShippingProvider = "Fed Ex";
            order.TrackingNumber = "asdf";
            order.TrackingLink = "";
            order.Subtotal = 499.00;
            order.Tax = 49.99;
            order.Total = 548.99;
            order.Phone = "1(206)555-1234";
            order.Status = OrderStatus.Shipped;
            order.ShippingProvider = "USPS";
            order.TrackingNumber = "123456789";
            order.TrackingLink = "https://tools.usps.com/go/TrackConfirmAction";
            var response = context.Activity.CreateReply();
            response.Attachments = new List<Attachment>();

            var card = new AdaptiveCard
            {
                Body = new List<AdaptiveElement>()
            };

            card.Body.Add(new AdaptiveContainer()
            {
                Items = new List<AdaptiveElement>()
                {
                    new AdaptiveTextBlock() { Text = "Order Status", Size = AdaptiveTextSize.Medium, Weight = AdaptiveTextWeight.Bolder },
                },
            });

            foreach (var group in order.Items.GroupBy(i => i.Id))
            {
                card.Body.Add(new AdaptiveColumnSet()
                {
                    Columns = new List<AdaptiveColumn>()
                    {
                        new AdaptiveColumn()
                        {
                            Width = "30",
                            Items = new List<AdaptiveElement>()
                            {
                                new AdaptiveImage()
                                {
                                    Url = new Uri(group.First().ImageUrl),
                                },
                            },
                        },
                        new AdaptiveColumn()
                        {
                            Width = "50",
                            Items = new List<AdaptiveElement>()
                            {
                                new AdaptiveTextBlock(){ Text = group.First().Name, Weight = AdaptiveTextWeight.Bolder, Wrap = true },
                                new AdaptiveFactSet()
                                {
                                    Spacing = AdaptiveSpacing.Small,
                                    Facts = new List<AdaptiveFact>()
                                    {
                                        new AdaptiveFact("Price", string.Format("{0:C}", group.First().Price)),
                                        new AdaptiveFact("Quantity", group.Count().ToString()),
                                    }
                                },
                            },
                        },
                    }
                });
            }

            card.Body.Add(new AdaptiveFactSet()
            {
                Facts = new List<AdaptiveFact>()
                {
                    new AdaptiveFact("Order #:", order.Id),
                    new AdaptiveFact("Status:", order.Status.ToString()),
                    new AdaptiveFact("Shipping Provider:", order.ShippingProvider),
                    new AdaptiveFact("Tracking Number", $"[{order.TrackingNumber}]({order.TrackingLink})"),
                }
            });

            response.Attachments.Add(new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = card,
            });

            return response;
        }

        private static IMessageActivity CreateAdaptiveCardResponse(ITurnContext context, dynamic data, string path)
        {
            var response = context.Activity.CreateReply();

            var introCard = File.ReadAllText(path);

            response.Attachments = new List<Attachment>
            {
                new Attachment()
                {
                    ContentType = "application/vnd.microsoft.card.adaptive",
                    Content = JsonConvert.DeserializeObject(introCard),
                }
            };

            return response;
        }

        public class ResponseIds
        {
            public const string CancelOrderPolicyCard = "cancelOrderPolicyCard";
            public const string CancelOrderPrompt = "cancelOrderPrompt";
            public const string CancelTypePrompt = "cancelTypePrompt";
            public const string OrderNumberPrompt = "cancelOrderNumberPrompt";
            public const string OrderNumberReprompt = "cancelOrderNumberReprompt";
            public const string PhoneNumberPrompt = "cancelOrderPhonePrompt";
            public const string PhoneNumberReprompt = "cancelOrderPhoneReprompt";
            public const string CancelOrderSuccessMessage = "cancelOrderSuccessMessage";
            public const string OrderStatusCard = "orderStatusCard";
            public const string OrderStatusMessage = "orderStatusMessage";
            public const string CurrentPromosMessage = "currentPromosMessage";
            public const string CurrentPromosCard = "currentPromos";
            public const string FindPromosForCartMessage = "findPromosForCartMessage";
            public const string CartIdPrompt = "cartIdPrompt";
            public const string CartIdReprompt = "cartIdReprompt";
            public const string FoundPromosMessage = "foundPromosMessage";
        }
    }
}