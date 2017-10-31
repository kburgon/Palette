using System;

namespace CanvasStorageManager
{
    public class Program
    {
        public static void Main()
        {
            var convoFactory = new ConversationFactory();
            var comms = new ConversationFactory( convoFactory );
            comms.startListening();
        }
    }
}
