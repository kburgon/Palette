using System;
using CanvasStorageManager.DataPersistence;
using CommunicationSubsystem.Conversations;
using Messages;
using System.Net;

namespace CanvasStorageManager.Conversations
{
    internal class DeleteCanvasConversation : ResponderConversation
    {
        public CanvasRepository _canvasRepo;

        public DeleteCanvasConversation()
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
            var deleteMessage = message as DeleteCanvasMessage;
            if (deleteMessage == null)
            {
                HandleWrongMessage();
                return;
            }
            _canvasRepo.Delete(deleteMessage.CanvasId);
        }

        private static void HandleWrongMessage() { }

        protected override Message CreateReply()
        {
            var message = EnvelopeQueue.Dequeue();
            var messageNum = message.Message.MessageNumber.Item2 + 1;
            return new CanvasMessage
            {
                CanvasId = (message.Message as DeleteCanvasMessage).CanvasId,
                ConversationId = message.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(message.Message.MessageNumber.Item1, (short)messageNum)
            };
        }
    }
}
