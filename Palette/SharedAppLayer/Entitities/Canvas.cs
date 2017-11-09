using System;
using System.Collections.Generic;

namespace SharedAppLayer.Entitities
{
    // TODO: This should probably be moved to some common sublayer
    public class Canvas : IHasKey
    {
        public uint CanvasId { get; set; }

        public DateTime DateCreated { get; set; }

        public IEnumerable<BrushStroke> BrushStrokes { get; set; }

        public uint Key
        {
            get => CanvasId;
            set => CanvasId = value;
        }
    }

    public interface IHasKey
    {
        uint Key { get; set; }
    }
}
