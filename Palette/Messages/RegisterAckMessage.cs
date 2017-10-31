using System.IO;

namespace Messages
{
    public class RegisterAckMessage : Message
    {
        public int DisplayId { get; set; }
        public override Message Decode(MemoryStream stream)
        {
            RegisterAckMessage message = new RegisterAckMessage();

            message.MessageID = DecodeShort(stream);
            message.MessageType = DecodeShort(stream);
            message.DisplayId = DecodeShort(stream);

            return message;
        }

        public override byte[] Encode()
        {
            MemoryStream stream = new MemoryStream();
            EncodeShort(stream, (short)MessageID);
            EncodeShort(stream, (short)MessageType);
            EncodeShort(stream, (short)DisplayId);

            return stream.ToArray();
        }
    }
}
