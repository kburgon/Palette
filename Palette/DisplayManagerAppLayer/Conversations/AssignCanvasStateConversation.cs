using CommunicationSubsystem;
using CommunicationSubsystem.Conversations;
using Messages;
using System;
using System.Net;

namespace DisplayManagerAppLayer.Conversations
{
    public class AssignCanvasStateConversation : StateConversation
    {
        public string DisplayAddress;
        public DisplayManager _displayManager;

        public AssignCanvasStateConversation()
        {
            RequestEp = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12250);
        }

        protected override bool CheckMessageType(EnvelopeQueue queue)
        {
            throw new NotImplementedException();
        }

        protected override Message CreateAuthRequest()
        {
            throw new System.NotImplementedException();
        }

        protected override Message CreateRequest()
        {
            var stepNumber = InitialReceivedEnvelope.Message.MessageNumber.Item2 + 1;
            var address = _displayManager.GetDisplayAddress((InitialReceivedEnvelope.Message as CanvasAssignMessage).DisplayId);
            RequestEp = new IPEndPoint(IPAddress.Parse(address), 12200);
            var message = new CanvasAssignMessage
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, (short)stepNumber),
                DisplayId = (InitialReceivedEnvelope.Message as CanvasAssignMessage).DisplayId,
                CanvasId = (InitialReceivedEnvelope.Message as CanvasAssignMessage).CanvasId
            };

            return message;
        }

        protected override Message CreateUpdate()
        {
            var stepNumber = Convert.ToInt16(InitialReceivedEnvelope.Message.MessageNumber.Item2 + 1);
            var message = new CanvasAssignMessage
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber)
            };

            return message;
        }

        protected override void ProcessAuthReply(Message receivedMessage)
        {
            throw new System.NotImplementedException();
        }

        protected override void ProcessFailure()
        {
            base.ProcessFailure();
        }

        protected override bool ProcessReply(Message receivedMessage)
        {
            var message = receivedMessage;

            return true;
        }
    }
}
