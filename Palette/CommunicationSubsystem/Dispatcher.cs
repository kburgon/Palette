﻿using System;
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
        public UDPCommunicator udpCommunicator = new UDPCommunicator();

        public Dispatcher()
        {
            _listenerThread = new Thread(RetrieveMessage);
        }

        public void EnqueueEnvelope(Envelope env)
        {
            EnvelopeQueue queue = GetQueue(env.message.ConversationId);
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
            while (_listening)
            {
                env = udpCommunicator.Receive(_timeout);

                if (env != null)
                {
                    envQueue = GetQueue(env.message.ConversationId);
                    _conversationFactory.CreateFromMessageType(env.message);
                    EnqueueEnvelope(env);
                }
            }
        }

        public void StartConversation(Envelope env)
        {
            EnqueueEnvelope(env);
            _conversationFactory.CreateFromConversationType(_conversation);
        }
    }
}
