using System;
using System.Collections.Generic;
using System.Text;
using CommunicationSubsystem.Conversations;
using CommunicationSubsystem.ConversationFactories;
using System.Threading;

namespace CommunicationSubsystem
{
    public class Dispatcher
    {
        protected EnvelopeQueueDictionary envelopeQueueDict = new EnvelopeQueueDictionary();
        private EnvelopeQueue _envelopeQueue { get; set; }
        private Envelope _envelope { get; set; }
        private Thread _listenerThread { get; set; }
        private bool _listening { get; set; }
        private int _timeout = 1000;
        private ConversationFactory _conversationFactory { get; set; }
        private Conversation _conversation { get; set; }
        private Dictionary<Tuple<short, short>, Conversation> _conversationDict = new Dictionary<Tuple<short, short>, Conversation>();
        public UDPCommunicator udpCommunicator = new UDPCommunicator();

        public Dispatcher()
        {
            _listenerThread = new Thread(RetrieveMessage);
        }

        public void EnqueueEnvelope(Envelope env)
        {
            EnvelopeQueue queue = GetQueue(env.Message.ConversationId);
            if(env != null)
                queue.Enqueue(env);
        }

        //public Envelope DequeueEnvelope()
        //{
        //    GetQueue();
        //    envelope = envelopeQueue.Dequeue();

        //    return envelope;
        //}

        public EnvelopeQueue GetQueue(Tuple<short, short> convId)
        {
            return envelopeQueueDict.GetConversation(convId);
        }

        public void StartListener()
        {
            _listening = true;
            _listenerThread.Start();
        }

        public void StopListener()
        {
            _listening = false;
        }

        public void SetFactory(ConversationFactory factory)
        {
            _conversationFactory = factory;
        }

        public void SetConversationType(Conversation conver)
        {
            _conversation = conver;
        }

        public void RetrieveMessage()
        {
            Envelope env = null;
            EnvelopeQueue envQueue = null;
            Conversation conversation;
            while (_listening)
            {
                conversation = null;
                env = udpCommunicator.Receive(_timeout);

                if (env != null)
                {
                    if (_conversationDict.ContainsKey(env.message.ConversationId))
                    {
                        envQueue = GetQueue(env.message.ConversationId);
                        EnqueueEnvelope(env);
                    }
                    else
                    {
                        envQueue = GetQueue(env.message.ConversationId);
                        conversation = _conversationFactory.CreateFromMessageType(env.message);
                        EnqueueEnvelope(env);
                        _conversationDict.Add(env.message.ConversationId, conversation);
                    }
                }
            }
        }

        public void StartConversationByMessageType(Envelope env)
        {
            EnvelopeQueue envQueue = GetQueue(env.message.ConversationId);
            EnqueueEnvelope(env);
            Conversation conversation = _conversationFactory.CreateFromMessageType(env.message);
            _conversationDict.Add(env.message.ConversationId, conversation);
        }
    }
}
