using CommunicationSubsystem.Conversations;
using Messages;
using System;

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

        protected override void CreateRequest()
        {
            var stepNumber = InitialReceivedEnvelope.Message.MessageNumber.Item2;
            var message = new CreateCanvasMessage()
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber)
            };
        }

        protected override void ProcessReply()
        {
            var envelope = EnvelopeQueue.Dequeue();
            var message = envelope.Message;
        }

        protected override void CreateUpdate()
        {
            var stepNumber = Convert.ToInt16(InitialReceivedEnvelope.Message.MessageNumber.Item2 + 1);
            var message = new CreateCanvasMessage()
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber)
            };
        }
    }
}
