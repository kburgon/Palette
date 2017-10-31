using System;
using System.Collections.Generic;
using System.Text;
using CommunicationSubsystem.Conversations;
using CommunicationSubsystem.Conversations.ResponderConversations;
using Messages;

namespace CommunicationSubsystem.ConversationFactories
{
    public class AdminManagerConversationFactory : ConversationFactory
    {
        public AdminManagerConversationFactory()
        {
            ResponderConversationTypes = new Dictionary<Type, Type>
            {
                {typeof(AuthMessage), typeof(VerifyAuthResponderConversation)}
            };
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
