using CommunicationSubsystem.Conversations;
using System;
using Messages;

namespace DisplayAppLayer.Conversations
{
    public class UnassignCanvasResponderConversation : ResponderConversation
    {
        public int CanvasID;
        private Tuple<Guid, short> nextMessageNumber;

        protected override Message CreateReply()
        {
            var message = new CanvasMessage()
            {
                ConversationId = this.ConversationId,
                MessageNumber = nextMessageNumber,
                CanvasId = this.CanvasID
            };

            return message;
        }

        protected override void ProcessFailure()
        {
            base.ProcessFailure();
        }

        protected override void ProcessReceivedMessage(Message receivedMessage)
        {
            var message = (CanvasAssignMessage)receivedMessage;
            CanvasID = message.CanvasId;
            nextMessageNumber = new Tuple<Guid, short>(message.MessageNumber.Item1, (short)(message.MessageNumber.Item2 + 1));
        }
    }
}
