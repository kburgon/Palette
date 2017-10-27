using System;
using System.Collections.Generic;
using System.Text;

namespace CommunicationSubsystem.Conversations.ResponderConversations
{
    public abstract class ResponderConversation : Conversation
    {
        public override void Execute()
        {
            throw new NotImplementedException();
        }
        protected abstract void ProcessReceivedMessage();
        protected abstract void CreateReply();
    }
}
