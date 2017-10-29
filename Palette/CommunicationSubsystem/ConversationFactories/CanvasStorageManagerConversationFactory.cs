using System;
using System.Collections.Generic;
using System.Text;
using CommunicationSubsystem.Conversations;
using CommunicationSubsystem.Conversations.ResponderConversations;
using Messages;

namespace CommunicationSubsystem.ConversationFactories
{
    public class CanvasStorageManagerConversationFactory : ConversationFactory
    {
        public CanvasStorageManagerConversationFactory()
        {
            ResponderConversationTypes = new Dictionary<Type, Type>
            {
                {typeof(CreateCanvasMessage), typeof(AckResponderConversation)},
                {typeof(DeleteCanvasMessage), typeof(AckResponderConversation)},
                {typeof(CanvasListMessage), typeof(GetCanvasListResponderConversation)},
                {typeof(BrushStrokeMessage), typeof(AckResponderConversation)}
            };
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
