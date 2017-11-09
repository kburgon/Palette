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
            ConversationId = new Tuple<Guid, short>(ProcessId, 1);
            CreateRequest();
            while (EnvelopeQueue.GetCount() == 0) { }
            ProcessReply();
        }

        protected abstract void ValidateConversationState();
        protected abstract void CheckProcessState();
        protected abstract void CreateRequest();
        protected abstract void ProcessReply();
    }
}
