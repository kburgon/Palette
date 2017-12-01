using System;
using System.Net;
using System.Threading;
using Messages;

namespace CommunicationSubsystem.Conversations
{
    public abstract class InitiatorConversation : Conversation
    {
        public IPEndPoint RemoteEndPoint { get; set; }

        public InitiatorConversation(int waitTimeMs = 100) 
            : base(waitTimeMs)
        {

        }

        protected override void StartConversation()
        {
            ConversationId = new Tuple<Guid, short>(ProcessId, 1);
            var message = CreateRequest();
            var envelope = new Envelope()
            {
                RemoteEP = RemoteEndPoint,
                Message = message
            };

            var sendReceiveSuccess = false;
            for (int receiveAttempt = 0; receiveAttempt < 30 && !sendReceiveSuccess; receiveAttempt++)
            {
                sendReceiveSuccess = AttemptSendReceive(envelope);
            }

            if (!sendReceiveSuccess)
            {
                ProcessFailure();
            }

            EnvelopeQueue.EndOfConversation = true;
        }

        private bool AttemptSendReceive(Envelope envelope)
        {
            var sendReceiveSuccess = false;
            _communicator.Send(envelope);
            Thread.Sleep(GetMessageWaitAmount);
            if (EnvelopeQueue.GetCount() != 0)
            {
                sendReceiveSuccess = AttemptProcessReply();
            }

            return sendReceiveSuccess;
        }

        private bool AttemptProcessReply()
        {
            try
            {
                var envelope = EnvelopeQueue.Dequeue();
                ProcessReply(envelope.Message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected abstract Message CreateRequest();
        protected abstract void ProcessReply(Message receivedMessage);
    }
}
