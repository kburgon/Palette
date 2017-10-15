using System.Collections.Generic;

namespace CommunicationSubsystem.Messages
{
    public class CanvasListMessage : Message
    {
        public IEnumerable<string> Canvases { get; set; }
    }
}
