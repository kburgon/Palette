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
            var envelope = new Envelope
            {
                RemoteEP = ReceivedEnvelope.RemoteEP,
                Message = reply
            };

            for (int sendAttempt = 0; sendAttempt < 10; sendAttempt++)
            {
                Communicator.Send(envelope); 
            }

            EnvelopeQueue.EndOfConversation = true;
        }

        protected abstract void ProcessReceivedMessage(Message message);
        protected abstract Message CreateReply();
    }
}
