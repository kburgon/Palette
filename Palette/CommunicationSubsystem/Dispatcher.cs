﻿using System;
using System.Collections.Generic;
using System.Text;
using CommunicationSubsystem.Conversations;
using CommunicationSubsystem.ConversationFactories;
using System.Threading;
using log4net;
using System.Net;

namespace CommunicationSubsystem
{
    /// <summary>
    /// This class is in charge of directing where envelopes go when 
    /// they are received and maintaining all the conversation queues
    /// </summary>
    public class Dispatcher
    {
        #region Private Members

        private static readonly ILog Logger = LogManager.GetLogger(typeof(Dispatcher));
        private Thread _listenerThread { get; set; }
        private bool _listening { get; set; }
        private ConversationFactory _conversationFactory { get; set; }
        private Conversation _conversation { get; set; }
        private Envelope _myEnvelope { get; set; }
        private object _dictionaryLock;

        #endregion


        #region Protected Members

        protected readonly Dictionary<Tuple<Guid, short>, Conversation> ConversationDict = new Dictionary<Tuple<Guid, short>, Conversation>();
        protected EnvelopeQueueDictionary EnvelopeQueueDict = new EnvelopeQueueDictionary();

        #endregion


        #region Public Members

        public UdpCommunicator UdpCommunicator;
        public bool ConversationEnd;
        public IPEndPoint AuthEP { get; set; } = null;
        public IPEndPoint CanvasManagerEP { get; set; } = null;
        public IPEndPoint CanvasStorageManagerEP { get; set; } = null;
        public IPEndPoint DisplayManagerEP { get; set; } = null;

        #endregion


        #region Public Methods

        public Dispatcher()
        {
            _listenerThread = new Thread(RunListener);
            UdpCommunicator = new UdpCommunicator() {EnvelopeHandler = GetEnvelope};
            _dictionaryLock = new object();
        }

        /// <summary>
        /// This method adds an envelope to the conversation queue that it belongs
        /// </summary>
        /// <param name="env"></param>
        public void EnqueueEnvelope(Envelope env)
        {
            Logger.InfoFormat("Adding envelope to queue: {0} {1}", env.Message.ConversationId.Item1, env.Message.ConversationId.Item2);
            EnvelopeQueue queue = GetQueue(env.Message.ConversationId);
            queue.Enqueue(env);
        }

        /// <summary>
        /// This method returns a conversation queue based on the conversation Id
        /// </summary>
        /// <param name="convId"></param>
        /// <returns></returns>
        public EnvelopeQueue GetQueue(Tuple<Guid, short> convId)
        {
            return EnvelopeQueueDict.GetConversation(convId);
        }

        /// <summary>
        /// This method returns the number of queues that are being stored in the dictionary
        /// </summary>
        /// <returns></returns>
        public int GetQueueDictionaryCount()
        {
            return EnvelopeQueueDict.GetCount();
        }

        /// <summary>
        /// This method starts the listener thread that is responsible for dealing with envelopes when they are received
        /// </summary>
        public void StartListener()
        {
            Logger.Info("Starting listener thread");
            _listening = true;
            UdpCommunicator.Start();
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
                UdpCommunicator.Stop();
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

        /// <summary>
        /// This method sets the converstion type
        /// </summary>
        /// <param name="conversation"></param>
        public void SetConversationType(Conversation conversation)
        {
            _conversation = conversation;
        }

        /// <summary>
        /// This method runs in the listener thread. It checks if there is a new envelope and adds it to the correct queue
        /// </summary>
        public void RunListener()
        {
            Envelope tempEnvelope = new Envelope();
            while (_listening)
            {
                if (_myEnvelope != tempEnvelope && _myEnvelope != null)
                {
                    tempEnvelope.Message = _myEnvelope.Message;
                    tempEnvelope.RemoteEP = _myEnvelope.RemoteEP;
                    EnvelopeQueue envQueue = null;
                    lock (_dictionaryLock)
                    {
                        if (ConversationDict.ContainsKey(_myEnvelope.Message.ConversationId))
                        {
                            Logger.Info("Adding envelope to queue");
                            //envQueue = GetQueue(_myEnvelope.Message.ConversationId);
                            EnqueueEnvelope(_myEnvelope);
                        }
                        else
                        {
                            Logger.Info("Creating new queue and conversation");
                            StartConversationByMessageType(tempEnvelope);
                        }
                    }
                }

                CheckConversationStatus();
            }
        }

        /// <summary>
        /// This method starts a conversation by using the message type
        /// </summary>
        /// <param name="env"></param>
        public void StartConversationByMessageType(Envelope env)
        { 

            EnvelopeQueue envQueue  = GetQueue(env.Message.ConversationId);

            EnqueueEnvelope(env);

            var conversation = _conversationFactory.CreateFromMessageType(env.Message);
            conversation.ReceivedEnvelope = env;

            conversation.EnvelopeQueue = envQueue;

            conversation.Execute();

            lock (_dictionaryLock)
            {
                ConversationDict.Add(env.Message.ConversationId, conversation);
            }

        }

        /// <summary>
        /// This method is used when a process is the initiator of a conversation
        /// </summary>
        /// <param name="conver"></param>
        public void StartConversationByConversationType(Conversation conver)
        {
            EnvelopeQueue envQueue = GetQueue(conver.ConversationId);;
            Conversation conversation = conver;
            conversation.EnvelopeQueue = envQueue;
            conversation.Execute();

            lock (_dictionaryLock)
            {
                ConversationDict.Add(conver.ConversationId, conversation);
            }
        }

        /// <summary>
        /// This method returns the dictionary that contains all the envelope queues
        /// </summary>
        /// <returns></returns>
        public Dictionary<Tuple<Guid, short>, Conversation> GetConversationDictionary()
        {
            return ConversationDict;
        }

        public Conversation GetConversation()
        {
            return _conversation;
        }

        #endregion


        #region Private Methods


        private void GetEnvelope(Envelope env)
        {
            _myEnvelope = env;
        }
        
        /// <summary>
        /// This method removes the queue for a convseration that has ended
        /// </summary>
        /// <param name="convId"></param>
        private void EndConversation(Tuple<Guid, short> convId)
        {
            EnvelopeQueueDict.CloseQueue(convId);
            ConversationDict.Remove(convId);
        }

        /// <summary>
        /// This method loops through all the queues in the dictionary and checks if they are flagged as being over.
        /// It then removes those queues
        /// </summary>
        private void CheckConversationStatus()
        {
            lock (_dictionaryLock)
            {
                List<Tuple<Guid, short>> convDeleteKeys = new List<Tuple<Guid, short>>();

                foreach (Conversation conv in ConversationDict.Values)
                {
                    EnvelopeQueue queue = GetQueue(conv.ConversationId);
                    if (queue != null && queue.EndOfConversation)
                        convDeleteKeys.Add(conv.ConversationId);                   
                }

                foreach(Tuple<Guid, short> key in convDeleteKeys)
                {
                    EndConversation(key);
                }
            }
        }

        #endregion
    }
}
