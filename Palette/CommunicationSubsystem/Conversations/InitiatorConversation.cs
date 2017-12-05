using System;
using System.Net;
using System.Threading;
using Messages;

namespace CommunicationSubsystem.Conversations
{
    public abstract class InitiatorConversation : Conversation
    {
        public IPEndPoint RemoteEndPoint { get; set; }

        public InitiatorConversation(int waitTimeMs = 5000) 
            : base(waitTimeMs)
        {
            ProcessId = Guid.NewGuid();
            ConversationId = new Tuple<Guid, short>(ProcessId, 1);
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
            if (EnvelopeQueue.GetCount() != 0 && CheckMessageType(EnvelopeQueue))
            {
                sendReceiveSuccess = AttemptProcessReply();
            }

            return sendReceiveSuccess;
        }

        private bool AttemptProcessReply()
        {
            try
            {
                CheckMessageType(EnvelopeQueue);
                var envelope = EnvelopeQueue.Dequeue();
                if (ProcessReply(envelope.Message))
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected abstract Message CreateRequest();
        protected abstract bool ProcessReply(Message receivedMessage);
        protected abstract bool CheckMessageType(EnvelopeQueue queue);
    }
}
