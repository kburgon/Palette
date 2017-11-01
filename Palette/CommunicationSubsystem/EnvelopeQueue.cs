using System;
using System.Collections.Generic;
using System.Text;

namespace CommunicationSubsystem
{
    public class EnvelopeQueue
    {
        private int _count;
        private object _myLock = new object();
        private List<Envelope> _envelopeQueue = new List<Envelope>();
        private string _convId;

        public void Enqueue(Envelope envelope)
        {
            lock(_myLock)
            {
                _envelopeQueue.Add(envelope);
                _count = _envelopeQueue.Count;
            }
        }

        public Envelope Dequeue()
        {
            Envelope env = null;
            lock(_myLock)
            {
                env = _envelopeQueue[0];
                _envelopeQueue.RemoveAt(0);
            }

            return env;
        }

        public int GetCount()
        {
            return _count;
        }
    }
}
