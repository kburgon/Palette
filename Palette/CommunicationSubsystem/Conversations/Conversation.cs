using System;
using System.Collections.Generic;
using System.Text;

namespace CommunicationSubsystem.Conversations
{
    public abstract class Conversation
    {
        public abstract void Execute();
        protected abstract void ProcessFailure();
    }
}
