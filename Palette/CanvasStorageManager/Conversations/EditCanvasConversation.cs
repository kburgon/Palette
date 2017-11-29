using CanvasStorageManager.DataPersistence;
using CommunicationSubsystem.Conversations;
using Messages;

namespace CanvasStorageManager.Conversations
{
    internal class EditCanvasConversation : ResponderConversation
    {
        private CanvasRepository _repo;

        public EditCanvasConversation(CanvasRepository repo)
        {
            _repo = repo;
        }

        protected override void ProcessFailure() { }

        protected override void ProcessReceivedMessage(Message message)
        {
            throw new System.NotImplementedException();
        }

        protected override Message CreateReply()
        {
            throw new System.NotImplementedException();
        }
    }
}
