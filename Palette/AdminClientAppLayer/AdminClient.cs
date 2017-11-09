using System;
using CommunicationSubsystem;

namespace AdminClientAppLayer
{
    public class AdminClient
    {
        private Dispatcher Dispatcher { get; set; }

        public AdminClient()
        {
            Dispatcher = new Dispatcher();
            Dispatcher.StartListener();
        }

        public void StartDispatcher()
        {
            var conversationFactory = new AdminClientConversationFactory();
            conversationFactory.Initialize();
            Dispatcher.SetFactory(conversationFactory);
            //Dispatcher.UdpCommunicator.SetPort() TODO: add port setting to UI
            Dispatcher.StartListener();
        }
    }
}
