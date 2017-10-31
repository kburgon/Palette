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
                        newMessage = new BrushStrokeMessage();
                        newMessage.Decode(stream);
                        break;
                    case 2:
                        newMessage = new CanvasAssignMessage();
                        newMessage.Decode(stream);
                        break;
                    case 3:
                        newMessage = new CanvasListMessage();
                        newMessage.Decode(stream);
                        break;
                    case 4:
                        newMessage = new CanvasMessage();
                        newMessage.Decode(stream);
                        break;
                    case 5:
                        newMessage = new CanvasUnassignMessage();
                        newMessage.Decode(stream);
                        break;
                    case 6:
                        newMessage = new CreateCanvasMessage();
                        newMessage.Decode(stream);
                        break;
                    case 7:
                        newMessage = new DeleteCanvasMessage();
                        newMessage.Decode(stream);
                        break;
                    case 8:
                        newMessage = new DisplayListMessage();
                        newMessage.Decode(stream);
                        break;
                    case 9:
                        newMessage = new GetCanvasListMessage();
                        newMessage.Decode(stream);
                        break;
                    case 10:
                        newMessage = new GetDisplayMessage();
                        newMessage.Decode(stream);
                        break;
                    case 11:
                        newMessage = new RegisterAckMessage();
                        newMessage.Decode(stream);
                        break;
                    case 12:
                        newMessage = new RegisterDisplayMessage();
                        newMessage.Decode(stream);
                        break;
                    case 13:
                        newMessage = new SubscriberCanvasMessage();
                        newMessage.Decode(stream);
                        break;
                    case 14:
                        newMessage = new TokenVerifyMessage();
                        newMessage.Decode(stream);
                        break;
                }
            }

            return newEnv;
        }
    }
}
