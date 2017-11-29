using System;
using System.Net;
using Messages;

namespace CommunicationSubsystem.Conversations.InitiatorConversations
{
    public abstract class InitiatorConversation : Conversation
    {
        public IPEndPoint DestinationIpEndPoint { get; set; }

        protected override void StartConversation()
        {
            var request = CreateRequest();
            Send(Package(request));
            var reply = GetNextEnvelope();
            ProcessReply(reply.Message);
            End();
        }

        private Envelope Package(Message request)
        {
            request.ConversationId = ConversationId;
            return new Envelope
            {
                Message = request,
                RemoteEP = DestinationIpEndPoint
            };
        }

        protected abstract void ValidateConversationState();
        protected abstract void CheckProcessState();
        protected abstract Message CreateRequest();
        protected abstract void ProcessReply(Message message);
    }
}
