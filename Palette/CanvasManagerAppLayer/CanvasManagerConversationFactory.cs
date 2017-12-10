using CanvasManagerAppLayer.Conversations;
using CommunicationSubsystem.ConversationFactories;
using Messages;
using System;
using System.Collections.Generic;

namespace CanvasManagerAppLayer
{
    class CanvasManagerConversationFactory : ConversationFactory
    {
        public override void Initialize()
        {
            ResponderConversationTypes = new Dictionary<Type, Type>
            {
                {typeof(CreateCanvasMessage), typeof(CreateCanvasStateConversation)},
                {typeof(DeleteCanvasMessage), typeof(DeleteCanvasStateConversation)},
                {typeof(GetCanvasListMessage), typeof(GetCanvasListStateConversation)},
                {typeof(BrushStrokeMessage), typeof(EditCanvasConversation)}
            };
        }
    }
}
