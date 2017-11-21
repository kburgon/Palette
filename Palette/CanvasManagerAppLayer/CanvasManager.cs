using System.Net;
using CommunicationSubsystem;

namespace CanvasManagerAppLayer
{
    public class CanvasManager
    {
        private static Dispatcher _dispatcher;
        private IPEndPoint StorageEP;
        private IPEndPoint AuthManagerEp;

        public CanvasManager()
        {
            _dispatcher = new Dispatcher();
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
    }
}
