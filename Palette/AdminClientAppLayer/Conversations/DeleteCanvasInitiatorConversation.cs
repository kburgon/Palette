using System;
using System.Collections.Generic;
using System.Text;
using CommunicationSubsystem.Conversations.InitiatorConversations;
using Messages;
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
            return new DeleteCanvasMessage
            {
                ConversationId = ConversationId,
                MessageNumber = new Tuple<Guid, short>(this.ProcessId, 1),
                CanvasId = CanvasId.Item1
            };
        }

        protected override void ProcessReply(Message message)
        {
            var canvasMessage = (CanvasMessage)message;
            CanvasId = new Tuple<int>(canvasMessage.CanvasId);
            EnvelopeQueue.EndOfConversation = true;
        }
    }
}
