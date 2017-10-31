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
            IPEndPoint ep = env.RemoteEP;
            udpClientSender = new UdpClient();
            if (env != null)
            {
                byte[] b = env.Message.Encode();
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

            if (udpClientReceiver != null)
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);
                try
                {
                    bytes = udpClientReceiver.Receive(ref ep);
                }
                catch (SocketException e)
                {
                    if (e.SocketErrorCode != SocketError.TimedOut)
                        throw;
                }


                if (bytes != null)
                {
                    newMessage = Message.Decode(bytes);

                    if (newMessage != null)
                    {
                        newEnv = new Envelope() { RemoteEP = ep, Message = newMessage };
                    }
                    
                }

                return newEnv;
            }
            else
                return null;
        }
    }
}
