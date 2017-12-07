using CommunicationSubsystem;
using CommunicationSubsystem.Conversations;
using Messages;
using System;
using System.Collections.Generic;

namespace DisplayManagerAppLayer.Conversations
{
    public class GetDisplayListResponderConversation : StateConversation
    {
        private IEnumerable<string> _displayList;

        protected override bool CheckMessageType(EnvelopeQueue queue)
        {
            throw new NotImplementedException();
        }

        protected override Message CreateAuthRequest()
        {
            return null;
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

        protected override Message CreateUpdate()
        {
            return null;
            //InitialReceivedEnvelope.RemoteEP.Port = 11900;
            //var stepNumber = Convert.ToInt16(InitialReceivedEnvelope.Message.MessageNumber.Item2 + 1);
            //var message = new DisplayListMessage
            //{
            //    ConversationId = InitialReceivedEnvelope.Message.ConversationId,
            //    MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber),
            //    Displays = _displayList
            //};

            //return message;
        }

        protected override void ProcessAuthReply(Message receivedMessage)
        {
            throw new System.NotImplementedException();
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
