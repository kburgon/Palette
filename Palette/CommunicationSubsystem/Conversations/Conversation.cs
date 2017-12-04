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
        public Envelope ReceivedEnvelope { get; set; }
        public int GetMessageWaitAmount { get; set; }

        protected Task _conversationExecution;
        public UdpCommunicator _communicator;

        private ILog Logger;

        public Conversation(int waitTimeMs = 5000)
        {
            GetMessageWaitAmount = waitTimeMs;
        }

        public void Execute()
        {
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
            ConversationId = message.ConversationId;
        }
    }
}
