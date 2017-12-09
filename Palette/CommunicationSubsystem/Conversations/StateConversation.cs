using System;
using System.Net;
using System.Threading;
using Messages;

namespace CommunicationSubsystem.Conversations
{
    public abstract class StateConversation : Conversation
    {
        public Envelope InitialReceivedEnvelope { get; set; }
        public IPEndPoint RequestEp { get; set; }
        public IPEndPoint AuthEp { get; set; } = new IPEndPoint(IPAddress.Parse("12.216.145.92"), 12001);

        public StateConversation(int waitTimeMs = 2000) : base(waitTimeMs)
        {

        }

        protected override void StartConversation()
        {
            if (EnvelopeQueue.GetCount() == 1)
                InitialReceivedEnvelope = EnvelopeQueue.Dequeue();

            ProcessId = InitialReceivedEnvelope.Message.ConversationId.Item1;

            ProcessReceivedMessage();

            if (ConversationId == null)
            {
                EnvelopeQueue.EndOfConversation = true;
                return;
            }

            if (InitialReceivedEnvelope.Message.GetType() == typeof(AuthMessage))
            {
                var authEnvelope = new Envelope()
                {
                    RemoteEP = AuthEp,
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
            }
            var message = CreateRequest();
            if (message != null)
            {
                var envelope = new Envelope()
                {
                    RemoteEP = RequestEp,
                    Message = message
                };

                var sendReceiveSuccess = false;
                for (int receiveAttempt = 0; receiveAttempt < 10 && !sendReceiveSuccess && !EnvelopeQueue.EndOfConversation; receiveAttempt++)
                {
                    sendReceiveSuccess = AttemptSendReceive(envelope);
                }

                if (!sendReceiveSuccess)
                {
                    ProcessFailure();
                    return;
                }
            }

            var updateEnvelope = new Envelope()
            {
                RemoteEP = InitialReceivedEnvelope.RemoteEP,
                Message = CreateUpdate()
            };

            if(!EnvelopeQueue.EndOfConversation)
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
                CheckMessageType(EnvelopeQueue);
                var envelope = EnvelopeQueue.Dequeue();
                if(ProcessReply(envelope.Message))
                    return true;
                return false;
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
                CheckAuthMessageType(EnvelopeQueue);
                var envelope = EnvelopeQueue.Dequeue();
                if(ProcessAuthReply(envelope.Message))
                    return true;
                return false;
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

        protected bool CheckAuthMessageType(EnvelopeQueue queue)
        {
            if (queue.GetMessageType(typeof(TokenVerifyMessage)) == typeof(TokenVerifyMessage))
                return true;

            return false;
        }
        protected Message CreateAuthRequest()
        {
            return null;
        }
        protected bool ProcessAuthReply(Message replyMessage)
        {
            var message = (TokenVerifyMessage)replyMessage;
            if (message.IsAuthorized)
                return true;

            return false;
        }
        protected abstract Message CreateRequest();
        protected abstract bool ProcessReply(Message receivedMessage);
        protected abstract Message CreateUpdate();
        protected abstract bool CheckMessageType(EnvelopeQueue queue);
    }
}
