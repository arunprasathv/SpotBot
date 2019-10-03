using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.SpotBot
{
    public class BotStateService
    {
        #region Variables
        // State Variables
        public ConversationState ConversationState { get; }
        public UserState UserState { get; }

        // IDs
        public static string OrderID { get; } = $"{nameof(BotStateService)}.OrderID";
        public static string ConversationDataId { get; } = $"{nameof(BotStateService)}.ConversationData";
        public static string DialogStateId { get; } = $"{nameof(BotStateService)}.DialogState";

        // Accessors
        public IStatePropertyAccessor<Order> OrderDataAccessor { get; set; }
        //public IStatePropertyAccessor<ConversationData> ConversationDataAccessor { get; set; }
        public IStatePropertyAccessor<DialogState> DialogStateAccessor { get; set; }
        #endregion

        public BotStateService(ConversationState conversationState, UserState userState)
        {
            ConversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
            UserState = userState ?? throw new ArgumentNullException(nameof(userState));

            InitializeAccessors();
        }

        public void InitializeAccessors()
        {
            // Initialize Conversation State Accessors
            //ConversationDataAccessor = ConversationState.CreateProperty<ConversationData>(ConversationDataId);
            DialogStateAccessor = ConversationState.CreateProperty<DialogState>(DialogStateId);

            // Initialize User State
            OrderDataAccessor = UserState.CreateProperty<Order>(OrderID);
        }
    }
}
