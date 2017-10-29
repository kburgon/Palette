using System.IO;

namespace CommunicationSubsystem.Messages
{
    public class GetDisplayMessage : AuthMessage
    {
        public override Message Decode(MemoryStream stream)
        {
            GetDisplayMessage message = new GetDisplayMessage();

            message.MessageID = DecodeShort(stream);
            message.MessageType = DecodeShort(stream);
            message.AuthToken = DecodeString(stream);

            return message;
        }

        public override byte[] Encode()
        {
            MemoryStream stream = new MemoryStream();
            EncodeShort(stream, (short)MessageID);
            EncodeShort(stream, (short)MessageType);
            EncodeString(stream, AuthToken);

            return stream.ToArray();
        }
    }
}
