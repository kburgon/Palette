using System;
using System.Collections.Generic;
using System.Text;

namespace CommunicationSubsystem.Conversations.ProxyConversations
{
    public class ProxyConversation : Conversation
    {
        public override void Execute()
        {
            ProcessReceivedMessage();
            CreateRequest();
            ProcessReplyFromRequest();
            CreateReply();

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
