﻿using CommunicationSubsystem;
using System;
using System.Collections.Generic;
using System.Net;

namespace DisplayManagerAppLayer
{

    public class DisplayManager
    {
        private static Dispatcher _dispatcher;
        private Dictionary<int, Tuple<int, IPEndPoint>> DisplayEPDictionary;
        private IPEndPoint AuthManagerEP;

        public DisplayManager()
        {
            _dispatcher = new Dispatcher();
            DisplayEPDictionary = new Dictionary<int, Tuple<int, IPEndPoint>>();
            AuthManagerEP = new IPEndPoint(IPAddress.Any, 0);
        }

        public void AddDisplay(IPEndPoint displayEP)
        {
            var id = GenerateDisplayId();
            var display = new Tuple<int, IPEndPoint>(id, displayEP);
            DisplayEPDictionary.Add(id, display);
        }

        public void RemoveDisplay(int id)
        {
            DisplayEPDictionary.Remove(id);
        }

        public void StartDispatcher(int port)
        {
            if (_dispatcher.IsListening())
                StopDispatcher();

            var convsersationFactory = new DisplayManagerConversationFactory();
            convsersationFactory.Initialize();
            _dispatcher.SetFactory(convsersationFactory);
            _dispatcher.UdpCommunicator.SetPort(port);
            _dispatcher.StartListener();
        }

        public void StopDispatcher()
        {
            if (_dispatcher.IsListening())
                _dispatcher.StopListener();
        }

        public void UpdateAuthManagerEndPoint(int port, IPAddress address)
        {
            AuthManagerEP.Address = address;
            AuthManagerEP.Port = port;
        }

        public void UpdateAuthManagerPort(int port)
        {
            AuthManagerEP.Port = port;
        }

        public void UpdateDisplayManagerPort(int port)
        {
            _dispatcher.UdpCommunicator.SetPort(port);
        }

        private List<int> GenerateIdList()
        {
            List<int> idList = new List<int>();
            foreach(Tuple<int, IPEndPoint> display in DisplayEPDictionary.Values)
            {
                idList.Add(display.Item1);
            }

            return idList;
        }

        private int GenerateDisplayId()
        {
            if (DisplayEPDictionary.Count == 0)
                return 1;
            else
                return DisplayEPDictionary.Count + 1;
        }
    }
}
