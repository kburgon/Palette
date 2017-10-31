using System;
using System.Collections.Generic;
using System.Text;
using CommunicationSubsystem.Conversations;
using CommunicationSubsystem.Conversations.ProxyConversations;
using CommunicationSubsystem.Conversations.ResponderConversations;
using CommunicationSubsystem.Conversations.StateConversations;
using Messages;

namespace CommunicationSubsystem.ConversationFactories
{
    public class DisplayManagerConversationFactory : ConversationFactory
    {
        public DisplayManagerConversationFactory()
        {
            ResponderConversationTypes = new Dictionary<Type, Type>
            {
                {typeof(RegisterDisplayMessage), typeof(AckResponderConversation)},
                {typeof(GetDisplayMessage), typeof(ProxyConversation)},
                {typeof(CanvasAssignMessage), typeof(AssignCanvasStateConversation)},
                {typeof(CanvasUnassignMessage), typeof(UnassignCanvasStateConversation)}
            };
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
