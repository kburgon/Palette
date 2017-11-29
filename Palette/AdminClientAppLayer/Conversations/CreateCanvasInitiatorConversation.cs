using System;
using CommunicationSubsystem.Conversations;
using Messages;

namespace AdminClientAppLayer.Conversations
{
    public class CreateCanvasInitiatorConversation : InitiatorConversation
    {
        public Tuple<int> CanvasId { get; set; }

        public CreateCanvasInitiatorConversation()
        {
            CanvasId = null;
        }

        protected override void ProcessFailure()
        {
            base.ProcessFailure();
        }

        protected override Message CreateRequest()
        {
            var message = new CreateCanvasMessage()
            {
                ConversationId = this.ConversationId,
                MessageNumber = new Tuple<Guid, short>(this.ProcessId, 1)
            };

            return message;
        }

        protected override void ProcessReply(Message receivedMessage)
        {
            var message = (CanvasMessage)receivedMessage;
            CanvasId = new Tuple<int>(message.CanvasId);
            EnvelopeQueue.EndOfConversation = true;
        }
    }
}
