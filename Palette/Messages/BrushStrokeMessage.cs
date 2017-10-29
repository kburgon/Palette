using System;
using System.Collections.Generic;

namespace Messages
{
    public class BrushStrokeMessage : Message
    {
        public int CanvasId { get; set; }
        public string BrushType { get; set; }
        public List<Tuple<int, int>> Points { get; set; }
    }
}
