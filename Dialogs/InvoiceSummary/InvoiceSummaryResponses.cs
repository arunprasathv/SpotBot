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

            card.Body.Add(new AdaptiveContainer()
            {
                Items = new List<AdaptiveElement>()
                {
                    new AdaptiveTextBlock() { Text = "Invoice Summary", Size = AdaptiveTextSize.Medium, Weight = AdaptiveTextWeight.Bolder, Color = AdaptiveTextColor.Accent },
                },
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
