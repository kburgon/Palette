using CommunicationSubsystem;
using CommunicationSubsystem.Conversations;
using Messages;
using System;
using System.Collections.Generic;

namespace DisplayManagerAppLayer.Conversations
{
    public class GetDisplayListStateConversation : StateConversation
    {
        private IEnumerable<string> _displayList;

        protected override void CreateAuthRequest()
        {
            throw new System.NotImplementedException();
        }

        protected override void CreateRequest()
        {
            var stepNumber = InitialReceivedEnvelope.Message.MessageNumber.Item2;
            var message = new GetDisplayListMessage
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

        protected override void CreateUpdate()
        {
            var stepNumber = Convert.ToInt16(InitialReceivedEnvelope.Message.MessageNumber.Item2 + 1);
            var message = new DisplayListMessage
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
            var message = (DisplayListMessage)envelope.Message;
            _displayList = message.Displays;
        }
    }
}
