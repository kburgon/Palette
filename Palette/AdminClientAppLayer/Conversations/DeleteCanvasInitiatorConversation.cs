using System;
using Messages;
using CommunicationSubsystem.Conversations;

namespace AdminClientAppLayer.Conversations
{
    public class DeleteCanvasInitiatorConversation : InitiatorConversation
    {
        public Tuple<int> CanvasId { get; set; }

        public DeleteCanvasInitiatorConversation()
        {
            CanvasId = null;
        }

        protected override void ProcessFailure()
        {
            base.ProcessFailure();
        }

        protected override Message CreateRequest()
        {
            var message = new DeleteCanvasMessage()
            {
                ConversationId = this.ConversationId,
                MessageNumber = new Tuple<Guid, short>(this.ProcessId, 1),
                CanvasId = CanvasId.Item1
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
