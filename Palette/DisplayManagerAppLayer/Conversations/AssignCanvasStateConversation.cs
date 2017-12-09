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
        public int DisplayId;
        public string State;

        public AssignCanvasStateConversation()
        {
            RequestEp = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12250);
            DisplayId = -1;
            DisplayAddress = String.Empty;
        }

        protected override bool CheckMessageType(EnvelopeQueue queue)
        {
            if (queue.GetMessageType(typeof(CanvasAssignMessage)) == typeof(CanvasAssignMessage))
                return true;

            return false;
        }

        protected override void ProcessReceivedMessage()
        {
            if (InitialReceivedEnvelope.Message.GetType() != typeof(CanvasAssignMessage))
                return;

            base.ProcessReceivedMessage();
            DisplayId = (InitialReceivedEnvelope.Message as CanvasAssignMessage).DisplayId;
            State = (InitialReceivedEnvelope.Message as CanvasAssignMessage).State;
        }

        protected override Message CreateRequest()
        {
            var stepNumber = InitialReceivedEnvelope.Message.MessageNumber.Item2 + 1;

            while (DisplayAddress == String.Empty) { }
            RequestEp = new IPEndPoint(IPAddress.Parse(DisplayAddress), 12200);

            var message = new CanvasAssignMessage
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, (short)stepNumber),
                DisplayId = (InitialReceivedEnvelope.Message as CanvasAssignMessage).DisplayId,
                CanvasId = (InitialReceivedEnvelope.Message as CanvasAssignMessage).CanvasId,
                State = (InitialReceivedEnvelope.Message as CanvasAssignMessage).State
            };

            return message;
        }

        protected override Message CreateUpdate()
        {
            var stepNumber = Convert.ToInt16(InitialReceivedEnvelope.Message.MessageNumber.Item2 + 1);
            var message = new CanvasAssignMessage
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber),
                DisplayId = (InitialReceivedEnvelope.Message as CanvasUnassignMessage).DisplayId,
                CanvasId = (InitialReceivedEnvelope.Message as CanvasUnassignMessage).CanvasId,
                State = this.State
            };

            return message;
        }

        protected override void ProcessFailure()
        {
            base.ProcessFailure();
        }

        protected override bool ProcessReply(Message receivedMessage)
        {
            var message = (CanvasAssignMessage)receivedMessage;

            State = message.State;

            return true;
        }
    }
}
