using CommunicationSubsystem.Conversations;
using Messages;

namespace CanvasManagerAppLayer.Conversations
{
    public class EditCanvasConversation : ResponderConversation
    {
        protected override void ProcessReceivedMessage(Message message)
        {
            if (!(message is BrushStrokeMessage brushMessage)) return;
            var storageConvo = new StoreEditConversation(brushMessage);
            // send to dispatcher
            var displayConvo = new DisplayUpdateConversation(brushMessage);
            // send to dispatcher
        }

        protected override Message CreateReply()
        {
            return null;
        }
    }
}
