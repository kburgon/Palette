using CommunicationSubsystem.ConversationFactories;
using System;
using CommunicationSubsystem.Conversations;

namespace AuthManagerAppLayer
{
    class AuthManagerConversationFactory : ConversationFactory
    {
        public override Conversation CreateFromMessageType(Type message)
        {
            throw new NotImplementedException();
        }
    }
}
