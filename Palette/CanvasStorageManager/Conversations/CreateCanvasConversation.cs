using System;
using CanvasStorageManager.DataPersistence;
using CommunicationSubsystem.Conversations;
using Messages;
using SharedAppLayer.Entitities;
using System.Net;

namespace CanvasStorageManager.Conversations
{
    internal class CreateCanvasConversation : ResponderConversation
    {
        public CanvasRepository _canvasRepo;
        private Canvas _createdCanvas;

        public CreateCanvasConversation()
        {
            var _dataStore = new FileDataStore();
            _canvasRepo = new CanvasRepository(_dataStore);
            RequestEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
        }

        protected override void ProcessFailure()
        {
            throw new NotImplementedException();
        }

        protected override void ProcessReceivedMessage(Message message)
        {
            if (!(message is CreateCanvasMessage)) HandleWrongMessage();
            _createdCanvas = _canvasRepo.CreateNew();
        }

        private static void HandleWrongMessage() { }

        protected override Message CreateReply()
        {
            var message = EnvelopeQueue.Dequeue();
            var messageNum = message.Message.MessageNumber.Item2 + 1;
            return new CanvasMessage
            {
                CanvasId = _createdCanvas.CanvasId,
                ConversationId = message.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(message.Message.MessageNumber.Item1, (short)messageNum)
            };
        }
    }
}
