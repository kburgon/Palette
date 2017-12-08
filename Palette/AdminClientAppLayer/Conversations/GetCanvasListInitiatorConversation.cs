using System;
using System.Collections.Generic;
using Messages;
using CommunicationSubsystem.Conversations;
using SharedAppLayer.Entitities;
using CommunicationSubsystem;

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
            base.ProcessFailure();
        }

        protected override Message CreateRequest()
        {
            var message = new GetCanvasListMessage()
            {
                ConversationId = this.ConversationId,
                MessageNumber = new Tuple<Guid, short>(this.ProcessId, 1)
            };

            return message;
        }

        protected override bool ProcessReply(Message receivedMessage)
        {
            Canvases = (receivedMessage as CanvasListMessage)?.Canvases ?? new List<Canvas> { };
            return EnvelopeQueue.EndOfConversation = true;
        }

        protected override bool CheckMessageType(EnvelopeQueue queue)
        {
            if (queue.GetMessageType(typeof(CanvasMessage)) == typeof(CanvasMessage))
                return true;

            return false;
        }
    }
}
