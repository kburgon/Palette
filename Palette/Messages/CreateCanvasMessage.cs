using System;
using System.Collections.Generic;
using System.Text;

namespace Messages
{
    public class CreateCanvasMessage : Message
    {
        public int CanvasId { get; set; }
    }
}
