using System;
using System.Collections.Generic;
using System.Text;

namespace Messages
{
    public class DeleteCanvasMessage : Message
    {
        public int CanvasId { get; set; }
    }
}
