using System;
using System.Collections.Generic;
using CommunicationSubsystem.Conversations.InitiatorConversations;
using Messages;
using SharedAppLayer.Entitities;

namespace AdminClientAppLayer.Conversations
{
    public class GetCanvasListInitiatorConversation : InitiatorConversation
    {
        public IEnumerable<Canvas> Canvases { get; set; }

        public GetCanvasListInitiatorConversation()
        {
            Canvases = null;
        }

        protected override void ProcessFailure()
        {
            throw new NotImplementedException();
        }

        protected override void ValidateConversationState()
        {
            throw new NotImplementedException();
        }

        protected override void CheckProcessState()
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

        protected override void ProcessReply(Message message)
        {
            EnvelopeQueue.EndOfConversation = true;
            Canvases = (message as CanvasListMessage)?.Canvases ?? new List<Canvas>();
        }
    }
}
