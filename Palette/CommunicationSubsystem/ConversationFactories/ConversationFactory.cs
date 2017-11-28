using System;
using CommunicationSubsystem.Conversations;
using Messages;
using System.Collections.Generic;
using CommunicationSubsystem.Conversations.InitiatorConversations;

namespace CommunicationSubsystem.ConversationFactories
{
    public abstract class ConversationFactory
    {
        public static Guid ProcessId = Guid.NewGuid();

        protected Dictionary<Type, Type> ResponderConversationTypes { private get; set; }

        public abstract void Initialize();

        public virtual Conversation CreateFromMessageType(Message message)
        {
            var conversation = (Conversation)Activator.CreateInstance(ResponderConversationTypes[message.GetType()]);
            conversation.GetDataFromMessage(message);
            return conversation;
        }

        public Conversation CreateFromConversationType(Conversation conversation)
        {
            return (Conversation)Activator.CreateInstance(conversation.GetType());
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
