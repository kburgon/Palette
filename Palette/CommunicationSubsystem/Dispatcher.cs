using System;
using System.Collections.Generic;
using System.Text;
using CommunicationSubsystem.Conversations;
using CommunicationSubsystem.ConversationFactories;
using System.Threading;
using log4net;

namespace CommunicationSubsystem
{
    public class Dispatcher
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Dispatcher));
        protected EnvelopeQueueDictionary envelopeQueueDict = new EnvelopeQueueDictionary();
        private Thread _listenerThread { get; set; }
        private bool _listening { get; set; }
        private int _timeout = 1000;
        private ConversationFactory _conversationFactory { get; set; }
        private Conversation _conversation { get; set; }
        protected readonly Dictionary<Tuple<short, short>, Conversation> _conversationDict = new Dictionary<Tuple<short, short>, Conversation>();
        public UdpCommunicator udpCommunicator = new UdpCommunicator();

        public Dispatcher()
        {
            _listenerThread = new Thread(RetrieveMessage);
        }

        public void EnqueueEnvelope(Envelope env)
        {
            Logger.InfoFormat("Adding envelope to queue: {0} {1}", env.Message.ConversationId.Item1, env.Message.ConversationId.Item2);
            EnvelopeQueue queue = GetQueue(env.Message.ConversationId);
            if(env != null)
                queue.Enqueue(env);
        }

        public EnvelopeQueue GetQueue(Tuple<short, short> convId)
        {
            return envelopeQueueDict.GetConversation(convId);
        }

        public void StartListener()
        {
            Logger.Info("Starting listener thread");
            _listening = true;
            _listenerThread.Start();
        }

        public void StopListener()
        {
            Logger.Info("Stopping listener thread");
            _listening = false;
            _listenerThread.Abort();
            _listenerThread.Join();
        }

        public bool IsListening()
        {
            return _listenerThread.IsAlive;
        }

        public void SetFactory(ConversationFactory factory)
        {
            _conversationFactory = factory;
        }

        public void SetConversationType(Conversation conversation)
        {
            _conversation = conversation;
        }

        public void RetrieveMessage()
        {
            while (_listening)
            {
                Envelope env = udpCommunicator.Receive(_timeout);

                if (env != null)
                {

                    EnvelopeQueue envQueue = null;
                    if (_conversationDict.ContainsKey(env.Message.ConversationId))
                    {
                        Logger.Info("Adding envelope to queue");
                        envQueue = GetQueue(env.Message.ConversationId);
                        EnqueueEnvelope(env);
                    }
                    else
                    {
                        Logger.Info("Creating new queue and conversation");
                        envQueue = GetQueue(env.Message.ConversationId);
                        Conversation conversation = _conversationFactory.CreateFromMessageType(env.Message);
                        EnqueueEnvelope(env);
                        conversation.EnvelopeQueue = envQueue;
                        _conversationDict.Add(env.Message.ConversationId, conversation);
                    }
                }
            }
        }

        public void StartConversationByMessageType(Envelope env)
        {
            EnvelopeQueue envQueue = GetQueue(env.Message.ConversationId);
            EnqueueEnvelope(env);
            Conversation conversation = _conversationFactory.CreateFromMessageType(env.Message);
            conversation.EnvelopeQueue = envQueue;
            _conversationDict.Add(env.Message.ConversationId, conversation);

        }

        public Dictionary<Tuple<short, short>, Conversation> GetConversationDictionary()
        {
            return _conversationDict;
        }
    }
}
