using System;
using System.Collections.Generic;
using CommunicationSubsystem.Conversations;
using CommunicationSubsystem.Conversations.ProxyConversations;
using CommunicationSubsystem.Conversations.ResponderConversations;
using Messages;

namespace CommunicationSubsystem.ConversationFactories
{
    public class DisplayConversationFactory : ConversationFactory
    {
        public DisplayConversationFactory()
        {
            ResponderConversationTypes = new Dictionary<Type, Type>
            {
                {typeof(CanvasAssignMessage), typeof(AssignCanvasResponderConversation)},
                {typeof(CanvasUnassignMessage), typeof(CanvasUnassignMessage)},
                {typeof(BrushStrokeMessage), typeof(AckResponderConversation)}
            };
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
