using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Messages;
using log4net;

namespace CommunicationSubsystem
{
    public delegate void EnvelopeHandler(Envelope env);

    /// <summary>
    /// This class sends and receives messages via UDP Sockets
    /// </summary>
    public class UdpCommunicator
    {
        #region Private Members

        private UdpClient _udpClient;
        private Thread _receiveThread;
        private IPAddress Address { get; set; }
        private int Port { get; set; }
        private bool _receiveStarted { get; set; }
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UdpCommunicator));
        private static readonly object _myLock = new object();
        private int MyTimeout = 1000;

        #endregion


        #region Public Members

        public EnvelopeHandler EnvelopeHandler { get; set; }

        #endregion


        #region Public Methods

        /// <summary>
        /// This method set the address of where the packet will be sent
        /// </summary>
        /// <param name="a"></param>
        public void SetAddress(IPAddress a)
        {
            Logger.InfoFormat("New Address: {0}", Address);
            Address = a;
        }

        /// <summary>
        /// This method sets the port to look for when dealing with the packets
        /// </summary>
        /// <param name="p"></param>
        public void SetPort(int p)
        {
            Logger.InfoFormat("New Port: {0}", Port);
            Port = p;
        }

        /// <summary>
        /// This method returns the address
        /// </summary>
        /// <returns></returns>
        public IPAddress GetAddress()
        {
            return Address;
        }
        /// <summary>
        /// This method returns the port
        /// </summary>
        /// <returns></returns>
        public int GetPort()
        {
            return Port;
        }
        /// <summary>
        /// This method starts the UdpCommunicator
        /// </summary>
        public void Start()
        {
            Logger.Info("Starting up Udp Communicator");
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
                    Logger.Debug("Failed to start Udp Client");
                }
            }
        }

        /// <summary>
        /// This method will stop the UdpCommunicator
        /// </summary>
        public void Stop()
        {
            Logger.Info("Stopping Udp Communicator");
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

        /// <summary>
        /// This method sends the envelope to the endpoint stored in the envelope
        /// </summary>
        /// <param name="env"></param>
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

        /// <summary>
        /// This method will receive messages and then decode that message and put it in an envelope
        /// </summary>
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

        #endregion


        #region Private Methods

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

        #endregion
    }
}
