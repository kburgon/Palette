using CommunicationSubsystem;

namespace CanvasManagerAppLayer
{
    public class CanvasManager
    {
        private static Dispatcher _dispatcher;

        public CanvasManager()
        {
            _dispatcher = new Dispatcher();
        }

        public void StartDispatcher(int communicatorPort)
        {
            var conversationFactory = new CanvasManagerConversationFactory();
            conversationFactory.Initialize();
            _dispatcher.SetFactory(conversationFactory);
            _dispatcher.udpCommunicator.SetPort(communicatorPort);
            _dispatcher.StartListener();
        }

        public void CloseDispatcher()
        {
            _dispatcher.StopListener();
        }
    }
}
