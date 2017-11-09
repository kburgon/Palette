using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AdminClientAppLayer.Conversations;
using CommunicationSubsystem;
using SharedAppLayer.Entitities;

namespace AdminClientAppLayer
{
    public delegate void CreatedCanvasIdHandler(int canvasId);

    public delegate void DeleteCanvasHandler(int canvasId);

    public delegate void GetCanvasListHandler(IEnumerable<Canvas> canvases);

    public class AdminClient
    {
        private Dispatcher Dispatcher { get; }
        public bool HasStartedDispatcher { get; private set; }
        public CreatedCanvasIdHandler CreatedCanvasIdHandler { get; set; }
        public DeleteCanvasHandler DeleteCanvasHandler { get; set; }
        public GetCanvasListHandler GetCanvasListHandler { get; set; }
        public string CanvasManagerIpAddress { get; set; }
        public int CanvasManagerPortNumber { get; set; }

        public AdminClient()
        {
            Dispatcher = new Dispatcher();
            HasStartedDispatcher = false;
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
            Task.Factory.StartNew(GetCreatedCanvasId);
        }

        public void DeleteCanvas(int canvasId)
        {
            Task.Factory.StartNew(() => RequestCanvasDelete(canvasId));
        }

        public void RequestCanvasList()
        {
            Task.Factory.StartNew(GetCanvasList);
        }

        public void CloseDispatcher()
        {
            Dispatcher.StopListener();
        }

        private void GetCreatedCanvasId()
        {
            try
            {
                var conversation = new CreateCanvasInitiatorConversation
                {
                    ConversationId = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                    RemoteEndPoint = new IPEndPoint(IPAddress.Parse(CanvasManagerIpAddress), CanvasManagerPortNumber)
                };

                Dispatcher.StartConversationByConversationType(conversation);
                while (conversation.CanvasId == null) { }
                CreatedCanvasIdHandler?.Invoke(conversation.CanvasId.Item1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void RequestCanvasDelete(int canvasId)
        {
            try
            {
                var conversation = new DeleteCanvasInitiatorConversation
                {
                    ConversationId = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                    RemoteEndPoint = new IPEndPoint(IPAddress.Parse(CanvasManagerIpAddress), CanvasManagerPortNumber),
                    CanvasId = new Tuple<int>(canvasId)
                };

                Dispatcher.StartConversationByConversationType(conversation);
                while (conversation.CanvasId == null) { }
                DeleteCanvasHandler?.Invoke(conversation.CanvasId.Item1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void GetCanvasList()
        {
            try
            {
                var conversation = new GetCanvasListInitiatorConversation()
                {
                    ConversationId = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                    RemoteEndPoint = new IPEndPoint(IPAddress.Parse(CanvasManagerIpAddress), CanvasManagerPortNumber)
                };

                Dispatcher.StartConversationByConversationType(conversation);
                while (conversation.Canvases == null){ }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
