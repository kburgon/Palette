using System;
using CommunicationSubsystem;

namespace AdminClientAppLayer
{
    public class AdminClient
    {
        private Dispatcher Dispatcher { get; }
        public bool HasStartedDispatcher { get; private set; }

        public AdminClient()
        {
            Dispatcher = new Dispatcher();
            Dispatcher.StartListener();
        }

        public void StartDispatcher(int portNumber)
        {
            if (!HasStartedDispatcher)
            {
                var conversationFactory = new AdminClientConversationFactory();
                conversationFactory.Initialize();
                Dispatcher.SetFactory(conversationFactory);
                Dispatcher.UdpCommunicator.SetPort(portNumber);
                Dispatcher.StartListener();
                HasStartedDispatcher = true;
            }
        }

        public void CreateCanvas()
        {
            
        }

        public void DeleteCanvas()
        {
            
        }

        public void RequestCanvasList()
        {
            
        }

        public void CloseDispatcher()
        {
            Dispatcher.StopListener();
        }
    }
}
