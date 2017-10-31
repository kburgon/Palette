using System;
using System.Collections.Generic;
using System.Text;
using Messages;

namespace CommunicationSubsystem.Conversations
{
    public abstract class Conversation
    {
        public Tuple<short, short> ConversationId { get; set; }

        public abstract void Execute();

        protected abstract void ProcessFailure();

        public virtual void GetDataFromMessage(Message message)
        {
            return;
        }
    }
}
