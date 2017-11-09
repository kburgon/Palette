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

        public void StartDispatcher(string communicatorAddress, int communicatorPort)
        {
            var conversationFactory = new CanvasManagerConversationFactory();
            conversationFactory.Initialize();
            StorageEP = new IPEndPoint(IPAddress.Parse(communicatorAddress), communicatorPort);
            _dispatcher.SetFactory(conversationFactory);
            _dispatcher.UdpCommunicator.SetAddress(IPAddress.Parse(communicatorAddress));
            _dispatcher.UdpCommunicator.SetPort(communicatorPort);
            _dispatcher.StartListener();
        }

        public void CloseDispatcher()
        {
            _dispatcher.StopListener();
        }

        public void UpdateAddress(IPAddress address)
        {
            _dispatcher.UdpCommunicator.SetAddress(address);
            StorageEP.Address = address;
        }

        public void UpdatePort(int port)
        {
            _dispatcher.UdpCommunicator.SetPort(port);
            StorageEP.Port = port;
        }
    }
}
