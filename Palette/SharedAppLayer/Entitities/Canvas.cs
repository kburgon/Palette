using System;
using System.Collections.Generic;

namespace SharedAppLayer.Entitities
{
    // TODO: This should probably be moved to some common sublayer
    public class Canvas
    {
        public uint CanvasId { get; set; }

        public DateTime DateCreated { get; set; }

        public IEnumerable<BrushStroke> BrushStrokes { get; set; }
    }
}
