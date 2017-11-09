using System.Collections.Generic;
using CanvasStorageManager.DataPersistence;
using CommunicationSubsystem.Conversations.ResponderConversations;
using Messages;
using SharedAppLayer.Entitities;

namespace CanvasStorageManager.Conversations
{
    internal class GetCanvasesConversation : ResponderConversation
    {
        private readonly CanvasRepository _canvasRepository;
        private IEnumerable<Canvas> _canvases;

        public GetCanvasesConversation(CanvasRepository canvasRepository)
        {
            _canvasRepository = canvasRepository;
        }

        protected override void ProcessFailure()
        {
            throw new System.NotImplementedException();
        }

        protected override void ProcessReceivedMessage(Message message)
        {
            _canvases = _canvasRepository.GetAll();
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
