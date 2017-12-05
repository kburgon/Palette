using CommunicationSubsystem.Conversations;
using System;
using Messages;
using CommunicationSubsystem;

namespace DisplayAppLayer.Conversations
{
    class SubscribeCanvasInitiatorConversation : InitiatorConversation
    {
        protected override bool CheckMessageType(EnvelopeQueue queue)
        {
            throw new NotImplementedException();
        }

        protected override Message CreateRequest()
        {
            throw new NotImplementedException();
        }

        protected override void ProcessFailure()
        {
            throw new NotImplementedException();
        }

        protected override bool ProcessReply(Message receivedMessage)
        {
            throw new NotImplementedException();
        }
    }
}
