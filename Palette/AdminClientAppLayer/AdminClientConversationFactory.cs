using System;
using System.Collections.Generic;
using System.Text;
using CommunicationSubsystem.ConversationFactories;
using Messages;

namespace AdminClientAppLayer
{
    public class AdminClientConversationFactory : ConversationFactory
    {
        public override void Initialize()
        {
            ResponderConversationTypes = new Dictionary<Type, Type> { };
        }
    }
}
