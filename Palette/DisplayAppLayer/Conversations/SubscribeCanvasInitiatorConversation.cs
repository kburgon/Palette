using CommunicationSubsystem.Conversations;
using System;
using Messages;

namespace DisplayAppLayer.Conversations
{
    class SubscribeCanvasInitiatorConversation : InitiatorConversation
    {
        protected override Message CreateRequest()
        {
            throw new NotImplementedException();
        }

        protected override void ProcessFailure()
        {
            throw new NotImplementedException();
        }

        protected override void ProcessReply(Message receivedMessage)
        {
            throw new NotImplementedException();
        }
    }
}
