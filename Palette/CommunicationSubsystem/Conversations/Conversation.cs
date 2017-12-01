using Messages;
using System;
using System.Threading.Tasks;
using log4net;

namespace CommunicationSubsystem.Conversations
{
    public abstract class Conversation
    {
        public Guid ProcessId { get; set; }
        public Tuple<Guid, short> ConversationId { get; set; }
        public EnvelopeQueue EnvelopeQueue { get; set; }
        public UdpCommunicator Communicator { get; set; }
        public Envelope ReceivedEnvelope { get; set; }
        public int GetMessageWaitAmount { get; set; }

        protected Task _conversationExecution;
        protected static UdpCommunicator _communicator;

        private ILog Logger;

        public Conversation(int waitTimeMs = 100)
        {
            GetMessageWaitAmount = waitTimeMs;
            ProcessId = Guid.NewGuid();
        }

        public void Execute()
        {
            _communicator = new UdpCommunicator();
            _conversationExecution = Task.Factory.StartNew(StartConversation);
        }

        protected abstract void StartConversation();

        protected virtual void ProcessFailure()
        {
            Logger.Warn($"Conversation {this.GetType().ToString()} failed to send/receive message.");
            EnvelopeQueue.EndOfConversation = true;
        }

        public virtual void GetDataFromMessage(Message message)
        {
            return;
        }
    }
}
