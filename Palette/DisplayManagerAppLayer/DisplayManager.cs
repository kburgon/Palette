using CommunicationSubsystem;
using System;
using System.Collections.Generic;
using System.Net;
using log4net;

namespace DisplayManagerAppLayer
{

    public class DisplayManager
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(DisplayManager));
        private static Dispatcher _dispatcher;
        private Dictionary<int, Tuple<int, IPEndPoint>> DisplayEPDictionary;
        private IPEndPoint AuthManagerEP;

        public DisplayManager()
        {
            Logger.InfoFormat("Display Manager Started....");
            _dispatcher = new Dispatcher();
            DisplayEPDictionary = new Dictionary<int, Tuple<int, IPEndPoint>>();
            AuthManagerEP = new IPEndPoint(IPAddress.Any, 0);
        }

        public void AddDisplay(IPEndPoint displayEP)
        {
            var id = GenerateDisplayId();
            var display = new Tuple<int, IPEndPoint>(id, displayEP);
            DisplayEPDictionary.Add(id, display);
            Logger.InfoFormat("Adding display to Display Manager: {0} {1}", id, displayEP.Address);
        }

        public void RemoveDisplay(int id)
        {
            if (DisplayEPDictionary.ContainsKey(id)) {
                Logger.InfoFormat("Removed display from Display Manager: {0} {1}", id, DisplayEPDictionary[id].Item2.Address);
                DisplayEPDictionary.Remove(id);
            }
            else
                Logger.InfoFormat("Failed to remove display, Display does not exist: {0}", id);
        }

        public void StartDispatcher(int port)
        {
            Logger.InfoFormat("Starting up the dispatcher...");
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
            Logger.InfoFormat("Stopping the dispatcher...");
            if (_dispatcher.IsListening())
                _dispatcher.StopListener();
        }

        public void UpdateAuthManagerEndPoint(int port, IPAddress address)
        {
            Logger.InfoFormat("Updated Auth Manager endpoint to: {0} {1}", address, port);
            AuthManagerEP.Address = address;
            AuthManagerEP.Port = port;
        }

        public void UpdateAuthManagerPort(int port)
        {
            Logger.InfoFormat("Updated Auth Manager port to: {0}", port);
            AuthManagerEP.Port = port;
        }

        public void UpdateDisplayManagerPort(int port)
        {
            Logger.InfoFormat("Updated Display Manager port to: {0}", port);
            _dispatcher.UdpCommunicator.SetPort(port);
        }

        public int GetDisplayCount()
        {
            return DisplayEPDictionary.Count;
        }

        public List<int> GenerateIdList()
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
            Logger.InfoFormat("Generating display id...");
            if (DisplayEPDictionary.Count == 0)
                return 1;
            else
                return DisplayEPDictionary.Count + 1;
        }
    }
}
