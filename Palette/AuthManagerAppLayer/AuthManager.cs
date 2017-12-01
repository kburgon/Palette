using CommunicationSubsystem;
using log4net;
using log4net.Config;

namespace AuthManagerAppLayer
{
    public class AuthManager
    {
        private static ILog Logger = LogManager.GetLogger(typeof(AuthManager));

        private static Dispatcher _dispatcher;

        public AuthManager()
        {
            _dispatcher = new Dispatcher();
            Logger.Info("Started Dispatcher.");
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
