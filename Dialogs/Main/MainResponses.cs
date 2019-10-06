 

using System.Collections.Generic;
using System.IO;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.TemplateManager;

namespace Hackathon.SpotBot
{
    public class MainResponses : TemplateManager
    {
        // Constants
        public const string Help = "help";
        public const string Intro = "intro";
        public const string Greeting = "greeting";
        public const string Confused = "confused";

        private static LanguageTemplateDictionary _responseTemplates = new LanguageTemplateDictionary
        {
            ["default"] = new TemplateIdMap
            {
                { Greeting, (context, data) => "Hi there!" },
                { Confused, (context, data) => "I'm sorry, I'm not able to help with that." },
                { Help, (context, data) => SendHelpCard(context, data) },
                { Intro, (context, data) => SendIntroCard(context, data) },
            },
            
            ["en"] = new TemplateIdMap { },
            ["fr"] = new TemplateIdMap { },
        };

        public MainResponses()
        {
            Register(new DictionaryRenderer(_responseTemplates));
        }

        public static IMessageActivity SendIntroCard(ITurnContext turnContext, dynamic data)
        {
            var response = turnContext.Activity.CreateReply();

            var introCard = File.ReadAllText(@".\Dialogs\Main\Resources\IntroCard.json");

            response.Attachments = new List<Attachment>
            {
                new Attachment()
                {
                    ContentType = "application/vnd.microsoft.card.adaptive",
                    Content = JsonConvert.DeserializeObject(introCard),
                }
            };

            //response.SuggestedActions = new SuggestedActions()
            //{
            //    Actions = new List<CardAction>()
            //    {
            //         new CardAction(ActionTypes.ImBack, "Check order status", value:  "Check order status"),
            //        new CardAction(ActionTypes.ImBack, "Check return status", value:  "Check return status"),
            //    }
            //};

            return response;
        }

        public static IMessageActivity SendHelpCard(ITurnContext turnContext, dynamic data)
        {
            var response = turnContext.Activity.CreateReply();

            var helpCard = File.ReadAllText(@".\Dialogs\Main\Resources\HelpCard.json");
            response.Attachments = new List<Attachment>
            {
                new Attachment()
                {
                    ContentType = "application/vnd.microsoft.card.adaptive",
                    Content = JsonConvert.DeserializeObject(helpCard),
                }
            };

            response.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                {
                    
                    new CardAction(ActionTypes.ImBack, "Check order status", value:  "Check order status"),
                    new CardAction(ActionTypes.ImBack, "Check return status", value:  "Check return status"),
                    
                }
            };

            return response;
        }
    }
}
