using System;
using System.Collections.Generic;
using CommunicationSubsystem.Conversations;
using CommunicationSubsystem.Conversations.InitiatorConversations;
using Messages;
using SharedAppLayer.Entitities;

namespace AdminClientAppLayer.Conversations
{
    public class GetCanvasStateConversation : StateSubscriberConversation
    {
        public IEnumerable<Canvas> Canvases { get; set; }

        public GetCanvasStateConversation()
        {
            Canvases = null;
        }

        protected override void ProcessFailure()
        {
            throw new NotImplementedException();
        }

        protected override Message CreateRequest()
        {
            return new GetCanvasListMessage
            {
                ConversationId = ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, 1)
            };
        }

        protected override void ProcessStateUpdates()
        {
            var updateMessage = GetNextEnvelope();
        }
    }
}
