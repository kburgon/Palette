﻿using System.IO;

namespace Messages
{
    public class CreateCanvasMessage : Message
    {
        public int CanvasId { get; set; }
        public override byte[] Encode()
        {
            throw new System.NotImplementedException();
        }

        public override Message Decode(MemoryStream stream)
        {
            throw new System.NotImplementedException();
        }
    }
}