using System;
using System.Collections.Generic;
using System.Text;
using CommunicationSubsystem.Conversations;
using CommunicationSubsystem.Conversations.MulticastReponderConversations;
using CommunicationSubsystem.Conversations.ResponderConversations;
using CommunicationSubsystem.Conversations.StateConversations;
using Messages;

namespace CommunicationSubsystem.ConversationFactories
{
    public class CanvasManagerConversationFactory : ConversationFactory
    {
        public CanvasManagerConversationFactory()
        {
            ResponderConversationTypes = new Dictionary<Type, Type>
            {
                {typeof(CreateCanvasMessage), typeof(CreateCanvasStateConversation)},
                {typeof(DeleteCanvasMessage), typeof(DeleteCanvasStateConversation)},
                {typeof(GetCanvasListMessage), typeof(ReadCanvasStateConversation)},
                {typeof(BrushStrokeMessage), typeof(MulticastResponderConversation)},
                {typeof(SubscriberCanvasMessage), typeof(AckResponderConversation)}
            };
        }
        public override void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
