using System;
using System.Collections.Generic;

namespace CommunicationSubsystem.Messages
{
    public class BrushStrokeMessage : Message
    {
        public int CanvasId { get; set; }
        public string BrushType { get; set; }
        public IEnumerable<Tuple<int, int>> Points { get; set; }
    }
}
