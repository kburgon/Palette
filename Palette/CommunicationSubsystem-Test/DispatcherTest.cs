using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommunicationSubsystem;
using CommunicationSubsystem.ConversationFactories;
using Messages;

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
