using System;
using System.Collections.Generic;
using System.Text;

namespace CommunicationSubsystem
{
    public class EnvelopeQueueDictionary
    {

        protected Dictionary<Tuple<Guid, short>, EnvelopeQueue> envelopeQueueDict = new Dictionary<Tuple<Guid, short>, EnvelopeQueue>();

        public EnvelopeQueue GetConversation(Tuple<Guid, short> convId)
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

        public void CreateQueue(Tuple<Guid, short> convId)
        {
            EnvelopeQueue newQueue = new EnvelopeQueue();
            envelopeQueueDict.Add(convId, newQueue);
        }

        public void CloseQueue(Tuple<Guid, short> convId)
        {
            envelopeQueueDict.Remove(convId);
        }
    }
}
