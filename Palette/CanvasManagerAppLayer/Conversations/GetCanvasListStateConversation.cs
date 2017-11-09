using CommunicationSubsystem;
using CommunicationSubsystem.Conversations;
using Messages;
using System;
using System.Collections.Generic;

namespace CanvasManagerAppLayer.Conversations
{
    public class GetCanvasListStateConversation : StateConversation
    {
        private IEnumerable<string> _canvasList;

        protected override void ProcessFailure()
        {
            throw new NotImplementedException();
        }

        protected override void CreateAuthRequest()
        {
            throw new NotImplementedException();
        }

        protected override void ProcessAuthReply()
        {
            throw new NotImplementedException();
        }

        protected override void CreateRequest()
        {
            var stepNumber = InitialReceivedEnvelope.Message.MessageNumber.Item2;
            var message = new GetCanvasListMessage
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber)
            };

            var envelope = new Envelope
            {
                Message = message,
                RemoteEP = InitialReceivedEnvelope.RemoteEP
            };
        }

        protected override void ProcessReply()
        {
            var envelope = EnvelopeQueue.Dequeue();
            var message = (CanvasListMessage) envelope.Message;
            _canvasList = message.Canvases;
        }

        protected override void CreateUpdate()
        {
            var stepNumber = Convert.ToInt16(InitialReceivedEnvelope.Message.MessageNumber.Item2 + 1);
            var message = new CanvasListMessage
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber)
            };

            var envelope = new Envelope
            {
                Message = message,
                RemoteEP = InitialReceivedEnvelope.RemoteEP
            };

            Communicator.Send(envelope);
        }
    }
}
