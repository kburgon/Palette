using CommunicationSubsystem;
using CommunicationSubsystem.Conversations;
using Messages;
using System;
using System.Net;

namespace CanvasManagerAppLayer.Conversations
{
    public class DeleteCanvasStateConversation : StateConversation
    {

        public DeleteCanvasStateConversation()
        {
            RequestEp = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12500);
        }

        protected override void ProcessFailure()
        {
            throw new NotImplementedException();
        }

        protected override void ProcessReceivedMessage()
        {
            if (InitialReceivedEnvelope.Message.GetType() != typeof(DeleteCanvasMessage))
                return;

            base.ProcessReceivedMessage();
        }

        protected override Message CreateRequest()
        {
            var stepNumber = InitialReceivedEnvelope.Message.MessageNumber.Item2;
            var message = new DeleteCanvasMessage
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber)
            };

            return message;
        }

        protected override bool ProcessReply(Message receivedMessage)
        {
            var message = receivedMessage;
            if(message.GetType() == typeof(CanvasMessage))
                return true;
            return false;
        }

        protected override Message CreateUpdate()
        {
            InitialReceivedEnvelope.RemoteEP.Port = 11900;
            var stepNumber = Convert.ToInt16(InitialReceivedEnvelope.Message.MessageNumber.Item2 + 1);
            var message = new DeleteCanvasMessage
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber)
            };

            return message;
        }

        protected override bool CheckMessageType(EnvelopeQueue queue)
        {
            if (queue.GetMessageType(typeof(CanvasMessage)) == typeof(CanvasMessage))
                return true;

            return false;
        }
    }
}
