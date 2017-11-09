using System;
using System.Collections.Generic;

namespace SharedAppLayer.Entitities
{
    public class Canvas : IHasKey
    {
        public int CanvasId { get; set; }

        public DateTime DateCreated { get; set; }

        public IEnumerable<BrushStroke> BrushStrokes { get; set; }

        public int Key
        {
            get => CanvasId;
            set => CanvasId = value;
        }

        public override string ToString()
        {
            return CanvasId.ToString();
        }
    }
}
