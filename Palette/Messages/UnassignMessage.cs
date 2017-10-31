using System.IO;

namespace Messages
{
    public class UnassignMessage : AuthMessage
    {
        public int DisplayId { get; set; }
        public string State { get; set; }

        public override Message Decode(MemoryStream stream)
        {
            UnassignMessage message = new UnassignMessage();

            message.MessageID = DecodeShort(stream);
            message.MessageType = DecodeShort(stream);
            message.AuthToken = DecodeString(stream);
            message.DisplayId = DecodeShort(stream);
            message.State = DecodeString(stream);

            return message;
        }

        public override byte[] Encode()
        {
            MemoryStream stream = new MemoryStream();
            EncodeShort(stream, (short)MessageID);
            EncodeShort(stream, (short)MessageType);
            EncodeString(stream, AuthToken);
            EncodeShort(stream, (short)DisplayId);
            EncodeString(stream, State);

            return stream.ToArray();
        }
    }
}
