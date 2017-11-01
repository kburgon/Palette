using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommunicationSubsystem;
using CommunicationSubsystem.Conversations.StateConversations;
using Messages;

namespace CanvasManagerAppLayer.Conversations
{
    public class CreateCanvasStateConversation : StateConversation
    {
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

        protected override void CreateFirstUpdatedState()
        {
            var stepNumber = Convert.ToInt16(InitialReceivedEnvelope.Message.MessageNumber.Item2 + 1);
            var message = new CreateCanvasMessage()
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber),
            };

            var envelope = new Envelope
            {
                Message = message,
                RemoteEP = InitialReceivedEnvelope.RemoteEP
            };

            Communicator.Send(envelope);
        }

        protected override void CreateRequest()
        {
            var stepNumber = InitialReceivedEnvelope.Message.MessageNumber.Item2;
            var message = new CreateCanvasMessage()
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

        protected override void ProcessReply()
        {
            throw new NotImplementedException();
        }

        protected override void CreateSecondUpdatedState()
        {
            var stepNumber = Convert.ToInt16(InitialReceivedEnvelope.Message.MessageNumber.Item2 + 2);
            var message = new CreateCanvasMessage()
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
