using System.IO;

namespace CommunicationSubsystem.Messages
{
    public class RegisterDisplayMessage : Message
    {
        public string IPAddress { get; set; }
        public string Name { get; set; }

        public override Message Decode(MemoryStream stream)
        {
            RegisterDisplayMessage message = new RegisterDisplayMessage();

            message.MessageID = DecodeShort(stream);
            message.MessageType = DecodeShort(stream);
            message.IPAddress = DecodeString(stream);
            message.Name = DecodeString(stream);

            return message;
        }

        public override byte[] Encode()
        {
            MemoryStream stream = new MemoryStream();
            EncodeShort(stream, (short)MessageID);
            EncodeShort(stream, (short)MessageType);
            EncodeString(stream, IPAddress);
            EncodeString(stream, Name);

            return stream.ToArray();
        }
    }
}
