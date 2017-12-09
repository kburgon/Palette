using CommunicationSubsystem;
using CommunicationSubsystem.Conversations;
using Messages;
using SharedAppLayer.Entitities;
using System;

namespace CanvasManagerAppLayer.Conversations
{
    public class SubscribeCanvasStateConversation : StateConversation
    {

        public int CanvasId;
        public int DisplayId;
        public Canvas Canvas;
        public Tuple<Guid, short> NextMessageNumber;

        protected override bool CheckMessageType(EnvelopeQueue queue)
        {
            if (queue.GetMessageType(typeof(SendCanvasMessage)) == typeof(SendCanvasMessage))
                return true;

            return false;
        }

        protected override void ProcessReceivedMessage()
        {
            if (InitialReceivedEnvelope.Message.GetType() != typeof(SubscriberCanvasMessage))
                return;

            base.ProcessReceivedMessage();

            CanvasId = (InitialReceivedEnvelope.Message as SubscriberCanvasMessage).CanvasId;
            DisplayId = (InitialReceivedEnvelope.Message as SubscriberCanvasMessage).DisplayId;
            NextMessageNumber = new Tuple<Guid, short>(InitialReceivedEnvelope.Message.MessageNumber.Item1, (short)(InitialReceivedEnvelope.Message.MessageNumber.Item2 + 1));
        }

        protected override void ProcessFailure()
        {
            base.ProcessFailure();
        }

        protected override Message CreateRequest()
        {
            var message = new SubscriberCanvasMessage()
            {
                ConversationId = this.ConversationId,
                MessageNumber = NextMessageNumber,
                CanvasId = this.CanvasId,
                DisplayId = this.DisplayId
            };

            return message;
        }

        protected override Message CreateUpdate()
        {
            var message = new SendCanvasMessage()
            {
                ConversationId = this.ConversationId,
                MessageNumber = NextMessageNumber,
                Canvas = this.Canvas
            };

            return message;
        }

        protected override bool ProcessReply(Message receivedMessage)
        {
            if (receivedMessage.GetType() != typeof(SendCanvasMessage))
                return false;

            var message = (SendCanvasMessage)receivedMessage;
            Canvas = message.Canvas;
            NextMessageNumber = new Tuple<Guid, short>(message.MessageNumber.Item1, (short)(message.MessageNumber.Item2 + 1));

            return true;
        }
    }
}
