using System;
using System.Collections.Generic;
using System.Net;
using CommunicationSubsystem.Conversations;
using CommunicationSubsystem.ConversationFactories;
using System.Threading;
using log4net;
using Messages;

namespace CommunicationSubsystem
{
    /// <summary>
    /// This class is in charge of directing where envelopes go when 
    /// they are received and maintaining all the conversation queues
    /// </summary>
    public class Dispatcher
    {
        #region Private Members

        private readonly UdpCommunicator _udpCommunicator;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Dispatcher));
        private readonly Thread _listenerThread;
        private bool _listening;
        private ConversationFactory _conversationFactory;
        private Envelope _myEnvelope;
        private readonly object _dictionaryLock;
        protected readonly Dictionary<Tuple<Guid, short>, Conversation> ConversationDict = new Dictionary<Tuple<Guid, short>, Conversation>();
        
        #endregion


        #region Public Methods

        public Dispatcher()
        {
            _listenerThread = new Thread(RunListener);
            _udpCommunicator = new UdpCommunicator {EnvelopeHandler = GetEnvelope};
            _dictionaryLock = new object();
        }

        /// <summary>
        /// This method starts the listener thread that is responsible for dealing with envelopes when they are received
        /// </summary>
        public void StartListener()
        {
            Logger.Info("Starting listener thread");
            _listening = true;
            _udpCommunicator.Start();
            _listenerThread.Start();
        }

        /// <summary>
        /// This method stops the listener thread
        /// </summary>
        public void StopListener()
        {
            Logger.Info("Stopping listener thread");
            _listening = false;
            if (_listenerThread.IsAlive)
            {
                _listenerThread.Join();
                _udpCommunicator.Stop();
            }
        }

        /// <summary>
        /// This method returns whether the listener thread is alive
        /// </summary>
        /// <returns></returns>
        public bool IsListening()
        {
            return _listenerThread.IsAlive;
        }

        /// <summary>
        /// This method sets the factory for the conversation
        /// </summary>
        /// <param name="factory"></param>
        public void SetFactory(ConversationFactory factory)
        {
            _conversationFactory = factory;
        }

        public void AddConversation(Conversation convo)
        {
            lock (_dictionaryLock)
            {
                ConversationDict.Add(convo.ConversationId, convo);
                Start(convo);
            }
        }

        #endregion


        #region Private Methods

        private void Start(Conversation convo)
        {
            convo.Dispatcher = this;
            convo.Execute();
        }

        private void RunListener()
        {
            while (_listening)
            {
                var envelope = GetNextEnvelope();
                if (envelope == null) continue;
                var conversation = GetConversation( envelope );
                conversation.EnvelopeQueue.Enqueue(envelope);
                if (conversation.IsEnded())
                    Remove(conversation);
            }
        }

        private void Remove(Conversation conversation)
        {
            lock (_dictionaryLock)
            {
                ConversationDict.Remove(conversation.ConversationId);
            }
        }

        private Envelope GetNextEnvelope()
        {
            return _myEnvelope;
        }

        private Conversation GetConversation(Envelope envelope)
        {
            var key = envelope.Message.ConversationId;
            lock (_dictionaryLock)
            {
                if (!ConversationDict.ContainsKey(key))
                    ConversationDict[key] = CreateConversationFrom(envelope.Message);
                return ConversationDict[key];

            }
        }

        private Conversation CreateConversationFrom(Message message)
        {
            var newConvo = _conversationFactory.CreateFromMessageType(message.MessageType);
            Start(newConvo);
            return newConvo;
        }


        private void GetEnvelope(Envelope env)
        {
            _myEnvelope = env;
        }

        #endregion

        public void SetPort(int portNumber)
        {
            _udpCommunicator.SetPort(portNumber);
        }

        public void Send(Envelope envelope)
        {
            _udpCommunicator.Send(envelope);
        }

        public void SetAddress(IPAddress address)
        {
            _udpCommunicator.SetAddress(address);
        }
    }
}
