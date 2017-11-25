using System;
using System.Collections.Generic;
using CommunicationSubsystem.Conversations;
using Messages;

namespace AuthManagerAppLayer.Conversations
{
    public class GetUsersResponderConversation : ResponderConversation
    {
        public TokenBank TokenBank { get; set; }
        public Guid AuthToken { get; set; }
        private List<User> Users { get; set; }

        public GetUsersResponderConversation(TokenBank tokenBank)
        {
            TokenBank = tokenBank;
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
            if (TokenBank.TokenExists(usersMessage.AuthToken))
            {
                AuthToken = usersMessage.AuthToken;
                Users = UserDataAccess.GetUsers();
            }
        }

        protected override Message CreateReply()
        {
            return new UserListMessage
            {
                ConversationId = ReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                AuthToken = AuthToken,
                Users = Users
            };
        }
    }
}
