using System.Collections.Generic;
using CanvasStorageManager.DataPersistence;
using CommunicationSubsystem.Conversations;
using Messages;
using SharedAppLayer.Entitities;

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
        }

        protected override void ProcessFailure()
        {
            throw new System.NotImplementedException();
        }

        protected override void ProcessReceivedMessage(Message message)
        {
            _canvases = _canvasRepo.GetAll();
        }

        protected override Message CreateReply()
        {
            return new CanvasListMessage
            {
                Canvases = _canvases
            };
        }
    }
}
