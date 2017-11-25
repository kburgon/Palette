using System;
using CommunicationSubsystem.Conversations;
using Messages;

namespace AuthManagerAppLayer.Conversations
{
    public class CheckTokenResponderConversation : ResponderConversation
    {
        public TokenBank TokenBank { get; set; }
        private bool IsAuthorized { get; set; }
        private Guid AuthToken { get; set; }

        protected override void ProcessFailure()
        {
            throw new NotImplementedException();
        }

        protected override void ProcessReceivedMessage(Message message)
        {
            var authMessage = (AuthMessage)message;
            if (TokenBank.TokenExists(authMessage.AuthToken))
            {
                IsAuthorized = true;
            }

            AuthToken = authMessage.AuthToken;
        }

        protected override Message CreateReply()
        {
            return new TokenVerifyMessage
            {
                ConversationId = ReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                AuthToken = AuthToken,
                IsAuthorized = IsAuthorized
            };
        }
    }
}
