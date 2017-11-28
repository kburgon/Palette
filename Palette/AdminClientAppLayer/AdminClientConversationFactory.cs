using System;
using AdminClientAppLayer.Conversations;
using CommunicationSubsystem.ConversationFactories;
using CommunicationSubsystem.Conversations;

namespace AdminClientAppLayer
{
    public class AdminClientConversationFactory : ConversationFactory
    {
        public override Conversation CreateFromMessageType(Type message)
        {
            throw new NotImplementedException();
        }
    }
}
