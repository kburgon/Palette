using CommunicationSubsystem;
using System;
using System.Collections.Generic;
using System.Net;
using log4net;
using CommunicationSubsystem.Conversations;
using DisplayManagerAppLayer.Conversations;

namespace DisplayManagerAppLayer
{

    public class DisplayManager
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(DisplayManager));
        private static Dispatcher _dispatcher;
        private Dictionary<int, Tuple<int, string>> DisplayEPDictionary;
        private IPEndPoint AuthManagerEP;
        public Conversation Conversation;

        public DisplayManager()
        {
            Logger.InfoFormat("Display Manager Started....");
            _dispatcher = new Dispatcher()
            {
                ConversationCreationHandler = SetConversation,
            };
            DisplayEPDictionary = new Dictionary<int, Tuple<int, string>>();
            AuthManagerEP = new IPEndPoint(IPAddress.Any, 0);
        }

        public int AddDisplay(string displayIP)
        {
            var id = GenerateDisplayId();
            var display = new Tuple<int, string>(id, displayIP);
            DisplayEPDictionary.Add(id, display);
            Logger.InfoFormat("Adding display to Display Manager: {0} {1}", id, displayIP);

            return id;
        }

        public string GetDisplayAddress(int id)
        {
            if (DisplayEPDictionary.ContainsKey(id))
                return DisplayEPDictionary[id].Item2;
            return null;
        }

        public void RemoveDisplay(int id)
        {
            if (DisplayEPDictionary.ContainsKey(id)) {
                Logger.InfoFormat("Removed display from Display Manager: {0} {1}", id, DisplayEPDictionary[id].Item2);
                DisplayEPDictionary.Remove(id);
            }
            else
                Logger.InfoFormat("Failed to remove display, Display does not exist: {0}", id);
        }

        public void StartDispatcher(int port)
        {
            Logger.InfoFormat("Starting up the dispatcher...");
            if (_dispatcher.IsListening())
                StopDispatcher();

            var convsersationFactory = new DisplayManagerConversationFactory();
            convsersationFactory.Initialize();
            _dispatcher.SetFactory(convsersationFactory);
            _dispatcher.UdpCommunicator.SetPort(12250);
            _dispatcher.StartListener();
        }

        public void StopDispatcher()
        {
            Logger.InfoFormat("Stopping the dispatcher...");
            if (_dispatcher.IsListening())
                _dispatcher.StopListener();
        }

        public void UpdateAuthManagerEndPoint(int port, IPAddress address)
        {
            Logger.InfoFormat("Updated Auth Manager endpoint to: {0} {1}", address, port);
            AuthManagerEP.Address = address;
            AuthManagerEP.Port = port;
        }

        public void UpdateAuthManagerPort(int port)
        {
            Logger.InfoFormat("Updated Auth Manager port to: {0}", port);
            AuthManagerEP.Port = port;
        }

        public void UpdateDisplayManagerPort(int port)
        {
            Logger.InfoFormat("Updated Display Manager port to: {0}", port);
            _dispatcher.UdpCommunicator.SetPort(port);
        }

        public int GetDisplayCount()
        {
            return DisplayEPDictionary.Count;
        }

        public IEnumerable<string> GenerateIdList()
        {
            List<string> idList = new List<string>();
            foreach(Tuple<int, string> display in DisplayEPDictionary.Values)
            {
                idList.Add(display.Item1.ToString());
            }

            return idList;
        }

        public int GenerateDisplayId()
        {
            Logger.InfoFormat("Generating display id...");
            if (DisplayEPDictionary.Count == 0)
                return 1;
            else
                return DisplayEPDictionary.Count + 1;
        }

        public void SetConversation(Conversation conversation)
        {
            if(conversation.GetType() == typeof(RegisterDisplayResponderConversation))
            {
                var myConversation = (RegisterDisplayResponderConversation)conversation;
                myConversation.DisplayId = AddDisplay(myConversation.DisplayAddress);
            }
            else if(conversation.GetType() == typeof(AssignCanvasStateConversation))
            {
                var myConversation = (AssignCanvasStateConversation)conversation;
                do
                {
                    try
                    {
                        myConversation.DisplayAddress = DisplayEPDictionary[myConversation.DisplayId].Item2;
                    }
                    catch { }
                } while (myConversation.DisplayId == -1);
            }
            else if(conversation.GetType() == typeof(UnassignCanvasStateConversation))
            {
                var myConversation = (UnassignCanvasStateConversation)conversation;
                do
                {
                    try
                    {
                        myConversation.DisplayAddress = DisplayEPDictionary[myConversation.DisplayId].Item2;
                    }
                    catch { }
                } while (myConversation.DisplayId == -1);
            }
            else if(conversation.GetType() == typeof(GetDisplayListResponderConversation))
            {
                var myConversation = (GetDisplayListResponderConversation)conversation;
                myConversation._displayList = GenerateIdList();
            }
        }
    }
}
