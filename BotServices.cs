 

using System;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Configuration;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace Hackathon.SpotBot
{
    public class BotServices : IBotServices
    {
        public BotServices(IConfiguration configuration)
        {
            // Read the setting for cognitive services (LUIS, QnA) from the appsettings.json
            Dispatch = new LuisRecognizer(new LuisApplication(
                configuration["LuisAppId"],
                configuration["LuisAPIKey"],
                $"https://{configuration["LuisAPIHostName"]}.api.cognitive.microsoft.com"),
                new LuisPredictionOptions { IncludeAllIntents = true, IncludeInstanceData = true },
                true);

           
        }
        public async Task<T> RecognizeAsync<T>(DialogContext dialogContext,  CancellationToken cancellationToken = default(CancellationToken))
        where T : IRecognizerConvert, new()
        {
            var result = new T();
            result.Convert(await RecognizeAsync(dialogContext, cancellationToken).ConfigureAwait(false));
            return result;
        }
        public async Task<T> RecognizeAsync<T>(ITurnContext turnContext,  CancellationToken cancellationToken = default(CancellationToken))
            where T : IRecognizerConvert, new()
        {
            var result = new T();
            result.Convert(await RecognizeAsync(turnContext, cancellationToken).ConfigureAwait(false));
            return result;
        }
        public async Task<RecognizerResult> RecognizeAsync(DialogContext dialogContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (dialogContext == null)
            {
                throw new ArgumentNullException(nameof(dialogContext));
            }
            return await RecognizeInternalAsync(dialogContext.Context, cancellationToken);
        }
        public async Task<RecognizerResult> RecognizeAsync(ITurnContext context,  CancellationToken cancellationToken = default(CancellationToken))
        {
            return await RecognizeInternalAsync(context, cancellationToken);
        }
        private async Task<RecognizerResult> RecognizeInternalAsync(ITurnContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Call Luis Recognizer
            
            var recognizerResult = await Dispatch.RecognizeAsync(context, cancellationToken);
            return recognizerResult;

        }
        public LuisRecognizer Dispatch { get; private set; }
        
    }
}
