using CommunicationSubsystem.Conversations;
using System;
using Messages;
using CommunicationSubsystem;
using SharedAppLayer.Entitities;

namespace DisplayAppLayer.Conversations
{
    class SubscribeCanvasInitiatorConversation : InitiatorConversation
    {
        public Canvas NewDisplayCanvas;


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
            base.ProcessFailure();
        }

        protected override bool ProcessReply(Message receivedMessage)
        {
            var message = (SendCanvasMessage)receivedMessage;
            NewDisplayCanvas = message.Canvas;

            return true;
        }
    }
}
