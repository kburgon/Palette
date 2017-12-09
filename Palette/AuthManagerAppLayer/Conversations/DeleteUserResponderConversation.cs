using System;
using CommunicationSubsystem.Conversations;
using Messages;

namespace AuthManagerAppLayer.Conversations
{
    public class DeleteUserResponderConversation : ResponderConversation
    {
        public TokenBank TokenBank { get; }
        private Guid AuthToken { get; set; }

        public DeleteUserResponderConversation()
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
            var deleteMessage = (DeleteUserMessage) message;
            if (TokenBank.EncryptedTokenExists(deleteMessage.AuthToken))
            {
                AuthToken = TokenBank.GetDecryptedToken(deleteMessage.AuthToken);
                UserDataAccess.DeleteUser(UserDataAccess.GetUserId(deleteMessage.Username, deleteMessage.Password));
            }
        }

        protected override Message CreateReply()
        {
            return new TokenVerifyMessage
            {
                ConversationId = ReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                IsAuthorized = AuthToken != Guid.NewGuid(),
                AuthToken = AuthToken
            };
        }
    }
}
