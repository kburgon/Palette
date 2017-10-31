using System;
using System.Collections.Generic;
using System.Text;

namespace CommunicationSubsystem
{
    public class EnvelopeQueueDictionary
    {

        protected Dictionary<Tuple<short, short>, EnvelopeQueue> envelopeQueueDict = new Dictionary<Tuple<short, short>, EnvelopeQueue>();

        public EnvelopeQueue GetConversation(Tuple<short, short> convId)
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

        public void CreateQueue(Tuple<short, short> convId)
        {
            EnvelopeQueue newQueue = new EnvelopeQueue();
            envelopeQueueDict.Add(convId, newQueue);
        }

        public void CloseQueue(Tuple<short, short> convId)
        {
            envelopeQueueDict.Remove(convId);
        }
    }
}
