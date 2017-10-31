using System;
using System.Collections.Generic;
using System.Text;
using CommunicationSubsystem.Conversations;

namespace CommunicationSubsystem.ConversationFactories
{
    public class AdminClientConversationFactory : ConversationFactory
    {
        public AdminClientConversationFactory()
        {
            ResponderConversationTypes = new Dictionary<Type, Type> { };
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
