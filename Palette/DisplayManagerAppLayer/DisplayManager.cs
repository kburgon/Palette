using CommunicationSubsystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

        public void UpdateAuthManagerEndPoint(int port, string address)
        {
            IPAddress newAddress;
            if (IPAddress.TryParse(address, out newAddress))
            {
                IPEndPoint ep = new IPEndPoint(newAddress, port);
                AuthManagerEP = ep;
            }
        }

        public void UpdateAuthManagerPort(int port)
        {
            AuthManagerEP.Port = port;
        }

        public void UpdateDisplayManagerPort(int port)
        {
            _dispatcher.UdpCommunicator.SetPort(port);
        }

        private int GenerateDisplayId()
        {
            int id = 0;
            return id;
        }
    }
}
