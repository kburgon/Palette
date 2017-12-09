using CommunicationSubsystem;
using CommunicationSubsystem.Conversations;
using Messages;
using System;
using System.Net;
using log4net;

namespace CanvasManagerAppLayer.Conversations
{
    public class CreateCanvasStateConversation : StateConversation
    {
        public int CanvasId { get; set; }

        private static readonly ILog Logger = LogManager.GetLogger(typeof(CreateCanvasStateConversation));

        public CreateCanvasStateConversation()
        {
            RequestEp = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12500);
        }

        protected override void ProcessFailure()
        {
            base.ProcessFailure();
        }

        protected override void ProcessReceivedMessage()
        {
            if (InitialReceivedEnvelope.Message.GetType() != typeof(CreateCanvasMessage))
                return;

            base.ProcessReceivedMessage();
        }

        protected override Message CreateRequest()
        {
            var stepNumber = InitialReceivedEnvelope.Message.MessageNumber.Item2;
            var message = new CreateCanvasMessage()
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber)
            };

            return message;
        }

        protected override bool ProcessReply(Message receivedMessage)
        {
            var message = receivedMessage;
            if (message.GetType() == typeof(CanvasMessage))
            {
                CanvasId = (message as CanvasMessage).CanvasId;
                return true;
            }
            return false;
        }

        protected override Message CreateUpdate()
        {
            InitialReceivedEnvelope.RemoteEP.Port = 11900;
            Logger.InfoFormat("Canvas Id: {0}", CanvasId);
            var stepNumber = Convert.ToInt16(InitialReceivedEnvelope.Message.MessageNumber.Item2 + 1);
            var message = new CanvasMessage()
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber),
                CanvasId = this.CanvasId
            };

            return message;
        }

        protected override bool CheckMessageType(EnvelopeQueue queue)
        {
            if (queue.GetMessageType(typeof(CanvasMessage)) == typeof(CanvasMessage))
                return true;

            return false;
        }
    }
}
