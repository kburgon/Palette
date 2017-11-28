using System;
using AdminClientAppLayer.Conversations;
using CommunicationSubsystem.ConversationFactories;
using CommunicationSubsystem.Conversations;
using Messages;

namespace AdminClientAppLayer
{
    public class AdminClientConversationFactory : ConversationFactory
    {
        public override Conversation CreateFromMessageType(MessageType message)
        {
            switch (message)
            {
                case MessageType.CanvasListMessage:
                    return new GetCanvasListInitiatorConversation();
                case MessageType.Unknown:
                    throw new Exception("Message is of Unknown Type");
                default:
                    throw new ArgumentOutOfRangeException(nameof(message), message, null);
            }
        }
    }
}
