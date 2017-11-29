using System;
using CommunicationSubsystem.Conversations.InitiatorConversations;
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
            return new CreateCanvasMessage()
            {
                ConversationId = this.ConversationId,
                MessageNumber = new Tuple<Guid, short>(this.ProcessId, 1)
            };
        }

        protected override void ProcessReply(Message message)
        {
            var canvasMessage = (CanvasMessage) message;
            CanvasId = new Tuple<int>(canvasMessage.CanvasId);
            EnvelopeQueue.EndOfConversation = true;
        }
    }
}
