using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommunicationSubsystem;
using System.Net;
using System.Threading;
using Messages;

namespace CommunicationSubsystemTest
{
    [TestClass]
    public class UdpCommunicatorTest
    {
        [TestMethod]
        public void UdpSendReceiveTest()
        {
            var testGuid = Guid.NewGuid();

            UdpCommunicator communicator1 = new UdpCommunicator();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12000);
            Message message = new CanvasMessage()
            {
                ConversationId = new Tuple<Guid, short>(testGuid,1),
                MessageNumber = new Tuple<Guid, short>(testGuid, 1),
                CanvasId = 1
            };
            Envelope envelope = new Envelope(){ RemoteEP = ep, Message = message};
            Dispatcher dispatcher = new Dispatcher();
            dispatcher.udpCommunicator.SetPort(12000);

            dispatcher.StartListener();

            Thread.Sleep(1000);

            communicator1.Send(envelope);

            Thread.Sleep(2000);

            EnvelopeQueue queue = dispatcher.GetQueue(envelope.Message.ConversationId);

            Envelope envelope2 = queue.Dequeue();
            Assert.IsNotNull(envelope2);
        }
    }
}
