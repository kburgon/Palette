using CommunicationSubsystem;

namespace AuthManagerAppLayer
{
    public class AuthManager
    {
        private static Dispatcher _dispatcher;

        public AuthManager()
        {
            _dispatcher = new Dispatcher();
        }

        public void StartDispatcher(int communicatorPort)
        {
            var conversationFactory = new AuthManagerConversationFactory();
            conversationFactory.Initialize();
            _dispatcher.SetFactory(conversationFactory);
            _dispatcher.UdpCommunicator.SetPort(communicatorPort);
            _dispatcher.StartListener();
        }

        public void CloseDispatcher()
        {
            _dispatcher.StopListener();
        }

        public void UpdateLocalPort(int port)
        {
            _dispatcher.UdpCommunicator.SetPort(port);
        }
    }
}
