using System.IO;

namespace CommunicationSubsystem.Messages
{
    public class SubscriberCanvasMessage : Message
    {
        public int DisplayId { get; set; }
        public int CanvasId { get; set; }

        public override Message Decode(MemoryStream stream)
        {
            SubscriberCanvasMessage message = new SubscriberCanvasMessage();

            message.MessageID = DecodeShort(stream);
            message.MessageType = DecodeShort(stream);
            message.DisplayId = DecodeShort(stream);
            message.CanvasId = DecodeShort(stream);

            return message;
        }

        public override byte[] Encode()
        {
            MemoryStream stream = new MemoryStream();
            EncodeShort(stream, (short)MessageID);
            EncodeShort(stream, (short)MessageType);
            EncodeShort(stream, (short)DisplayId);
            EncodeShort(stream, (short)CanvasId);

            return stream.ToArray();
        }
    }
}
