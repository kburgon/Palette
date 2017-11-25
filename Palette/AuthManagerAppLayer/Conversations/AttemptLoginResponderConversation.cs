using Messages;
using System;
using CommunicationSubsystem.Conversations;

namespace AuthManagerAppLayer.Conversations
{
    public class AttemptLoginResponderConversation : ResponderConversation
    {
        public AttemptLoginMessage ReceivedMessage { get; set; }
        public TokenBank TokenBank { get; set; }
        
        private bool LoginSuccessful { get; set; }
        private Guid Token { get; set; }

        public AttemptLoginResponderConversation(TokenBank tokenBank)
        {
            LoginSuccessful = false;
            TokenBank = tokenBank;
            Token = Guid.Empty;
        }

        protected override void ProcessFailure()
        {
            throw new NotImplementedException();
        }

        protected override void ProcessReceivedMessage(Message message)
        {
            ReceivedMessage = (AttemptLoginMessage)message;
            var userId = UserDataAccess.GetUserId(ReceivedMessage.Username, ReceivedMessage.Password);
            if (userId != 0)
            {
                SetAuthToken();
            }
        }

        private void SetAuthToken()
        {
            LoginSuccessful = true;
            Token = Guid.NewGuid();
            TokenBank.AddToken(Token);
        }

        protected override Message CreateReply()
        {
            return new TokenVerifyMessage
            {
                ConversationId = ReceivedMessage.ConversationId,
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                IsAuthorized = LoginSuccessful,
                AuthToken = Token
            };
        }
    }
}
