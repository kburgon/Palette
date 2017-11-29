using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Messages;

namespace CommunicationSubsystem.Conversations.ResponderConversations
{
    public abstract class ResponderConversation : Conversation
    {
        private IPEndPoint _sourceEndpoint;
        private Tuple<Guid, short> _conversationId;

        protected override void StartConversation()
        {
            var envelope = GetNextEnvelope();
            _sourceEndpoint = envelope.RemoteEP;
            _conversationId = envelope.Message.ConversationId;
            ProcessReceivedMessage( envelope.Message );
            var reply = CreateReply();
            SendReply( reply );
            End();

            // TODO: Add handling for conversation failures.
            //ProcessFailure();
        }

        private void SendReply(Message reply)
        {
            reply.ConversationId = _conversationId;
            var envelope = new Envelope
            {
                Message = reply,
                RemoteEP = _sourceEndpoint
            };
            Send(envelope);
        }

        protected abstract void ProcessReceivedMessage(Message message);
        protected abstract Message CreateReply();
    }
}
