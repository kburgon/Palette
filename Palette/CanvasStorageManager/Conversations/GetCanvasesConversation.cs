using System.Collections.Generic;
using CanvasStorageManager.DataPersistence;
using CommunicationSubsystem.Conversations;
using Messages;
using SharedAppLayer.Entitities;
using System;
using System.Net;

namespace CanvasStorageManager.Conversations
{
    internal class GetCanvasesConversation : ResponderConversation
    {
        public CanvasRepository _canvasRepo;
        private IEnumerable<Canvas> _canvases;

        public GetCanvasesConversation()
        {
            var _dataStore = new FileDataStore();
            _canvasRepo = new CanvasRepository(_dataStore);
            RequestEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
        }

        protected override void ProcessFailure()
        {
            base.ProcessFailure();
        }

        protected override void ProcessReceivedMessage(Message message)
        {
            _canvases = _canvasRepo.GetAll();
        }

        protected override Message CreateReply()
        {
            var message = EnvelopeQueue.Dequeue();
            var messageNum = message.Message.MessageNumber.Item2 + 1;
            return new CanvasListMessage
            {
                Canvases = _canvases,
                ConversationId = message.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(message.Message.MessageNumber.Item1, (short)messageNum)
            };
        }
    }
}
