using System;
using CommunicationSubsystem.Conversations;
using Messages;
using System.Collections.Generic;
using CommunicationSubsystem.Conversations.InitiatorConversations;

namespace CommunicationSubsystem.ConversationFactories
{
    public abstract class ConversationFactory
    {
        protected Dictionary<Type, Type> ResponderConversationTypes;

        public abstract void Initialize();

        public Conversation CreateFromMessageType(Message message)
        {
            var conversation = (Conversation)Activator.CreateInstance(ResponderConversationTypes[message.GetType()]);
            conversation.GetDataFromMessage(message);
            return conversation;
        }

        public Conversation CreateFromConversationType(Conversation conversation)
        {
            return (Conversation)Activator.CreateInstance(conversation.GetType());
        }

        public Conversation CreateFromConversationType(ConversationType conversationType)
        {
            switch (conversationType)
            {
                case ConversationType.AssignCanvas:
                    return new AssignCanvasInitiatorConversation();
                case ConversationType.CreateCanvas:
                    return new CreateCanvasInitiatorConversation();
                case ConversationType.EditCanvas:
                    return new EditCanvasInitiatorConversation();
                case ConversationType.GetDisplay:
                    return new GetDisplayInitiatorConversation();
                case ConversationType.ReadCanvas:
                    return new RegisterDisplayInitiatorConversation();
                case ConversationType.RegisterDisplay:
                    return new RegisterDisplayInitiatorConversation();
                case ConversationType.SubscribeCanvas:
                    return new SubscribeCanvasInitiatorConversation();
                case ConversationType.UnassignCanvas:
                    return new UnassignCanvasInitiatorConversation();
                default:
                    return null;
            }
        }

        public enum ConversationType
        {
            AssignCanvas,
            CreateCanvas,
            EditCanvas,
            GetDisplay,
            ReadCanvas,
            RegisterDisplay,
            SubscribeCanvas,
            UnassignCanvas
        }
    }
}
