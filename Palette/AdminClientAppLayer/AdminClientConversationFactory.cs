using System;
using AdminClientAppLayer.Conversations;
using CommunicationSubsystem.ConversationFactories;
using CommunicationSubsystem.Conversations;

namespace AdminClientAppLayer
{
    public class AdminClientConversationFactory : ConversationFactory
    {
        public override Conversation CreateFromMessageType(Type message)
        {
            throw new NotImplementedException();
        }

        public override Conversation CreateFromConversationType(ConversationType conversationType)
        {
            switch (conversationType)
            {
                case ConversationType.ReadCanvas:
                    return new CreateCanvasInitiatorConversation
                    {
                        ProcessId = ProcessId
                    };
                case ConversationType.AssignCanvas:
                case ConversationType.CreateCanvas:
                case ConversationType.EditCanvas:
                case ConversationType.GetDisplay:
                case ConversationType.RegisterDisplay:
                case ConversationType.SubscribeCanvas:
                case ConversationType.UnassignCanvas:
                    throw new Exception();
                default:
                    throw new ArgumentOutOfRangeException(nameof(conversationType), conversationType, null);
            }
        }
    }
}
