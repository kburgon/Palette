using System.Net;
using CommunicationSubsystem;

namespace CanvasManagerAppLayer
{
    public class CanvasManager
    {
        private static Dispatcher _dispatcher;
        private IPEndPoint StorageEP = new IPEndPoint(IPAddress.Any, 0);

        public CanvasManager()
        {
            _dispatcher = new Dispatcher();
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

        public void UpdateStorageManagerAddress(IPAddress address)
        {
            StorageEP.Address = address;
            _dispatcher.UdpCommunicator.SetAddress(address);
        }

        public void UpdateStorageManagerPort(int port)
        {
            StorageEP.Port = port;
        }

        public void UpdateCanvasManagerPort(int port)
        {
            _dispatcher.UdpCommunicator.SetPort(port);
        }
    }
}
