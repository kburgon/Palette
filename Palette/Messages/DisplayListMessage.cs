using System.Collections.Generic;

namespace Messages
{
    public class DisplayListMessage : Message
    {
        public IEnumerable<string> Displays { get; set; }
    }
}
