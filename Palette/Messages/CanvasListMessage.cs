using System.Collections.Generic;
using System.IO;

namespace Messages
{
    public class CanvasListMessage : Message
    {
        public IEnumerable<string> Canvases { get; set; }

        public override Message Decode(MemoryStream stream)
        {
            CanvasListMessage message = new CanvasListMessage();

            message.MessageID = DecodeShort(stream);
            message.MessageType = DecodeShort(stream);

            return message;
        }

        public override byte[] Encode()
        {
            MemoryStream stream = new MemoryStream();
            EncodeShort(stream, (short)MessageID);
            EncodeShort(stream, (short)MessageType);

            return stream.ToArray();
        }
    }
}
