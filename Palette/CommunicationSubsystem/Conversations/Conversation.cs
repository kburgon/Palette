using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Messages;

namespace CommunicationSubsystem.Conversations
{
    public abstract class Conversation
    {
        public Guid ProcessId { get; set; }
        public Tuple<Guid, short> ConversationId { get; set; }
        public EnvelopeQueue EnvelopeQueue { get; set; }
        public UdpCommunicator Communicator { get; set; }

        protected Task _conversationExecution;
        protected static UdpCommunicator _communicator;

        public void Execute()
        {
            _communicator = new UdpCommunicator();
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
