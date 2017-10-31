using System.Collections.Generic;
using System.IO;

namespace Messages
{
    public class DisplayListMessage : Message
    {
        public IEnumerable<string> Displays { get; set; }
        public override Message Decode(MemoryStream stream)
        {
            DisplayListMessage message = new DisplayListMessage();

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
