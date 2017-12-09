using CommunicationSubsystem.Conversations;
using System;
using Messages;
using CommunicationSubsystem;
using SharedAppLayer.Entitities;

namespace DisplayAppLayer.Conversations
{
    class SubscribeCanvasInitiatorConversation : InitiatorConversation
    {
        public Canvas NewDisplayCanvas;
        public int SubCanvasId;
        public int DisplayId;

        protected override bool CheckMessageType(EnvelopeQueue queue)
        {
            if (queue.GetMessageType(typeof(SendCanvasMessage)) == typeof(SendCanvasMessage))
                return true;

            return false;
        }

        protected override Message CreateRequest()
        {
            var message = new SubscriberCanvasMessage()
            {
                ConversationId = this.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, 1),
                CanvasId = SubCanvasId,
                DisplayId = this.DisplayId
            };

            return message;
        }

        protected override void ProcessFailure()
        {
            base.ProcessFailure();
        }

        protected override bool ProcessReply(Message receivedMessage)
        {
            var message = (SendCanvasMessage)receivedMessage;
            NewDisplayCanvas = message.Canvas;

            return true;
        }
    }
}
