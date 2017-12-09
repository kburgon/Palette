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

        protected override Message CreateReply()
        {
            RequestEP = ReceivedEnvelope.RemoteEP;
            RequestEP.Port = 12200;
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
            if (message.GetType() != typeof(RegisterDisplayMessage))
                return;
            DisplayAddress = (message as RegisterDisplayMessage).IPAddress;

            var nextNum = message.MessageNumber.Item2 + 1;
            NextMessageNumber = new Tuple<Guid, short>(message.MessageNumber.Item1, (short)nextNum);
        }
    }
}
