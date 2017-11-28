using CommunicationSubsystem.ConversationFactories;
using System;
using CommunicationSubsystem.Conversations;

namespace CanvasManagerAppLayer
{
    class CanvasManagerConversationFactory : ConversationFactory
    {
        public override Conversation CreateFromMessageType(Type message)
        {
            return null;
        }
    }
}
