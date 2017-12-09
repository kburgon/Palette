using CommunicationSubsystem.Conversations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using CanvasStorageManager.DataPersistence;
using System.Net;
using SharedAppLayer.Entitities;

namespace CanvasStorageManager.Conversations
{
    public class RetrieveCanvasResponderConversation : ResponderConversation
    {
        public int CanvasId;
        private CanvasRepository _canvasRepo;
        public Canvas Canvas;
        private Tuple<Guid, short> NextMessageNumber;

        public RetrieveCanvasResponderConversation()
        {
            var _dataStore = new FileDataStore();
            _canvasRepo = new CanvasRepository(_dataStore);
            RequestEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
        }
        protected override Message CreateReply()
        {
            var message = new SendCanvasMessage()
            {
                ConversationId = this.ConversationId,
                MessageNumber = NextMessageNumber,
                Canvas = this.Canvas
            };

            return message;
        }

        protected override void ProcessReceivedMessage(Message receivedMessage)
        {
            if (receivedMessage.GetType() != typeof(SubscriberCanvasMessage))
                return;

            var message = (SubscriberCanvasMessage)receivedMessage;
            CanvasId = message.CanvasId;
            NextMessageNumber = new Tuple<Guid, short>(message.MessageNumber.Item1, (short)(NextMessageNumber.Item2 + 1));

            Canvas = _canvasRepo.GetCanvas(CanvasId);
        }

        protected override void ProcessFailure()
        {
            base.ProcessFailure();
        }
    }
}
