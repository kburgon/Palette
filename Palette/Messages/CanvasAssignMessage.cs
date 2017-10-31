using System.IO;

namespace Messages
{
    public class CanvasAssignMessage : AuthMessage
    {
        public int DisplayId { get; set; }
        public int CanvasId { get; set; }
        public string State { get; set; }

        public override Message Decode(MemoryStream stream)
        {
            CanvasAssignMessage message = new CanvasAssignMessage();

            message.MessageID = DecodeShort(stream);
            message.MessageType = DecodeShort(stream);
            message.DisplayId = DecodeShort(stream);
            message.CanvasId = DecodeShort(stream);
            message.State = DecodeString(stream);

            return message;
        }

        public override byte[] Encode()
        {
            MemoryStream stream = new MemoryStream();
            EncodeShort(stream, (short)MessageID);
            EncodeShort(stream, (short)MessageType);
            EncodeShort(stream, (short)DisplayId);
            EncodeShort(stream, (short)CanvasId);
            EncodeString(stream, State);

            return stream.ToArray();
        }
    }
}
