using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Schema;
using System.Collections.Generic;

namespace Hackathon.SpotBot
{
    /// <summary>
    /// Main entry point and orchestration for bot.
    /// </summary>
    public class CustomerSupportTemplate<T> : ActivityHandler where T : Dialog
    {
        private IBotServices _services;
        private readonly ConversationState _conversationState;
        private readonly UserState _userState;
        private DialogSet _dialogs;
        protected readonly Dialog _dialog;
        private IStatePropertyAccessor<CustomerSupportTemplateState> _stateAccessor;
        protected readonly BotStateService _botStateService;
        private MainResponses _responder = new MainResponses();

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerSupportTemplate"/> class.
        /// </summary>
        /// <param name="botServices">Bot services.</param>
        /// <param name="conversationState">Bot conversation state.</param>
        /// <param name="userState">Bot user state.</param>
        public CustomerSupportTemplate(BotStateService botStateService, T dialog,IBotServices botServices, ConversationState conversationState, UserState userState)
        {
            _conversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
            _userState = userState ?? throw new ArgumentNullException(nameof(userState));
            _services = botServices ?? throw new ArgumentNullException(nameof(botServices));
            _stateAccessor = _conversationState.CreateProperty<CustomerSupportTemplateState>(nameof(CustomerSupportTemplateState));
            _botStateService = botStateService ?? throw new System.ArgumentNullException(nameof(botStateService));
            _dialog = dialog ?? throw new System.ArgumentNullException(nameof(dialog));

            // _dialogs = new DialogSet(_conversationState.CreateProperty<DialogState>(nameof(CustomerSupportTemplate)));
            // _dialogs.Add(new MainDialog(_services, _conversationState, _userState));
        }
        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            await base.OnTurnAsync(turnContext, cancellationToken);

            // Save any state changes that might have occured during the turn.
            await _botStateService.UserState.SaveChangesAsync(turnContext, false, cancellationToken);
            await _botStateService.ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
        }
        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            await SendWelcomeMessageAsync(turnContext, cancellationToken);
        }
        private static async Task SendWelcomeMessageAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            
              


            foreach (var member in turnContext.Activity.MembersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    var view = new MainResponses();
                    await view.ReplyWith(turnContext, MainResponses.Intro);
                    //await turnContext.SendActivityAsync(
                    //    $"",
                    //    cancellationToken: cancellationToken);
                }
            }
        }
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
 
            // Run the Dialog with the new message Activity.
            await _dialog.Run(turnContext, _botStateService.DialogStateAccessor, cancellationToken);
        }
        /// <summary>
        /// Run every turn of the conversation. Handles orchestration of messages.
        /// </summary>
        /// <param name="turnContext">Bot Turn Context.</param>
        /// <param name="cancellationToken">Task CancellationToken.</param>
        ///// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        //public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        //{
        //    var dc = await _dialogs.CreateContextAsync(turnContext);
        //    var result = await dc.ContinueDialogAsync();
        //    //await dc.BeginDialogAsync(nameof(MainDialog));

        //    if (result.Status == DialogTurnStatus.Empty)
        //    {
        //        if (turnContext.Activity.Type == ActivityTypes.ConversationUpdate)
        //        {
        //            var activity = turnContext.Activity.AsConversationUpdateActivity();

        //            // if conversation update is not from the bot.
        //            if (!activity.MembersAdded.Any(m => m.Id == activity.Recipient.Id))
        //            {
        //                await dc.BeginDialogAsync(nameof(MainDialog));
        //            }
        //        }
        //    }
        //}
    }
}
