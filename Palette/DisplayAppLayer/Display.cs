using CommunicationSubsystem;
using CommunicationSubsystem.Conversations;
using DisplayAppLayer.Conversations;
using log4net;
using SharedAppLayer.Entitities;
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
        public IPEndPoint CanvasManagerEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
        public IPEndPoint DisplayManagerEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12250);
        private int SubscribedCanvasId;
        public int DisplayId;
        public Canvas DisplaysCanvas;

        public Display()
        {
            Logger.InfoFormat("Display Started....");
            _dispatcher = new Dispatcher()
            {
                ConversationCreationHandler = SetConversation
            };
            DisplayId = 0;
        }

        public void StartDispatcher(int port)
        {
            Logger.InfoFormat("Starting up the dispatcher...");
            if (_dispatcher.IsListening())
                StopDispatcher();

            var convsersationFactory = new DisplayConversationFactory();
            convsersationFactory.Initialize();
            _dispatcher.SetFactory(convsersationFactory);
            _dispatcher.UdpCommunicator.SetPort(12200);
            _dispatcher.StartListener();
        }

        public void StopDispatcher()
        {
            Logger.InfoFormat("Stopping the dispatcher...");
            if (_dispatcher.IsListening())
                _dispatcher.StopListener();
        }

        public void RegisterDisplay()
        {
            Task.Factory.StartNew(Register);
        }
        private void Register()
        {
            try
            {
                var conversation = new RegisterDisplayInitiatorConversation()
                {

                    RemoteEndPoint = DisplayManagerEP
                };

                _dispatcher.StartConversationByConversationType(conversation);
                while (conversation.DisplayId == -1) { }
                DisplayId = conversation.DisplayId;

            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void SubscribeToCanvas()
        {
            try
            {
                var conversation = new SubscribeCanvasInitiatorConversation()
                {
                    //RemoteEndPoint = CanvasManagerEP,
                    DisplayId = this.DisplayId,
                    SubCanvasId = this.SubscribedCanvasId
                };

                _dispatcher.StartConversationByConversationType(conversation);
                while (conversation.NewDisplayCanvas == null) { }
                DisplaysCanvas = conversation.NewDisplayCanvas;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
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

        public void SetConversation(Conversation conversation)
        {
            if (conversation.GetType() == typeof(AssignCanvasResponderConversation))
                Task.Factory.StartNew(SubscribeToCanvas);
        }
    }
}
