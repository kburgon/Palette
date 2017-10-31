using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Messages;

namespace CommunicationSubsystem.Conversations
{
    public abstract class Conversation
    {
        public Tuple<short, short> ConversationId { get; set; }

        protected Task _conversationExecution;

        public void Execute()
        {
            _conversationExecution = Task.Factory.StartNew(StartConversation);
        }

        protected abstract void StartConversation();

        protected abstract void ProcessFailure();

        public virtual void GetDataFromMessage(Message message)
        {
            return;
        }
    }
}
