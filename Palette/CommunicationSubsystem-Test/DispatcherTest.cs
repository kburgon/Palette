using System;
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
            var testGuid = Guid.NewGuid();

            Dispatcher dispatcher = new Dispatcher();
            Envelope envelope = new Envelope()
            {
                RemoteEP = null,
                Message = new BrushStrokeMessage(){ConversationId = new Tuple<Guid, short>(testGuid, 1)}
            };

            EnvelopeQueue queue = dispatcher.GetQueue(envelope.Message.ConversationId);
            dispatcher.EnqueueEnvelope(envelope);

            Assert.AreEqual(1, queue.GetCount());
        }

        [TestMethod]
        public void AddToQueueTest()
        {
            var testProcessId = Guid.NewGuid();

            Dispatcher dispatcher = new Dispatcher();
            Envelope envelope = new Envelope()
            {
                RemoteEP = null,
                Message = new BrushStrokeMessage() {
                    ConversationId = new Tuple<Guid, short>(testProcessId, 1),
                    MessageNumber = new Tuple<Guid, short>(testProcessId, 1)
                }
            };
            Envelope envelope2 = new Envelope()
            {
                RemoteEP = null,
                Message = new BrushStrokeMessage()
                {
                    ConversationId = new Tuple<Guid, short>(testProcessId, 1),
                    MessageNumber = new Tuple<Guid, short>(testProcessId, 2)
                }
            };

            EnvelopeQueue queue = dispatcher.GetQueue(envelope.Message.ConversationId);
            dispatcher.EnqueueEnvelope(envelope);
            dispatcher.EnqueueEnvelope(envelope2);

            Assert.AreEqual(2, queue.GetCount());
        }

        [TestMethod]
        public void DeleteQueueTest()
        {
            var testProcessId = Guid.NewGuid();

            Dispatcher dispatcher = new Dispatcher();
            Envelope envelope = new Envelope()
            {
                RemoteEP = null,
                Message = new BrushStrokeMessage() { ConversationId = new Tuple<Guid, short>(testProcessId, 1) }
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
