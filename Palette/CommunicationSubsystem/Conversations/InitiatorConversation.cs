using System;
using System.Net;
using Messages;

namespace CommunicationSubsystem.Conversations.InitiatorConversations
{
    public abstract class InitiatorConversation : Conversation
    {
        public IPEndPoint RemoteEndPoint { get; set; }

        protected override void StartConversation()
        {
            ConversationId = new Tuple<Guid, short>(ProcessId, 1);
            var request = CreateRequest();
            Send(Package(request));
            var reply = GetReply();
            ProcessReply(reply.Message);
        }

        private Envelope GetReply()
        {
            while (EnvelopeQueue.GetCount() == 0) { }
            return EnvelopeQueue.Dequeue();
        }

        private void Send(Envelope package)
        {
            //Send to Dispatcher somehow
        }

        private Envelope Package(Message request)
        {
            return new Envelope
            {
                Message = request,
                RemoteEP = RemoteEndPoint
            };
        }

        protected abstract void ValidateConversationState();
        protected abstract void CheckProcessState();
        protected abstract Message CreateRequest();
        protected abstract void ProcessReply(Message message);
    }
}
