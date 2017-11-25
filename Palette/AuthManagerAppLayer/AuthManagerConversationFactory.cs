using CommunicationSubsystem.ConversationFactories;
using System;
using System.Collections.Generic;
using AuthManagerAppLayer.Conversations;
using Messages;

namespace AuthManagerAppLayer
{
    class AuthManagerConversationFactory : ConversationFactory
    {
        public AuthManagerConversationFactory()
        {
            ResponderConversationTypes = new Dictionary<Type, Type>
            {
                {typeof(AttemptLoginMessage), typeof(AttemptLoginResponderConversation)},
                {typeof(CreateUserMessage), typeof(CreateUserResponderConversation)},
                {typeof(DeleteUserMessage), typeof(DeleteUserResponderConversation)},
                {typeof(GetUserListMessage), typeof(GetUsersResponderConversation)}
            };
        }

        public override void Initialize()
        {
            
        }
    }
}
