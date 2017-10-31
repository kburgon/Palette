using System;
using System.Collections.Generic;
using System.Text;

namespace CommunicationSubsystem.Conversations.StateConversations
{
    public abstract class StateConversation : Conversation
    {
        protected override void StartConversation()
        {
            ProcessReceivedMessage();
            CreateAuthRequest();
            ProcessAuthReply();
            CreateFirstUpdatedState();
            CreateRequest();
            ProcessReply();
            CreateSecondUpdatedState();

            // TODO: Add handling for conversation failures.
            //ProcessFailure();
        }

        protected abstract void ProcessReceivedMessage();
        protected abstract void CreateAuthRequest();
        protected abstract void ProcessAuthReply();
        protected abstract void CreateFirstUpdatedState();
        protected abstract void CreateRequest();
        protected abstract void ProcessReply();
        protected abstract void CreateSecondUpdatedState();
    }
}
