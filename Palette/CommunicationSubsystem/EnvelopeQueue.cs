using System;
using System.Collections.Generic;
using log4net;

namespace CommunicationSubsystem
{
    /// <summary>
    /// This class stores a queue of the envelopes associated with a certain conversation
    /// </summary>
    public class EnvelopeQueue
    {
        #region Private Members

        private int _count = 0;
        private static readonly object _myLock = new object();
        private readonly List<Envelope> _envelopeQueue = new List<Envelope>();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(EnvelopeQueue));

        #endregion

        #region Public Members

        public bool EndOfConversation { get; set; } = false;

        #endregion

        #region Public Methods

        /// <summary>
        /// This method adds an envelope to the queue
        /// </summary>
        /// <param name="env"></param>
        public void Enqueue(Envelope env)
        {
            Logger.InfoFormat("Adding envelope to queue for conversation: {0} {1}", env.Message.ConversationId.Item1, env.Message.ConversationId.Item2);
            lock(_myLock)
            {
                _envelopeQueue.Add(env);
                _count = _envelopeQueue.Count;
            }
        }

        /// <summary>
        /// This method takes the next envelope off the queue
        /// </summary>
        /// <returns></returns>
        public Envelope Dequeue()
        {
            Envelope env = null;
            lock(_myLock)
            {
                env = _envelopeQueue[0];
                _envelopeQueue.RemoveAt(0);
                Logger.InfoFormat("Removing envelope to queue for conversation: {0} {1}", env.Message.ConversationId.Item1, env.Message.ConversationId.Item2);
            }
            
            return env;
        }

        /// <summary>
        /// This method returns the number of envelopes that are in the queue
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return _count;
        }

        public bool Contains(Envelope env)
        {
            foreach(Envelope e in _envelopeQueue)
            {
                if (env.Message.MessageNumber == e.Message.MessageNumber)
                    return true;
            }

            return false;
        }

        #endregion
    }
}
