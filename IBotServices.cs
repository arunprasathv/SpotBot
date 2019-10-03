 

 using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading;
using System.Threading.Tasks;
namespace  Hackathon.SpotBot

{
    public interface IBotServices
    {
        LuisRecognizer Dispatch { get; }
        // Task<T> RecognizeAsync<T>(DialogContext dialogContext, bool logOriginalMessage, CancellationToken cancellationToken = default(CancellationToken));
    }
}
