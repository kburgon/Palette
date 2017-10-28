using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace CommunicationSubsystem
{
    public class UDPCommunicator
    {
        private UdpClient udpClientReceiver;
        private UdpClient udpClientSender;
        private Task _task;
        private bool _keepGoing;
        private IPAddress address { get; set; }
        private int port { get; set; }

        public void SetAddress(IPAddress a)
        {
            address = a;
        }

        public void SetPort(int p)
        {
            port = p;
        }

        public IPAddress GetAddress()
        {
            return address;
        }

        public int GetPort()
        {
            return port;
        }

        public void Send(Envelope envelope)
        {

        }

        public Envelope Receive(int timeout)
        {
            Envelope newEnv = null;
            udpClientReceiver = null;
            byte[] bytes;
            try
            {
                udpClientReceiver = new UdpClient(port);
                udpClientReceiver.Client.ReceiveTimeout = timeout;
            }
            catch (SocketException) { }

            if(udpClientReceiver != null)
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);
                try
                {
                    bytes = udpClientReceiver.Receive(ref ep);
                }
                catch(SocketException e)
                {
                    if (e.SocketErrorCode != SocketError.TimedOut)
                        throw;
                }
            }

            return newEnv;
        }
    }
}
