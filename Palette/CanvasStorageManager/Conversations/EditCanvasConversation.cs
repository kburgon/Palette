using CanvasStorageManager.DataPersistence;
using CommunicationSubsystem.Conversations;
using Messages;
using System.Net;

namespace CanvasStorageManager.Conversations
{
    internal class EditCanvasConversation : ResponderConversation
    {
        public CanvasRepository _canvasRepo;

        public EditCanvasConversation()
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
            if (message.GetType() != typeof(BrushStrokeMessage))
                return;
        }

        protected override Message CreateReply()
        {
            throw new System.NotImplementedException();
        }
    }
}
