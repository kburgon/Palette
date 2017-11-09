using System;
using System.Collections.Generic;
using System.Text;
using Messages;

namespace CommunicationSubsystem.Conversations.ResponderConversations
{
    public abstract class ResponderConversation : Conversation
    {
        protected override void StartConversation()
        {
            var message = GetMessageFromQueue();
            ProcessReceivedMessage( message );
            var reply = CreateReply();
            SendReply( reply );

            EnvelopeQueue.EndOfConversation = true;

            // TODO: Add handling for conversation failures.
            //ProcessFailure();
        }

        private void SendReply(Message reply)
        {
            
        }

        private Message GetMessageFromQueue()
        {
            return null;
        }

        protected abstract void ProcessReceivedMessage(Message message);
        protected abstract Message CreateReply();
    }
}
