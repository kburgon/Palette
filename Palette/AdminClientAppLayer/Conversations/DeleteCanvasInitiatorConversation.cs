using System;
using Messages;
using CommunicationSubsystem.Conversations;
using CommunicationSubsystem;

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

        protected override bool ProcessReply(Message receivedMessage)
        {
            var message = (CanvasMessage)receivedMessage;
            CanvasId = new Tuple<int>(message.CanvasId);
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
