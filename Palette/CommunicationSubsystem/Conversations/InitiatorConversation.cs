using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CommunicationSubsystem.Conversations.InitiatorConversations
{
    public abstract class InitiatorConversation : Conversation
    {
        public IPEndPoint RemoteEndPoint { get; set; }

        protected override void StartConversation()
        {
            EnvelopeQueue.EndOfConversation = true;
            throw new NotImplementedException();
        }

        protected abstract void ValidateConversationState();
        protected abstract void CheckProcessState();
        protected abstract void CreateRequest();
        protected abstract void ProcessReply();
    }
}
