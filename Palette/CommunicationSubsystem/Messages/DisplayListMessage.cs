using System.Collections.Generic;

namespace CommunicationSubsystem.Messages
{
    public class DisplayListMessage : Message
    {
        public IEnumerable<string> Displays { get; set; }
    }
}
