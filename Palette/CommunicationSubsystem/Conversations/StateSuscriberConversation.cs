using System;
using System.Net;
using Messages;

namespace CommunicationSubsystem.Conversations
{
    public abstract class StateSubscriberConversation : Conversation
    {
        public IPEndPoint DestinationEndpoint { get; set; }
        private readonly Tuple<Guid, short> _conversationId = new Tuple<Guid, short>(Guid.NewGuid(), 1);

        protected override void StartConversation()
        {
            var request = CreateRequest();
            Send(Package(request));
            ProcessStateUpdates();
        }

        private Envelope Package(Message message)
        {
            message.ConversationId = _conversationId;
            return new Envelope
            {
                RemoteEP = DestinationEndpoint,
                Message = message
            };
        }

        protected abstract Message CreateRequest();
        protected abstract void ProcessStateUpdates();
    }
}
