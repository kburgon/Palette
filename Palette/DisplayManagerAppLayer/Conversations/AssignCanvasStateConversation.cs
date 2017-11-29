﻿using CommunicationSubsystem;
using CommunicationSubsystem.Conversations;
using Messages;
using System;

namespace DisplayManagerAppLayer.Conversations
{
    public class AssignCanvasStateConversation : StateConversation
    {
        protected override Message CreateAuthRequest()
        {
            throw new System.NotImplementedException();
        }

        protected override Message CreateRequest()
        {
            var stepNumber = InitialReceivedEnvelope.Message.MessageNumber.Item2;
            var message = new CanvasAssignMessage
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber)
            };

            return message;
        }

        protected override Message CreateUpdate()
        {
            var stepNumber = Convert.ToInt16(InitialReceivedEnvelope.Message.MessageNumber.Item2 + 1);
            var message = new CanvasAssignMessage
            {
                ConversationId = InitialReceivedEnvelope.Message.ConversationId,
                MessageNumber = new Tuple<Guid, short>(ProcessId, stepNumber)
            };

            return message;
        }

        protected override void ProcessAuthReply(Message receivedMessage)
        {
            throw new System.NotImplementedException();
        }

        protected override void ProcessFailure()
        {
            base.ProcessFailure();
        }

        protected override void ProcessReply(Message receivedMessage)
        {
            var message = receivedMessage;
        }
    }
}
