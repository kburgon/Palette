using CommunicationSubsystem;
using log4net;
using log4net.Config;

namespace CanvasStorageManager
{
    public class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Dispatcher));
        public static void Main()
        {
            XmlConfigurator.Configure();
            var dispatcher = new Dispatcher();
            dispatcher.UdpCommunicator.SetPort(12500);
            var factory = new StorageManagerConvoFactory();
            factory.Initialize();
            dispatcher.SetFactory(factory);
            dispatcher.StartListener();
        }
    }
}
