using System;
using System.Collections.Generic;
using System.Net;
using CommunicationSubsystem;
using CommunicationSubsystem.Conversations;
using Messages;

namespace CanvasManagerAppLayer.Conversations
{
    internal class StoreEditConversation : InitiatorConversation
    {
        private readonly BrushStrokeMessage _initialMessage;

        public StoreEditConversation(BrushStrokeMessage initialMessage)
        {
            RemoteEndPoint = new IPEndPoint (IPAddress.Parse("127.0.0.1"), 12500);
            _initialMessage = initialMessage;
        }

        protected override Message CreateRequest()
        {
            return new BrushStrokeMessage
            {
                CanvasId = _initialMessage.CanvasId,
                Points = _initialMessage.Points,
                BrushType = _initialMessage.BrushType,
                ConversationId = ConversationId,
                MessageNumber = ConversationId
            };
        }

        protected override bool ProcessReply(Message receivedMessage)
        {
            return true;
        }

        protected override bool CheckMessageType(EnvelopeQueue queue)
        {
            return true;
        }
    }
}
