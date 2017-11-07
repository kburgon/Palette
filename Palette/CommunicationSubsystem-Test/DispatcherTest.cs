﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommunicationSubsystem;
using CommunicationSubsystem.ConversationFactories;
using Messages;
using System.Threading;

namespace CommunicationSubsystemTest
{
    [TestClass]
    public class DispatcherTest
    {
        [TestMethod]
        public void CreateNewQueueTest()
        {
            Dispatcher dispatcher = new Dispatcher();
            Envelope envelope = new Envelope()
            {
                RemoteEP = null,
                Message = new BrushStrokeMessage(){ConversationId = new Tuple<short, short>(1, 1)}
            };

            EnvelopeQueue queue = dispatcher.GetQueue(envelope.Message.ConversationId);
            dispatcher.EnqueueEnvelope(envelope);

            Assert.AreEqual(1, queue.GetCount());
        }

        [TestMethod]
        public void AddToQueueTest()
        {
            Dispatcher dispatcher = new Dispatcher();
            Envelope envelope = new Envelope()
            {
                RemoteEP = null,
                Message = new BrushStrokeMessage() { ConversationId = new Tuple<short, short>(1, 1) }
            };
            Envelope envelope2 = new Envelope()
            {
                RemoteEP = null,
                Message = new CanvasMessage() { ConversationId = new Tuple<short, short>(1, 1) }
            };

            EnvelopeQueue queue = dispatcher.GetQueue(envelope.Message.ConversationId);
            dispatcher.EnqueueEnvelope(envelope);
            dispatcher.EnqueueEnvelope(envelope2);

            Assert.AreEqual(2, queue.GetCount());
        }

        [TestMethod]
        public void DeleteQueueTest()
        {
            Dispatcher dispatcher = new Dispatcher();
            Envelope envelope = new Envelope()
            {
                RemoteEP = null,
                Message = new BrushStrokeMessage() { ConversationId = new Tuple<short, short>(1, 1) }
            };

            EnvelopeQueue queue = dispatcher.GetQueue(envelope.Message.ConversationId);
            dispatcher.EnqueueEnvelope(envelope);

            Assert.AreEqual(1, dispatcher.GetQueueDictionaryCount());

            queue.EndOfConversation = true;

            dispatcher.StartListener();

            Thread.Sleep(2000);

            Assert.AreEqual(1, dispatcher.GetQueueDictionaryCount());

            dispatcher.StopListener();
        }

        [TestMethod]
        public void StartStopListenerTest()
        {
            Dispatcher dispatcher = new Dispatcher();
            dispatcher.StartListener();
            Assert.IsTrue(dispatcher.IsListening());

            dispatcher.StopListener();

            Assert.IsFalse(dispatcher.IsListening());
        }
    }
}
