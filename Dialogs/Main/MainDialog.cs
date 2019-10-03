 

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
 using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Hackathon.SpotBot;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Microsoft.Bot.Schema;

namespace Hackathon.SpotBot
{
    public class MainDialog : ComponentDialog
    {
        private IBotServices _services;
        private UserState _userState;
        private ConversationState _conversationState;
        private IStatePropertyAccessor<CustomerSupportTemplateState> _stateAccessor;
        private MainResponses _responder = new MainResponses();
        private readonly BotStateService _botStateService;

        public MainDialog(BotStateService botStateService,IBotServices services, ConversationState conversationState, UserState userState)
            : base(nameof(MainDialog))
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _conversationState = conversationState;
            _userState = userState;
            _stateAccessor = _conversationState.CreateProperty<CustomerSupportTemplateState>(nameof(CustomerSupportTemplateState));
            _botStateService = botStateService ?? throw new System.ArgumentNullException(nameof(botStateService));
            InitializeWaterfallDialog();

            RegisterDialogs();
        }

        private void InitializeWaterfallDialog()
        {
            // Create Waterfall Steps
            var waterfallSteps = new WaterfallStep[]
            {
                InitialStepAsync,
                FinalStepAsync
            };

            // Add Named Dialogs
        //    AddDialog(new GreetingDialog($"{nameof(MainDialog)}.greeting", _botStateService));
          //  AddDialog(new BugReportDialog($"{nameof(MainDialog)}.bugReport", _botStateService));

            AddDialog(new WaterfallDialog($"{nameof(MainDialog)}.mainFlow", waterfallSteps));

            // Set the starting Dialog
            InitialDialogId = $"{nameof(MainDialog)}.mainFlow";
        }
         
 
        private async Task<DialogTurnResult> InitialStepAsync(DialogContext stepContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            var routeResult = EndOfTurn;

            // Check dispatch result
            var dispatchResult = await _services.Dispatch.RecognizeAsync(stepContext.Context, CancellationToken.None);
            // var topIntent = dispatchResult.GetTopScoringIntent();
            var luisResult = dispatchResult.Properties["luisResult"] as LuisResult;

            var result = luisResult.ConnectedServiceResult;
            var topIntent = result.TopScoringIntent.Intent;


            if (topIntent == "Order Details")
            {
                 return await stepContext.BeginDialogAsync($"{nameof(MainDialog)}.Order", null, cancellationToken);
                //routeResult = await dc.BeginDialogAsync(nameof(CheckOrderStatusDialog));
            }
            else
            {
                return await stepContext.BeginDialogAsync($"{nameof(MainDialog)}.Order", null, cancellationToken);
            }
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await _responder.ReplyWith(stepContext.Context, "What else can I help you with?");
            return await stepContext.EndDialogAsync(null, cancellationToken);
        }
        //protected override async Task RouteAsync(DialogContext dc, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    var routeResult = EndOfTurn;
            
        //    // Check dispatch result
        //    var dispatchResult = await _services.Dispatch.RecognizeAsync(dc.Context,  CancellationToken.None);
        //    // var topIntent = dispatchResult.GetTopScoringIntent();
        //   var luisResult= dispatchResult.Properties["luisResult"] as LuisResult;

        //    var result = luisResult.ConnectedServiceResult;
        //    var topIntent = result.TopScoringIntent.Intent;


        //    if (topIntent=="Order Details")
        //    {
        //        // return await stepContext.BeginDialogAsync($"{nameof(MainDialog)}.greeting", null, cancellationToken);

        //        routeResult=await dc.BeginDialogAsync(nameof(CheckOrderStatusDialog));
        //    }
        //    if (routeResult.Status == DialogTurnStatus.Complete)
        //    {
        //        await CompleteAsync(dc);
        //    }
        //}

        //protected override async Task CompleteAsync(DialogContext innerDc, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    // The active dialog's stack ended with a complete status
        //    await _responder.ReplyWith(innerDc.Context, "What else can I help you with?");
        //}

        private void RegisterDialogs()
        {
            //$"{nameof(MainDialog)}.greeting"
            // Add Named Dialogs
            //AddDialog(new GreetingDialog($"{nameof(MainDialog)}.greeting", _botStateService));
            //AddDialog(new BugReportDialog($"{nameof(MainDialog)}.bugReport", _botStateService));

            AddDialog(new CheckOrderStatusDialog($"{nameof(MainDialog)}.Order", _botStateService,_services));
        }
    }
}
