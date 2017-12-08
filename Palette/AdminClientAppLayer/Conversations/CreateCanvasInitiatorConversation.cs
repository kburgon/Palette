using System;
using CommunicationSubsystem;
using CommunicationSubsystem.Conversations;
using Messages;
using log4net;

namespace AdminClientAppLayer.Conversations
{
    public class CreateCanvasInitiatorConversation : InitiatorConversation
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CreateCanvasInitiatorConversation));
        public int CanvasId { get; set; }

        public CreateCanvasInitiatorConversation()
        {
            CanvasId = -1;
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

        protected override bool ProcessReply(Message receivedMessage)
        {
            var message = (CanvasMessage)receivedMessage;
            CanvasId = message.CanvasId;
            Logger.InfoFormat("New CanvasId: {0}", CanvasId);
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
