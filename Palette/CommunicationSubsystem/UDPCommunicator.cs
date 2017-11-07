using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Messages;
using log4net;

namespace CommunicationSubsystem
{
    public delegate void EnvelopeHandler(Envelope env);

    public class UdpCommunicator
    {
        private UdpClient _udpClient;
        private Thread _receiveThread;
        private bool _keepGoing;
        private IPAddress Address { get; set; }
        private int Port { get; set; }
        private bool _receiveStarted { get; set; }
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UdpCommunicator));
        private static readonly object _myLock = new object();
        private int MyTimeout = 1000;

        public EnvelopeHandler EnvelopeHandler { get; set; }

        public void SetAddress(IPAddress a)
        {
            Logger.InfoFormat("New Address: {0}", Address);
            Address = a;
        }

        public void SetPort(int p)
        {
            Logger.InfoFormat("New Port: {0}", Port);
            Port = p;
        }

        public IPAddress GetAddress()
        {
            return Address;
        }

        public int GetPort()
        {
            return Port;
        }

        public void Start()
        {
            lock (_myLock)
            {
                if (_receiveStarted)
                    Stop();
                try
                {
                    IPEndPoint myEp = new IPEndPoint(IPAddress.Any, Port);
                    _udpClient = new UdpClient(myEp);
                    _receiveStarted = true;
                    StartReceive();
                }
                catch (SocketException)
                {
                }
            }
        }

        public void Stop()
        {
            lock (_myLock)
            {
                _receiveStarted = false;
                _receiveThread.Join(MyTimeout);
                _receiveThread = null;

                if (_udpClient != null)
                {
                    _udpClient.Close();
                    _udpClient = null;
                }
            }
        }

        private void StartReceive()
        {
            if (!_receiveStarted)
                throw new ApplicationException($"Cannot bind the socket to a port {Port}");
            else
            {
                _receiveThread = new Thread(Receive);
                _receiveThread.Start();
            }
        }

        public void Send(Envelope env)
        {
            Logger.InfoFormat("Send Message: {0} {1}", env.Message.MessageNumber.Item1, env.Message.MessageNumber.Item2);
            var ep = env.RemoteEP;
            if (env != null)
            {
                byte[] b = env.Message.Encode();
                _udpClient.Send(b, b.Length, ep);
            }
        }

        public void Receive()
        {
            Logger.Info("Attempting to receive message");
            while (_receiveStarted)
            {
                Envelope newEnv = null;
                byte[] bytes = null;
                if (Port > 10000 && Port < 13000)
                {
                    try
                    {
                        _udpClient.Client.ReceiveTimeout = MyTimeout;
                    }
                    catch (SocketException e)
                    {
                        Logger.DebugFormat("Failed to start udp receiver: {0}", e);
                    }


                    if (_udpClient != null)
                    {
                        var ep = new IPEndPoint(IPAddress.Any, 0);
                        try
                        {
                            bytes = _udpClient.Receive(ref ep);
                        }
                        catch (SocketException e)
                        {
                            if (e.SocketErrorCode != SocketError.TimedOut)
                            {
                                Logger.DebugFormat("Error receiving message: {0}", e);
                                throw;
                            }
                        }


                        if (bytes != null)
                        {

                            var newMessage = Message.Decode(bytes);

                            if (newMessage != null)
                            {
                                Logger.InfoFormat("Message received, creating envelope for: {0} {1}",
                                    newMessage.MessageNumber.Item1, newMessage.MessageNumber.Item2);
                                newEnv = new Envelope() {RemoteEP = ep, Message = newMessage};
                                EnvelopeHandler.Invoke(newEnv);
                            }
                            else
                                EnvelopeHandler.Invoke(null);

                        }
                    }
                }
            }
        }
    }
}
