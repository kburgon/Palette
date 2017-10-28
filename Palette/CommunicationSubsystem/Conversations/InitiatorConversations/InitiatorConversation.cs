using System;
using System.Collections.Generic;
using System.Text;

namespace CommunicationSubsystem.Conversations.InitiatorConversations
{
    public abstract class InitiatorConversation : Conversation
    {
        public override void Execute()
        {
            throw new NotImplementedException();
        }

        protected abstract void ValidateConversationState();
        protected abstract void CheckProcessState();
        protected abstract void CreateRequest();
        protected abstract void ProcessReply();
    }
}
