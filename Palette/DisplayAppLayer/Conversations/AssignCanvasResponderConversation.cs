using CommunicationSubsystem.Conversations;
using Messages;
using System;

namespace DisplayAppLayer.Conversations
{
    public class AssignCanvasResponderConversation : ResponderConversation
    {
        public int CanvasID { get; set; }
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
