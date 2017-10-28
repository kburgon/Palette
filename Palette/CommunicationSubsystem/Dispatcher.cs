using System;
using System.Collections.Generic;
using System.Text;

namespace CommunicationSubsystem
{
    class Dispatcher
    {
        protected EnvelopeQueueDictionary envelopeQueueDict = new EnvelopeQueueDictionary();
        private EnvelopeQueue _envelopeQueue { get; set; }
        private Envelope _envelope { get; set; }
        private string _convId { get; set; }

        public void EnqueueEnvelope()
        {
            GetQueue();
            if(_envelope != null)
                _envelopeQueue.Enqueue(_envelope);
        }

        //public Envelope DequeueEnvelope()
        //{
        //    GetQueue();
        //    envelope = envelopeQueue.Dequeue();

        //    return envelope;
        //}

        public void GetQueue()
        {
            _envelopeQueue = envelopeQueueDict.GetConversation(_convId);
        }
    }
}
