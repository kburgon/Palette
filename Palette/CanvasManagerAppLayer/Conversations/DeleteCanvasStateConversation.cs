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
            RequestEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12500);
        }

        protected override void ProcessFailure()
        {
            throw new NotImplementedException();
        }

        protected override void ProcessReceivedMessage()
        {
            throw new NotImplementedException();
        }

        protected override Message CreateAuthRequest()
        {
            throw new NotImplementedException();
        }

        protected override void ProcessAuthReply(Message receivedMessage)
        {
            throw new NotImplementedException();
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

        protected override void ProcessReply(Message receivedMessage)
        {
            var message = receivedMessage;
        }

        protected override Message CreateUpdate()
        {
            var stepNumber = Convert.ToInt16(InitialReceivedEnvelope.Message.MessageNumber.Item2 + 1);
            var message = new DeleteCanvasMessage
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber)
            };

            return message;
        }
    }
}
