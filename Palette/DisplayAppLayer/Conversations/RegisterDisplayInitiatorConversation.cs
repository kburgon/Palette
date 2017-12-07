using CommunicationSubsystem;
using CommunicationSubsystem.Conversations;
using Messages;
using System;
using log4net;
using System.Net;

namespace DisplayAppLayer.Conversations
{
    class RegisterDisplayInitiatorConversation : InitiatorConversation
    {
        public int DisplayId { get; set; }
        private static readonly ILog Logger = LogManager.GetLogger(typeof(RegisterDisplayInitiatorConversation));

        protected override bool CheckMessageType(EnvelopeQueue queue)
        {
            return true;
        }

        protected override Message CreateRequest()
        {
            var message = new RegisterDisplayMessage()
            {
                ConversationId = this.ConversationId,
                MessageNumber = new Tuple<Guid, short>(this.ProcessId, 1),
                IPAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString()
            };

            return message;
        }

        protected override void ProcessFailure()
        {
            base.ProcessFailure();
        }

        protected override bool ProcessReply(Message receivedMessage)
        {
            var message = (RegisterAckMessage)receivedMessage;
            DisplayId = message.DisplayId;
            Logger.InfoFormat("Display added: {0}", DisplayId);
            return EnvelopeQueue.EndOfConversation = true;
        }
    }
}
