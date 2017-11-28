using System;
using CommunicationSubsystem.Conversations;
using Messages;

namespace CommunicationSubsystem.ConversationFactories
{
    public abstract class ConversationFactory
    {
        protected static Guid ProcessId = Guid.NewGuid();

        public abstract Conversation CreateFromMessageType(MessageType message);
    }
}
