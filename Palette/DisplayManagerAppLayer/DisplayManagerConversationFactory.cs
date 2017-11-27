using CommunicationSubsystem.ConversationFactories;
using System;
using System.Collections.Generic;
using Messages;
using DisplayManagerAppLayer.Conversations;

namespace DisplayManagerAppLayer
{
    class DisplayManagerConversationFactory : ConversationFactory
    {
        public override void Initialize()
        {
            ResponderConversationTypes = new Dictionary<Type, Type>
            {
                {typeof(CanvasAssignMessage), typeof(AssignCanvasStateConversation) },
                {typeof(GetDisplayListMessage), typeof(GetDisplayListStateConversation) },
                {typeof(RegisterDisplayMessage), typeof(RegisterDisplayResponderConversation) },
                {typeof(CanvasUnassignMessage), typeof(UnassignCanvasStateConversation) }
            };
        }
    }
}
