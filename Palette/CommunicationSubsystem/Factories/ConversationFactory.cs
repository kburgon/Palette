using System;
using System.Collections.Generic;
using System.Text;
using CommunicationSubsystem.Conversations;

namespace CommunicationSubsystem.Factories
{
    public abstract class ConversationFactory
    {
        public abstract void Initialize();

        public Conversation CreateMessageFromType()
        {
            throw new NotImplementedException();
        }

        public Conversation CreateFromConversationType()
        {
            throw new NotImplementedException();
        }

        protected void Add()
        {
            throw new NotImplementedException();
        }
    }
}
