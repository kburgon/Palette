using System;
using Messages;
using CommunicationSubsystem.Conversations;
using CommunicationSubsystem;

namespace AdminClientAppLayer.Conversations
{
    public class DeleteCanvasInitiatorConversation : InitiatorConversation
    {
        public int CanvasId { get; set; }

        public DeleteCanvasInitiatorConversation()
        {
            CanvasId = -1;
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
                CanvasId = this.CanvasId
            };

            return message;
        }

        protected override bool ProcessReply(Message receivedMessage)
        {
            var message = (CanvasMessage)receivedMessage;
            CanvasId = message.CanvasId;
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
