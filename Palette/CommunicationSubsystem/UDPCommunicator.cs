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
    public class UdpCommunicator
    {
        private UdpClient _udpClientReceiver;
        private UdpClient _udpClientSender;
        private Task _task;
        private bool _keepGoing;
        private IPAddress _address { get; set; }
        private int _port { get; set; }

        public void SetAddress(IPAddress a)
        {
            _address = a;
        }

        public void SetPort(int p)
        {
            _port = p;
        }

        public IPAddress GetAddress()
        {
            return _address;
        }

        public int GetPort()
        {
            return _port;
        }

        public void Send(Envelope env)
        {
            var ep = env.RemoteEP;
            _udpClientSender = new UdpClient();
            if (env != null)
            {
                byte[] b = env.Message.Encode();
                _udpClientSender.Send(b, b.Length, ep);
            }
        }

        public Envelope Receive(int timeout)
        {
            Envelope newEnv = null;
            _udpClientReceiver = null;
            byte[] bytes = null;
            try
            {
                _udpClientReceiver = new UdpClient(_port);
                _udpClientReceiver.Client.ReceiveTimeout = timeout;
            }
            catch (SocketException) { }

            if (_udpClientReceiver != null)
            {
                var ep = new IPEndPoint(IPAddress.Any, _port);
                try
                {
                    bytes = _udpClientReceiver.Receive(ref ep);
                }
                catch (SocketException e)
                {
                    if (e.SocketErrorCode != SocketError.TimedOut)
                        throw;
                }


                if (bytes != null)
                {
                    var newMessage = Message.Decode(bytes);

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
