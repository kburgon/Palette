using System.Net;
using CommunicationSubsystem;

namespace CanvasManagerAppLayer
{
    public class CanvasManager
    {
        private static Dispatcher _dispatcher;
        private IPEndPoint StorageEP { get; set; }

        public CanvasManager()
        {
            _dispatcher = new Dispatcher();
        }

        public void StartDispatcher(string storageAddress, int storagePort, int communicatorPort)
        {
            var conversationFactory = new CanvasManagerConversationFactory();
            conversationFactory.Initialize();
            StorageEP = new IPEndPoint(IPAddress.Parse(storageAddress), communicatorPort);
            _dispatcher.SetFactory(conversationFactory);
            _dispatcher.UdpCommunicator.SetAddress(IPAddress.Parse(storageAddress));
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
