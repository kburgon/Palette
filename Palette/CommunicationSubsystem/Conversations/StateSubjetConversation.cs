using System;
using System.Net;
using Messages;

namespace CommunicationSubsystem.Conversations
{
    public abstract class StateSubjetConversation : Conversation
    {
        private IPEndPoint _sourceEndpoint;
        private Tuple<Guid, short> _conversationId;
        public Envelope InitialReceivedEnvelope { get; set; }

        protected override void StartConversation()
        {
            var envelope = GetNextEnvelope();
            _sourceEndpoint = envelope.RemoteEP;
            _conversationId = envelope.Message.ConversationId;
            StartStateUpdates( envelope.Message );
        }

        protected void SendStatusUpdate(Message message)
        {
            message.ConversationId = _conversationId;
            var envelope = new Envelope
            {
                RemoteEP = _sourceEndpoint,
                Message = message
            };
            Send( envelope );
        }

        protected abstract void StartStateUpdates(Message message);
    }
}
