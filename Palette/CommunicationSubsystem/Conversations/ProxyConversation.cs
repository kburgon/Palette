using System;

namespace CommunicationSubsystem.Conversations.ProxyConversations
{
    public class ProxyConversation : Conversation
    {
        protected override void StartConversation()
        {
            ProcessReceivedMessage();
            CreateRequest();
            ProcessReplyFromRequest();
            CreateReply();

            EnvelopeQueue.EndOfConversation = true;

            // TODO: Add handling for conversation failures.
            //ProcessFailure();
        }

        protected override void ProcessFailure()
        {
            throw new NotImplementedException();
        }

        private void ProcessReceivedMessage()
        {
            throw new NotImplementedException();
        }

        private void CreateRequest()
        {
            throw new NotImplementedException();
        }

        private void ProcessReplyFromRequest()
        {
            throw new NotImplementedException();
        }

        private void CreateReply()
        {
            throw new NotImplementedException();
        }
    }
}
