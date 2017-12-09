using CommunicationSubsystem.ConversationFactories;
using System.Collections.Generic;
using System;
using Messages;
using DisplayAppLayer.Conversations;

namespace DisplayAppLayer
{
    class DisplayConversationFactory : ConversationFactory
    {
        public override void Initialize()
        {
            ResponderConversationTypes = new Dictionary<Type, Type>()
            {
                {typeof(CanvasAssignMessage), typeof(AssignCanvasResponderConversation)},
                {typeof(CanvasUnassignMessage), typeof(UnassignCanvasResponderConversation)}
            };
        }
    }
}
