using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace CommunicationSubsystem
{
    /// <summary>
    /// This class is a dictionary that stores all the queues being used by convsersations
    /// </summary>
    public class EnvelopeQueueDictionary
    {
        #region Private Members

        private static readonly ILog Logger = LogManager.GetLogger(typeof(EnvelopeQueue));

        #endregion

        #region Protected Methods

        protected Dictionary<Tuple<Guid, short>, EnvelopeQueue> EnvelopeQueueDict = new Dictionary<Tuple<Guid, short>, EnvelopeQueue>();

        #endregion


        #region Public Methods

        /// <summary>
        /// This method returns the queue associated with a given conversation Id
        /// </summary>
        /// <param name="convId"></param>
        /// <returns></returns>
        public EnvelopeQueue GetConversation(Tuple<Guid, short> convId)
        {
            Logger.InfoFormat("Getting queue for conversation: {0} {1}", convId.Item1, convId.Item2);
            EnvelopeQueue queue = null;
            if (EnvelopeQueueDict.TryGetValue(convId, out queue))
                return queue;
            else
            {
                CreateQueue(convId);
                return EnvelopeQueueDict[convId];
            }
        }

        /// <summary>
        /// This method creates a new queue if queue and adds it to the dictionary
        /// </summary>
        /// <param name="convId"></param>
        public void CreateQueue(Tuple<Guid, short> convId)
        {
            Logger.InfoFormat("Creating new queue for conversation: {0} {1}", convId.Item1, convId.Item2);
            EnvelopeQueue newQueue = new EnvelopeQueue();
            EnvelopeQueueDict.Add(convId, newQueue);
        }

        /// <summary>
        /// This method is used when a conversation is ended and the queue for the conversation needs to be removed
        /// </summary>
        /// <param name="convId"></param>
        public void CloseQueue(Tuple<Guid, short> convId)
        {
            Logger.InfoFormat("Deleting Queue for conversation: {0} {1}", convId.Item1, convId.Item2);
            EnvelopeQueueDict.Remove(convId);
        }

        /// <summary>
        /// This method returns the number of current queues
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return EnvelopeQueueDict.Count;
        }

        #endregion
    }
}
