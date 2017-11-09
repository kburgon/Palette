using CommunicationSubsystem;

namespace CanvasStorageManager
{
    public class Program
    {
        public static void Main()
        {
            var dispatcher = new Dispatcher();
            dispatcher.UdpCommunicator.SetPort(12500);
            var factory = new StorageManagerConvoFactory();
            dispatcher.SetFactory(factory);
            dispatcher.StartListener();
        }
    }
}
