using Messages;

namespace CommunicationSubsystem.Conversations
{
    public abstract class ResponderConversation : Conversation
    {
        protected override void StartConversation()
        {
            var message = ReceivedEnvelope.Message;
            ProcessReceivedMessage( message );
            var reply = CreateReply();
            Communicator.Send(new Envelope
            {
                RemoteEP = ReceivedEnvelope.RemoteEP,
                Message = reply
            });

            EnvelopeQueue.EndOfConversation = true;

            // TODO: Add handling for conversation failures.
            //ProcessFailure();
        }

        protected abstract void ProcessReceivedMessage(Message message);
        protected abstract Message CreateReply();
    }
}
