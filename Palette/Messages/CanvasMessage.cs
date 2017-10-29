using System.IO;

namespace CommunicationSubsystem.Messages
{
    public class CanvasMessage : AuthMessage
    {
        public int CanvasId { get; set; }
        public override Message Decode(MemoryStream stream)
        {
            CanvasMessage message = new CanvasMessage();

            message.MessageID = DecodeShort(stream);
            message.MessageType = DecodeShort(stream);
            message.CanvasId = DecodeShort(stream);

            return message;
        }

        public override byte[] Encode()
        {
            MemoryStream stream = new MemoryStream();
            EncodeShort(stream, (short)MessageID);
            EncodeShort(stream, (short)MessageType);
            EncodeShort(stream, (short)CanvasId);

            return stream.ToArray();
        }
    }
}
