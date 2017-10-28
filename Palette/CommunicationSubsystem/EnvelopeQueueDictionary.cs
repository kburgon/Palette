using System;
using System.Collections.Generic;
using System.Text;

namespace CommunicationSubsystem
{
    public class EnvelopeQueueDictionary
    {

        protected Dictionary<string, EnvelopeQueue> envelopeQueueDict = new Dictionary<string, EnvelopeQueue>();

        public EnvelopeQueue GetConversation(string convId)
        {
            EnvelopeQueue queue = null;
            if (envelopeQueueDict.TryGetValue(convId, out queue))
                return queue;
            else
            {
                CreateQueue(convId);
                return envelopeQueueDict[convId];
            }
        }

        public void CreateQueue(string convId)
        {
            EnvelopeQueue newQueue = new EnvelopeQueue();
            envelopeQueueDict.Add(convId, newQueue);
        }

        public void CloseQueue(string convId)
        {
            envelopeQueueDict.Remove(convId);
        }
    }
}
