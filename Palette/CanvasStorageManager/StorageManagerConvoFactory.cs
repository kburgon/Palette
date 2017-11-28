using System;
using CanvasStorageManager.Conversations;
using CanvasStorageManager.DataPersistence;
using CommunicationSubsystem.Conversations;
using CommunicationSubsystem.ConversationFactories;

namespace CanvasStorageManager
{
    public class StorageManagerConvoFactory : ConversationFactory
    {
        private readonly FileDataStore _dataStore;

        public StorageManagerConvoFactory()
        {
            _dataStore = new FileDataStore();
        }

        public override Conversation CreateFromMessageType(Type message)
        {
            throw new NotImplementedException();
        }

        private CreateCanvasConvsersation CreateCanvasConvsersation()
        {
            var repo = CreateCanvasRepo();
            return new CreateCanvasConvsersation( repo );
        }

        private EditCanvasConversation EditCanvasConversation()
        {
            var repo = CreateCanvasRepo();
            return new EditCanvasConversation( repo );
        }

        private GetCanvasesConversation GetCanvasListConversation()
        {
            var repo = CreateCanvasRepo();
            return new GetCanvasesConversation( repo );
        }

        private CanvasRepository CreateCanvasRepo()
        {
            return new CanvasRepository( _dataStore );
        }
    }
}
