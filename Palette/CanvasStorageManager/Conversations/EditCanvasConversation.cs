using CanvasStorageManager.DataPersistence;
using CommunicationSubsystem.Conversations;
using Messages;

namespace CanvasStorageManager.Conversations
{
    internal class EditCanvasConversation : ResponderConversation
    {
        public CanvasRepository _canvasRepo;


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
