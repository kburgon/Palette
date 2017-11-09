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

        protected override void CreateRequest()
        {
            var message = new DeleteCanvasMessage()
            {
                ConversationId = this.ConversationId,
                MessageNumber = new Tuple<Guid, short>(this.ProcessId, 1),
                CanvasId = CanvasId.Item1
            };

            var envelope = new Envelope()
            {
                RemoteEP = RemoteEndPoint,
                Message = message
            };

            Communicator.Send(envelope);
        }

        protected override void ProcessReply()
        {
            var envelope = EnvelopeQueue.Dequeue();
            var message = (CanvasMessage)envelope.Message;
            CanvasId = new Tuple<int>(message.CanvasId);
            EnvelopeQueue.EndOfConversation = true;
        }
    }
}
