using CommunicationSubsystem;
using CommunicationSubsystem.Conversations;
using Messages;
using System;
using System.Collections.Generic;
using SharedAppLayer.Entitities;
using System.Net;

namespace CanvasManagerAppLayer.Conversations
{
    public class GetCanvasListStateConversation : StateConversation
    {
        private IEnumerable<Canvas> _canvasList;

        public GetCanvasListStateConversation()
        {
            RequestEp = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12500);
        }

        protected override void ProcessFailure()
        {
            throw new NotImplementedException();
        }

        protected override void ProcessReceivedMessage()
        {
            if (InitialReceivedEnvelope.Message.GetType() != typeof(GetCanvasListMessage))
                return;

            base.ProcessReceivedMessage();
        }

        protected override Message CreateRequest()
        {
            var stepNumber = InitialReceivedEnvelope.Message.MessageNumber.Item2;
            var message = new GetCanvasListMessage
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber)
            };

            return message;
        }

        protected override bool ProcessReply(Message receivedMessage)
        {
            if (receivedMessage.GetType() == typeof(CanvasListMessage))
            {
                var message = (CanvasListMessage)receivedMessage;
                _canvasList = message.Canvases;
                return true;
            }
            return false;
        }

        protected override Message CreateUpdate()
        {
            InitialReceivedEnvelope.RemoteEP.Port = 11900;
            var stepNumber = Convert.ToInt16(InitialReceivedEnvelope.Message.MessageNumber.Item2 + 1);
            var message = new CanvasListMessage
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber)
            };

            return message;
        }

        protected override bool CheckMessageType(EnvelopeQueue queue)
        {
            if (queue.GetMessageType(typeof(CanvasListMessage)) == typeof(CanvasListMessage))
                return true;

            return false;
        }
    }
}
