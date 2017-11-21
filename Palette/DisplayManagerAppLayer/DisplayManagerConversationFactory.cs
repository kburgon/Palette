using CommunicationSubsystem.ConversationFactories;
using System;
using System.Collections.Generic;
using Messages;

namespace DisplayManagerAppLayer
{
    class DisplayManagerConversationFactory : ConversationFactory
    {
        public override void Initialize()
        {
            ResponderConversationTypes = new Dictionary<Type, Type>
            {

            };
        }
    }
}
