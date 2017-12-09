using CommunicationSubsystem;
using CommunicationSubsystem.Conversations;
using Messages;

namespace CanvasManagerAppLayer.Conversations
{
    public class SubscribeCanvasStateConversation : StateConversation
    {
        protected override bool CheckMessageType(EnvelopeQueue queue)
        {
            throw new System.NotImplementedException();
        }

        protected override Message CreateAuthRequest()
        {
            throw new System.NotImplementedException();
        }

        protected override void ProcessReceivedMessage()
        {
            if (InitialReceivedEnvelope.Message.GetType() != typeof(SubscriberCanvasMessage))
                return;

            base.ProcessReceivedMessage();
        }

        protected override void ProcessFailure()
        {
            base.ProcessFailure();
        }

        protected override Message CreateRequest()
        {
            throw new System.NotImplementedException();
        }

        protected override Message CreateUpdate()
        {
            throw new System.NotImplementedException();
        }

        protected override void ProcessAuthReply(Message replyMessage)
        {
            throw new System.NotImplementedException();
        }

        protected override bool ProcessReply(Message receivedMessage)
        {
            throw new System.NotImplementedException();
        }
    }
}
