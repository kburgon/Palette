using System.IO;

namespace Messages
{
    public class UnassignMessage : AuthMessage
    {
        public int DisplayId { get; set; }
        public string State { get; set; }

        public override Message Decode(MemoryStream stream)
        {
            UnassignMessage message = new UnassignMessage
            {
                MessageID = DecodeShort(stream),
                MessageType = DecodeShort(stream),
                AuthToken = DecodeString(stream),
                DisplayId = DecodeShort(stream),
                State = DecodeString(stream)
            };


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
