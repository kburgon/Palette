using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CanvasManagerAppLayer.Conversations;
using CommunicationSubsystem.ConversationFactories;
using Messages;

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
                {typeof(GetCanvasListMessage), typeof(GetCanvasListStateConversation)}
            };
        }
    }
}
