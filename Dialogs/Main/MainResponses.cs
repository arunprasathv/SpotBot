﻿ 

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
        public const string Intro = "intro";
        public const string Greeting = "greeting";
        public const string Confused = "confused";
        public const string Goodbye = "goodbye";

        private static LanguageTemplateDictionary _responseTemplates = new LanguageTemplateDictionary
        {
            ["default"] = new TemplateIdMap
            {
                { Greeting, (context, data) => "Welcome Arun! In a few words, what do you need help with today? Not sure where to start, Type Help" },
                { Confused, (context, data) => "I'm sorry, I'm not able to help with that." },
                { Goodbye, (context, data) => "Of course, have a great Day." },
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

      
            return response;
        }

        
    }
}
