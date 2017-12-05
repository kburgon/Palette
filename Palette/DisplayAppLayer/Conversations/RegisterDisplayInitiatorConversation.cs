using CommunicationSubsystem;
using CommunicationSubsystem.Conversations;
using Messages;

namespace DisplayAppLayer.Conversations
{
    class RegisterDisplayInitiatorConversation : InitiatorConversation
    {
        protected override bool CheckMessageType(EnvelopeQueue queue)
        {
            throw new System.NotImplementedException();
        }

        protected override Message CreateRequest()
        {
            throw new System.NotImplementedException();
        }

        protected override void ProcessFailure()
        {
            throw new System.NotImplementedException();
        }

        protected override bool ProcessReply(Message receivedMessage)
        {
            throw new System.NotImplementedException();
        }
    }
}
