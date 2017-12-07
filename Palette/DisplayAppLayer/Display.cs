using CommunicationSubsystem;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DisplayAppLayer
{
    public class Display
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Display));
        private Dispatcher _dispatcher;
        private IPEndPoint CanvasManagerEP;
        private IPEndPoint DisplayManagerEP;
        private int SubscribedCanvasId;

        public Display()
        {
            Logger.InfoFormat("Display Manager Started....");
            _dispatcher = new Dispatcher();
        }

        public void StartDispatcher(int port)
        {
            Logger.InfoFormat("Starting up the dispatcher...");
            if (_dispatcher.IsListening())
                StopDispatcher();

            var convsersationFactory = new DisplayConversationFactory();
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

        public void UpdateCanvasManagerPort(int port)
        {
            CanvasManagerEP.Port = port;
        }

        public void UpdateCanvasManagerAddress(string address)
        {
            CanvasManagerEP.Address = IPAddress.Parse(address);
        }

        public void UpdateDisplayManagerPort(int port)
        {
            DisplayManagerEP.Port = port;
        }

        public void UpdateDisplayManagerAddress(string address)
        {
            DisplayManagerEP.Address = IPAddress.Parse(address);
        }
    }
}
