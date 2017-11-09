namespace CommunicationSubsystem.Conversations
{
    public abstract class StateConversation : Conversation
    {
        public Envelope InitialReceivedEnvelope { get; set; }

        protected override void StartConversation()
        {
            ProcessReceivedMessage();
            //CreateAuthRequest();
            //ProcessAuthReply();
            CreateRequest();
            ProcessReply();
            CreateUpdate();
            EnvelopeQueue.EndOfConversation = true;
        }

        protected virtual void ProcessReceivedMessage()
        {
            ConversationId = InitialReceivedEnvelope.Message.ConversationId;
        }

        protected abstract void CreateAuthRequest();
        protected abstract void ProcessAuthReply();
        protected abstract void CreateRequest();
        protected abstract void ProcessReply();
        protected abstract void CreateUpdate();
    }
}
