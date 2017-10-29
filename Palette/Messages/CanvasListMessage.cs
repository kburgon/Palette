using System.Collections.Generic;

namespace Messages
{
    public class CanvasListMessage : Message
    {
        public IEnumerable<string> Canvases { get; set; }
    }
}
