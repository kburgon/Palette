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
        private Thread _listenerThread { get; set; }
        private bool _listening { get; set; }
        private int _timeout = 1000;
        private ConversationFactory _conversationFactory { get; set; }
        private Conversation _conversation { get; set; }
        private Envelope _myEnvelope { get; set; }

        protected readonly Dictionary<Tuple<short, short>, Conversation> _conversationDict = new Dictionary<Tuple<short, short>, Conversation>();
        protected EnvelopeQueueDictionary envelopeQueueDict = new EnvelopeQueueDictionary();

        public UdpCommunicator udpCommunicator;


        public Dispatcher()
        {
            _listenerThread = new Thread(RunListener);
            udpCommunicator = new UdpCommunicator() {EnvelopeHandler = GetEnvelope};
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
            udpCommunicator.Start();
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

        public void RunListener()
        {
            while (_listening)
            {
                if (_myEnvelope != null)
                {

                    EnvelopeQueue envQueue = null;
                    if (_conversationDict.ContainsKey(_myEnvelope.Message.ConversationId))
                    {
                        Logger.Info("Adding envelope to queue");
                        envQueue = GetQueue(_myEnvelope.Message.ConversationId);
                        EnqueueEnvelope(_myEnvelope);
                    }
                    else
                    {
                        Logger.Info("Creating new queue and conversation");
                        envQueue = GetQueue(_myEnvelope.Message.ConversationId);
                        Conversation conversation = _conversationFactory.CreateFromMessageType(_myEnvelope.Message);
                        EnqueueEnvelope(_myEnvelope);
                        conversation.EnvelopeQueue = envQueue;
                        _conversationDict.Add(_myEnvelope.Message.ConversationId, conversation);
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

        private void GetEnvelope(Envelope env)
        {
            _myEnvelope = env;
        }
    }
}
