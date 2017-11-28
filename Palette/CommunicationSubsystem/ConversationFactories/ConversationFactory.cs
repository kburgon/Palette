using System;
using CommunicationSubsystem.Conversations;

namespace CommunicationSubsystem.ConversationFactories
{
    public abstract class ConversationFactory
    {
        protected static Guid ProcessId = Guid.NewGuid();

        public abstract Conversation CreateFromMessageType(Type message);
    }
}
