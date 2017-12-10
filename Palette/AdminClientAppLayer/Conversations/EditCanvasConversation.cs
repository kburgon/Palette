using System;
using System.Collections.Generic;
using CommunicationSubsystem;
using CommunicationSubsystem.Conversations;
using Messages;

namespace AdminClientAppLayer.Conversations
{
    public class EditCanvasConversation : InitiatorConversation
    {
        private readonly int _canvasId;
        private readonly List<Tuple<int, int>> _points;

        public EditCanvasConversation(int canvasId, List<Tuple<int, int>> points)
        {
            _canvasId = canvasId;
            _points = points;
        }

        protected override Message CreateRequest()
        {
            return new BrushStrokeMessage
            {
                BrushType = "",
                CanvasId = _canvasId,
                Points = _points,
                MessageNumber = ConversationId,
                ConversationId = ConversationId
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
