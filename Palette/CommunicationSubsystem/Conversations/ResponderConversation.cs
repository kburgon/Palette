using Messages;
using System;
using System.Net;

namespace CommunicationSubsystem.Conversations
{
    public abstract class ResponderConversation : Conversation
    {
        protected IPEndPoint RequestEP;

        protected override void StartConversation()
        {
            var message = ReceivedEnvelope.Message;
            ProcessId = message.ConversationId.Item1;
            ProcessReceivedMessage( message );

            var reply = CreateReply();
            var envelope = new Envelope
            {
                RemoteEP = RequestEP,
                Message = reply
            };

            _communicator.Send(envelope);
            
            EnvelopeQueue.EndOfConversation = true;
        }

        protected abstract void ProcessReceivedMessage(Message message);
        protected abstract Message CreateReply();
    }
}
