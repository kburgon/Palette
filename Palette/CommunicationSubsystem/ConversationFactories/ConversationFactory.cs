using System;
using CommunicationSubsystem.Conversations;

namespace CommunicationSubsystem.ConversationFactories
{
    public abstract class ConversationFactory
    {
        protected static Guid ProcessId = Guid.NewGuid();

        public abstract Conversation CreateFromMessageType(Type message);

        public abstract Conversation CreateFromConversationType(ConversationType conversationType);

        public enum ConversationType
        {
            AssignCanvas,
            CreateCanvas,
            EditCanvas,
            GetDisplay,
            ReadCanvas,
            RegisterDisplay,
            SubscribeCanvas,
            UnassignCanvas
        }
    }
}
