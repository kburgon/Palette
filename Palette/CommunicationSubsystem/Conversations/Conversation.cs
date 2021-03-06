﻿using System;
using System.Threading.Tasks;

namespace CommunicationSubsystem.Conversations
{
    public abstract class Conversation
    {
        public Guid ProcessId { get; set; }
        public Tuple<Guid, short> ConversationId { get; set; }
        public EnvelopeQueue EnvelopeQueue { get; }
        public Dispatcher Dispatcher { get; set; }

        protected Task ConversationExecution;
        private bool _isEnded;

        protected Conversation()
        {
            EnvelopeQueue = new EnvelopeQueue();
        }

        public void Execute()
        {
            ConversationExecution = Task.Factory.StartNew(StartConversation);
        }

        protected abstract void StartConversation();

        protected abstract void ProcessFailure();

        protected void End()
        {
            _isEnded = true;
        }

        public bool IsEnded()
        {
            return _isEnded;
        }

        protected Envelope GetNextEnvelope()
        {
            while (EnvelopeQueue.GetCount() == 0) { }
            return EnvelopeQueue.Dequeue();
        }

        protected void Send(Envelope envelope)
        {
            Dispatcher.Send(envelope);
        }
    }
}
