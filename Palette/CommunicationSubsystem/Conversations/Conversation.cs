using Messages;
using System;
using System.Threading.Tasks;

namespace CommunicationSubsystem.Conversations
{
    public abstract class Conversation
    {
        public Guid ProcessId { get; set; }
        public Tuple<Guid, short> ConversationId { get; set; }
        public EnvelopeQueue EnvelopeQueue { get; }
        public UdpCommunicator Communicator { get; set; }

        protected Task _conversationExecution;
        protected static UdpCommunicator _communicator;

        protected Conversation()
        {
            EnvelopeQueue = new EnvelopeQueue();
        }

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

        public bool IsEnded()
        {
            return EnvelopeQueue.EndOfConversation;
        }
    }
}
