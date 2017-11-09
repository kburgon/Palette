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

        public Conversation CreateConversation(ConversationType type)
        {
            switch (type)
            {
                case ConversationType.CreateCanvas:
                    return CreateCanvasConvsersation();
                case ConversationType.EditCanvas:
                    return EditCanvasConversation();
                case ConversationType.ReadCanvas:
                    return GetCanvasListConversation();
                case ConversationType.AssignCanvas:
                case ConversationType.GetDisplay:
                case ConversationType.RegisterDisplay:
                case ConversationType.SubscribeCanvas:
                case ConversationType.UnassignCanvas:
                    throw new Exception(); //TODO: This should be an IncorrectMessageException
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
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

        public override void Initialize() { }
    }
}
