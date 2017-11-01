using System;
using System.Collections.Generic;
using System.Text;
using Messages;

namespace CommunicationSubsystem.Conversations.MulticastReponderConversations
{
    public class MulticastResponderConversation : Conversation
    {
        public override void GetDataFromMessage(Message message)
        {
            throw new NotImplementedException();
        }

        protected override void StartConversation()
        {
            ProcessReceivedMessage();
            CreateMulticast();
            CreateReply();

            // TODO: Add handling for when a conversation fails.
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

        private void CreateMulticast()
        {
            throw new NotImplementedException();
        }

        private void CreateReply()
        {
            throw new NotImplementedException();
        }
    }
}
