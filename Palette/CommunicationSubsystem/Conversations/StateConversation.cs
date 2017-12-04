using System;
using System.Net;
using System.Threading;
using Messages;

namespace CommunicationSubsystem.Conversations
{
    public abstract class StateConversation : Conversation
    {
        public Envelope InitialReceivedEnvelope { get; set; }
        public IPEndPoint RequestEP { get; set; }
        public IPEndPoint AuthEP { get; set; }

        public override void GetDataFromMessage(Message message)
        {
            base.GetDataFromMessage(message);
        }

        protected override void StartConversation()
        {
            if (EnvelopeQueue.GetCount() == 1)
                InitialReceivedEnvelope = EnvelopeQueue.Dequeue();

            ProcessId = InitialReceivedEnvelope.Message.ConversationId.Item1;

            ProcessReceivedMessage();

            var authEnvelope = new Envelope()
            {
                RemoteEP = AuthEP,
                Message = InitialReceivedEnvelope.Message
            };

            var authSendreceiveSuccess = false;
            for (int receiveAttempt = 0; receiveAttempt < 30 && !authSendreceiveSuccess; receiveAttempt++)
            {
                authSendreceiveSuccess = AttemptAuthVerification(authEnvelope);
            }

            if (!authSendreceiveSuccess)
            {
                ProcessFailure();
                return;
            }

            var message = CreateRequest();
            var envelope = new Envelope()
            {
                RemoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11900),
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
                return;
            }

            var updateEnvelope = new Envelope()
            {
                RemoteEP = InitialReceivedEnvelope.RemoteEP,
                Message = CreateUpdate()
            };

            _communicator.Send(updateEnvelope);

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

        private bool AttemptAuthVerification(Envelope envelope)
        {
            var sendReceiveSuccess = false;
            _communicator.Send(envelope);
            Thread.Sleep(GetMessageWaitAmount);
            if (EnvelopeQueue.GetCount() != 0)
            {
                sendReceiveSuccess = AttemptAuthProcessReply();
            }

            return sendReceiveSuccess;
        }

        private bool AttemptAuthProcessReply()
        {
            try
            {
                var envelope = EnvelopeQueue.Dequeue();
                ProcessAuthReply(envelope.Message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected virtual void ProcessReceivedMessage()
        {
            ConversationId = InitialReceivedEnvelope.Message.ConversationId;
        }

        protected abstract Message CreateAuthRequest();
        protected abstract void ProcessAuthReply(Message replyMessage);
        protected abstract Message CreateRequest();
        protected abstract void ProcessReply(Message receivedMessage);
        protected abstract Message CreateUpdate();
    }
}
