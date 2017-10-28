using System;
using System.Collections.Generic;
using System.Text;

namespace CommunicationSubsystem.Conversations.ResponderConversations
{
    public abstract class ResponderConversation : Conversation
    {
        public override void Execute()
        {
            ProcessReceivedMessage();
            CreateReply();

            // TODO: Add handling for conversation failures.
            //ProcessFailure();
        }
        protected abstract void ProcessReceivedMessage();
        protected abstract void CreateReply();
    }
}
