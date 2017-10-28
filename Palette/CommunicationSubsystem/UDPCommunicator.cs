using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace CommunicationSubsystem
{
    class UDPCommunicator
    {
        protected UdpClient udpClient;
        private Task _task;
        private bool _keepGoing;
        public IPAddress address { get; set; }
        public int port { get; set; }
        public int timeout { get; set; }

        public void Send(Envelope envelope)
        {

        }

        public Envelope Receive()
        {
            bool endReceive = false;
            while (!endReceive)
            {
                try
                {
                }
                catch (SocketException e)
                {
                    if (e.SocketErrorCode != SocketError.TimedOut)
                        throw;
                }
            }

            return null;
        }
    }
}
