using CommunicationSubsystem;
using CommunicationSubsystem.Conversations;
using Messages;

namespace CanvasManagerAppLayer.Conversations
{
    internal class DisplayUpdateConversation : InitiatorConversation
    {
        private readonly BrushStrokeMessage _brushMessage;

        public DisplayUpdateConversation(BrushStrokeMessage brushMessage)
        {
            _brushMessage = brushMessage;
        }

        protected override Message CreateRequest()
        {
            return _brushMessage;
        }

        protected override bool ProcessReply(Message receivedMessage)
        {
            return true;
        }

        protected override bool CheckMessageType(EnvelopeQueue queue)
        {
            return true;
        }
    }
}