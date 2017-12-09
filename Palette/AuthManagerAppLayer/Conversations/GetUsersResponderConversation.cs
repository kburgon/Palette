using System;
using System.Collections.Generic;
using CommunicationSubsystem.Conversations;
using Messages;

namespace AuthManagerAppLayer.Conversations
{
    public class GetUsersResponderConversation : ResponderConversation
    {
        public TokenBank TokenBank { get; }
        public Guid AuthToken { get; set; }
        private List<User> Users { get; set; }

        public GetUsersResponderConversation()
        {
            TokenBank = TokenBank.GetInstance();
            AuthToken = Guid.Empty;
            Users = new List<User>();
        }

        protected override void ProcessFailure()
        {
            throw new NotImplementedException();
        }

        protected override void ProcessReceivedMessage(Message message)
        {
            var usersMessage = (GetUserListMessage) message;
            if (TokenBank.EncryptedTokenExists(usersMessage.AuthToken))
            {
                AuthToken = TokenBank.GetDecryptedToken(usersMessage.AuthToken);
                Users = UserDataAccess.GetUsers();
            }
        }

        protected override Message CreateReply()
        {
            return new UserListMessage
            {
                ConversationId = ReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                Users = Users
            };
        }
    }
}
