using System.IO;

namespace Messages
{
    public class CanvasUnassignMessage : Message
    {
        public int DisplayId { get; set; }
        public int CanvasId { get; set; }
        public string State { get; set; }
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
