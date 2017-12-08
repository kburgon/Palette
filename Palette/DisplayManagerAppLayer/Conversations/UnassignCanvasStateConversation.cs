using CommunicationSubsystem;
using CommunicationSubsystem.Conversations;
using Messages;
using System;
using System.Net;

namespace DisplayManagerAppLayer.Conversations
{
    public class UnassignCanvasStateConversation : StateConversation
    {
        public UnassignCanvasStateConversation()
        {
            RequestEp = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12250);
        }

        protected override bool CheckMessageType(EnvelopeQueue queue)
        {
            throw new NotImplementedException();
        }

        protected override Message CreateAuthRequest()
        {
            throw new System.NotImplementedException();
        }

        protected override Message CreateRequest()
        {
            var stepNumber = Convert.ToInt16(InitialReceivedEnvelope.Message.MessageNumber.Item2 + 1);
            var message = new CanvasUnassignMessage
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber)
            };

            return message;
        }

        protected override Message CreateUpdate()
        {
            InitialReceivedEnvelope.RemoteEP.Port = 11900;
            var stepNumber = Convert.ToInt16(InitialReceivedEnvelope.Message.MessageNumber.Item2 + 1);
            var message = new CanvasUnassignMessage
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber)
            };

            return message;
        }

        protected override void ProcessAuthReply(Message receivedMessage)
        {
            throw new System.NotImplementedException();
        }

        protected override void ProcessFailure()
        {
            throw new System.NotImplementedException();
        }

        protected override bool ProcessReply(Message receivedMessage)
        {
            var message = receivedMessage;

            return true;
        }
    }
}
