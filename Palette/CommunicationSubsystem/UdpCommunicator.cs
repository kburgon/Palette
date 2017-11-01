﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.IO;
using Messages;
using log4net;

namespace CommunicationSubsystem
{
    public class UdpCommunicator
    {
        private UdpClient _udpClientReceiver;
        private UdpClient _udpClientSender;
        private Task _task;
        private bool _keepGoing;
        private IPAddress Address { get; set; }
        private int Port { get; set; }
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UdpCommunicator));

        public void SetAddress(IPAddress a)
        {
            Address = a;
        }

        public void SetPort(int p)
        {
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

        public void Send(Envelope env)
        {
            Logger.InfoFormat("Sending Message: {0} {1}", env.Message.MessageNumber.Item1, env.Message.MessageNumber.Item2);
            var ep = env.RemoteEP;
            _udpClientSender = new UdpClient();
            if (env != null)
            {
                var b = env.Message.Encode();
                _udpClientSender.Send(b, b.Length, ep);
            }
        }

        public Envelope Receive(int timeout)
        {
            Logger.Info("Trying to receive a message");
            Envelope newEnv = null;
            _udpClientReceiver = null;
            byte[] bytes = null;
            try
            {
                _udpClientReceiver = new UdpClient(Port);
                _udpClientReceiver.Client.ReceiveTimeout = timeout;
            }
            catch (SocketException)
            {
                Logger.DebugFormat("Error with receiving message");
            }

            if (_udpClientReceiver != null)
            {
                var ep = new IPEndPoint(IPAddress.Any, Port);
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
                        Logger.InfoFormat("Creating Envelope for message: {0} {1}", newMessage.MessageNumber.Item1, newMessage.MessageNumber.Item2);
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
