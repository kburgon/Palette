﻿using System.Net;
using CommunicationSubsystem;
using System.Threading.Tasks;
using CommunicationSubsystem.Conversations;
using log4net;
using CanvasManagerAppLayer.Conversations;
using System;
using System.Collections.Generic;

namespace CanvasManagerAppLayer
{
    public class CanvasManager
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CanvasManager));
        private static Dispatcher _dispatcher;
        private IPEndPoint StorageEP;
        private IPEndPoint AuthManagerEp;
        private Dictionary<int, string> DisplayList = new Dictionary<int, string>();

        public CanvasManager()
        {
            Logger.InfoFormat("Starting Canvas Manager...");
            _dispatcher = new Dispatcher()
            {
                ConversationCreationHandler = SetConversation
            };
            AuthManagerEp = new IPEndPoint(IPAddress.Any, 0);
            StorageEP = new IPEndPoint(IPAddress.Any, 0);
        }

        public void StartDispatcher(int communicatorPort)
        {
            if (_dispatcher.IsListening())
                CloseDispatcher();

            var conversationFactory = new CanvasManagerConversationFactory();
            conversationFactory.Initialize();
            _dispatcher.SetFactory(conversationFactory);
            _dispatcher.UdpCommunicator.SetPort(communicatorPort);
            _dispatcher.StartListener();
            Task.Factory.StartNew(GetConversation);

        }

        private void GetConversation()
        {
            Conversation conversation = null;

            while(conversation == null)
            {
                conversation = _dispatcher.GetConversation();
            }

            if (conversation.GetType() == typeof(CreateCanvasStateConversation))
            {
                (conversation as StateConversation).RequestEp = StorageEP;
                (conversation as StateConversation).AuthEp = AuthManagerEp;
            }
        }

        public void CloseDispatcher()
        {
            _dispatcher.StopListener();
        }

        public void UpdateStorageManagerEndpoint(int port, IPAddress address)
        {
            StorageEP.Address = address;
            StorageEP.Port = port;
        }

        public void UpdateStorageManagerPort(int port)
        {
            StorageEP.Port = port;
        }

        public void UpdateAuthManagerEndPoint(int port, IPAddress address)
        {
            AuthManagerEp.Address = address;
            AuthManagerEp.Port = port;
        }

        public void UpdateAuthManagerPort(int port)
        {
            AuthManagerEp.Port = port;
        }

        public void UpdateCanvasManagerPort(int port)
        {
            _dispatcher.UdpCommunicator.SetPort(port);
        }

        public void SetConversation(Conversation conversation)
        {
            if(conversation.GetType() == typeof(SubscribeCanvasStateConversation))
            {
                var conv = (SubscribeCanvasStateConversation)conversation;
                DisplayList.Add(conv.DisplayId, conv.DisplayAddress);
            }
        }
    }
}
