using System;
using CommunicationSubsystem.Conversations;
using Messages;

namespace AuthManagerAppLayer.Conversations
{
    public class CreateUserResponderConversation : ResponderConversation
    {
        public TokenBank TokenBank { get; }
        private Guid AuthToken { get; set; }

        public CreateUserResponderConversation()
        {
            TokenBank = TokenBank.GetInstance();
            AuthToken = Guid.Empty;
        }

        protected override void ProcessFailure()
        {
            throw new NotImplementedException();
        }

        protected override void ProcessReceivedMessage(Message message)
        {
            var createUserMessage = (CreateUserMessage)message;
            if (TokenBank.TokenExists(createUserMessage.AuthToken))
            {
                UserDataAccess.CreateUser(createUserMessage.Username, createUserMessage.Password);
                AuthToken = createUserMessage.AuthToken;
            }
        }

        protected override Message CreateReply()
        {
            return new TokenVerifyMessage
            {
                IsAuthorized = AuthToken != Guid.Empty,
                AuthToken = AuthToken,
                ConversationId = ReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1)
            };
        }
    }
}
