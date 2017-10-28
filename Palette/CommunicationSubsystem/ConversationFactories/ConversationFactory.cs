using System;
using CommunicationSubsystem.Conversations;
using CommunicationSubsystem.Messages;

namespace CommunicationSubsystem.ConversationFactories
{
    public abstract class ConversationFactory
    {
        public abstract void Initialize();

        public Conversation CreateFromMessageType(Message message)
        {
            throw new NotImplementedException();
        }

        public Conversation CreateFromConversationType(Conversation conversation)
        {
            throw new NotImplementedException();
        }

        protected void Add(Message message, Conversation conversation)
        {
            throw new NotImplementedException();
        }
    }
}
