using CommunicationSubsystem.ConversationFactories;
using System;
using CanvasManagerAppLayer.Conversations;
using CommunicationSubsystem.Conversations;
using Messages;

namespace CanvasManagerAppLayer
{
    class CanvasManagerConversationFactory : ConversationFactory
    {
        public override Conversation CreateFromMessageType(MessageType message)
        {
            switch (message)
            {
                case MessageType.CanvasListMessage:
                    return new GetCanvasListStateConversation();
                case MessageType.Unknown:
                    throw new Exception("Unknown Message Type");
                default:
                    throw new ArgumentOutOfRangeException(nameof(message), message, null);
            }
        }
    }
}
