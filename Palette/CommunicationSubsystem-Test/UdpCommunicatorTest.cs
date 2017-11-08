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
        private Envelope envelope1;
        private Envelope envelope2;

        [TestMethod]
        public void UdpSendReceiveTest()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12000);
            Message message = new CanvasMessage()
            {
                ConversationId = new Tuple<short, short>(1,1),
                MessageNumber = new Tuple<short, short>(1, 1),
                CanvasId = 1
            };
            Envelope envelope = new Envelope(){ RemoteEP = ep, Message = message};
            UdpCommunicator communicator1 = new UdpCommunicator()
            {
                EnvelopeHandler = FirstEnvelope
            };
            communicator1.SetPort(12001);
            communicator1.Start();

            UdpCommunicator communicator2 = new UdpCommunicator()
            {
                EnvelopeHandler =  SecondEnvelope
            };
            communicator2.SetPort(12000);
            communicator2.Start();

            Thread.Sleep(2000);
            communicator1.Send(envelope);
            Thread.Sleep(1000);

            Assert.IsNotNull(envelope2);
            Assert.IsNotNull(envelope2.Message);
            Assert.AreEqual(envelope.Message.ConversationId, envelope2.Message.ConversationId);
            Assert.AreEqual(envelope.Message.MessageNumber, envelope2.Message.MessageNumber);
            Assert.AreEqual((envelope.Message as CanvasMessage).CanvasId, (envelope2.Message as CanvasMessage).CanvasId);

        }

        private void FirstEnvelope(Envelope env)
        {
            envelope1 = env;
        }

        private void SecondEnvelope(Envelope env)
        {
            envelope2 = env;
        }
    }
}
