using CommunicationSubsystem;
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

        protected override Message CreateAuthRequest()
        {
            return InitialReceivedEnvelope.Message;
        }

        protected override void ProcessAuthReply(Message receivedMessage)
        {
            throw new NotImplementedException();
        }

        protected override Message CreateRequest()
        {
            var stepNumber = InitialReceivedEnvelope.Message.MessageNumber.Item2;
            var message = new CreateCanvasMessage()
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
            var message = new CreateCanvasMessage()
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber)
            };

            return message;
        }
    }
}
