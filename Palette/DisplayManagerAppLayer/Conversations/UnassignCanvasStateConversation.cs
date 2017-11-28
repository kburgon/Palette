using CommunicationSubsystem;
using CommunicationSubsystem.Conversations;
using Messages;
using System;

namespace DisplayManagerAppLayer.Conversations
{
    public class UnassignCanvasStateConversation : StateConversation
    {
        protected override void CreateAuthRequest()
        {
            throw new System.NotImplementedException();
        }

        protected override void CreateRequest()
        {
            var stepNumber = Convert.ToInt16(InitialReceivedEnvelope.Message.MessageNumber.Item2 + 1);
            var message = new CanvasUnassignMessage
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

        protected override void CreateUpdate()
        {
            var stepNumber = Convert.ToInt16(InitialReceivedEnvelope.Message.MessageNumber.Item2 + 1);
            var message = new CanvasUnassignMessage
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

        protected override void ProcessAuthReply()
        {
            throw new System.NotImplementedException();
        }

        protected override void ProcessFailure()
        {
            throw new System.NotImplementedException();
        }

        protected override void ProcessReply()
        {
            var envelope = EnvelopeQueue.Dequeue();
            var message = envelope.Message;
        }
    }
}
