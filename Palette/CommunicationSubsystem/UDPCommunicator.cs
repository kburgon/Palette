using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.IO;
using Messages;

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

        public void Send(Envelope env)
        {
            IPEndPoint ep = env.remoteEP;

            if (env != null)
            {
                byte[] b = env.message.Encode();
                udpClientSender.Send(b, b.Length, ep);
            }
        }

        public Envelope Receive(int timeout)
        {
            Envelope newEnv = null;
            Message newMessage = null;
            udpClientReceiver = null;
            byte[] bytes = null;
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

            if(bytes != null)
            {
                MemoryStream stream = new MemoryStream(bytes);
                MemoryStream tempStream = new MemoryStream(bytes);
                int messageId = newMessage.DecodeShort(tempStream);
                int messageType = newMessage.DecodeShort(tempStream);

                switch (messageType)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    case 10:
                        break;
                    case 11:
                        break;
                }
            }

            return newEnv;
        }
    }
}
