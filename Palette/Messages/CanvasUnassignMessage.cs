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
            MemoryStream stream = new MemoryStream();
            EncodeShort(stream, MessageNumber.Item1);
            EncodeShort(stream, MessageNumber.Item2);
            EncodeShort(stream, ConversationId.Item1);
            EncodeShort(stream, ConversationId.Item2);
            EncodeShort(stream, (short)MessageType);
            EncodeShort(stream, (short)DisplayId);
            EncodeShort(stream, (short)CanvasId);
            EncodeString(stream, State);

            return stream.ToArray();

        }

        public override Message Decode(MemoryStream stream)
        {

            CanvasUnassignMessage message = new CanvasUnassignMessage();

            message.MessageType = DecodeShort(stream);
            message.DisplayId = DecodeShort(stream);
            message.CanvasId = DecodeShort(stream);
            message.State = DecodeString(stream);

            return message;
        }
    }
}
