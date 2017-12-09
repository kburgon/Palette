using CommunicationSubsystem;
using CommunicationSubsystem.Conversations;
using Messages;
using System;
using System.Collections.Generic;

namespace DisplayManagerAppLayer.Conversations
{
    public class GetDisplayListResponderConversation : StateConversation
    {
        public IEnumerable<string> _displayList;

        public GetDisplayListResponderConversation()
        {
            _displayList = null;
        }

        protected override bool CheckMessageType(EnvelopeQueue queue)
        {
            throw new NotImplementedException();
        }

        protected override Message CreateRequest()
        {
            return null;
            //var stepNumber = InitialReceivedEnvelope.Message.MessageNumber.Item2;
            //var message = new GetDisplayListMessage
            //{
            //    ConversationId = InitialReceivedEnvelope.Message.ConversationId,
            //    MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber)
            //};

            //return message;
        }

        protected override void ProcessReceivedMessage()
        {
            if (InitialReceivedEnvelope.Message.GetType() != typeof(GetDisplayListMessage))
                return;

            base.ProcessReceivedMessage();

            while (_displayList == null) { }
        }

        protected override Message CreateUpdate()
        {
            InitialReceivedEnvelope.RemoteEP.Port = 11900;
            var stepNumber = Convert.ToInt16(InitialReceivedEnvelope.Message.MessageNumber.Item2 + 1);
            var message = new DisplayListMessage
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber),
                Displays = _displayList
            };

            return message;
        }

        protected override void ProcessFailure()
        {
            base.ProcessFailure();
        }

        protected override bool ProcessReply(Message receivedMessage)
        {
            throw new NotImplementedException();
        }
    }
}
