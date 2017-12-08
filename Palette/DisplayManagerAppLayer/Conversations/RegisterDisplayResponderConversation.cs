using CommunicationSubsystem.Conversations;
using Messages;
using System;
using System.Net;

namespace DisplayManagerAppLayer.Conversations
{
    public class RegisterDisplayResponderConversation : ResponderConversation
    {
        public string DisplayAddress;
        public int DisplayId;
        private Tuple<Guid, short> NextMessageNumber;
        private DisplayManager DisplayManager = new DisplayManager();

        protected override Message CreateReply()
        {
            var message = new RegisterAckMessage()
            {
                ConversationId = this.ConversationId,
                MessageNumber = NextMessageNumber,
                DisplayId = this.DisplayId
            };

            return message;
        }

        protected override void ProcessFailure()
        {
            base.ProcessFailure();
        }

        protected override void ProcessReceivedMessage(Message message)
        {
            DisplayAddress = (message as RegisterDisplayMessage).IPAddress;
            DisplayManager.AddDisplay((message as RegisterDisplayMessage).IPAddress);
            var nextNum = message.MessageNumber.Item2 + 1;
            NextMessageNumber = new Tuple<Guid, short>(message.MessageNumber.Item1, (short)nextNum);
        }
    }
}
