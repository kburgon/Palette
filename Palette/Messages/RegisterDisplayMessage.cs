using System;
using System.IO;

namespace Messages
{
    public class RegisterDisplayMessage : Message
    {
        public RegisterDisplayMessage()
        {
            MessageType = 12;
        }

        public string IPAddress { get; set; }
        public string Name { get; set; }

        public override Message Decode(MemoryStream stream)
        {
            RegisterDisplayMessage message = new RegisterDisplayMessage();
            short messageNum1 = DecodeShort(stream);
            short messageNum2 = DecodeShort(stream);
            message.MessageNumber = new Tuple<short, short>(messageNum1, messageNum2);
            short convId1 = DecodeShort(stream);
            short convId2 = DecodeShort(stream);
            message.MessageType = DecodeInt(stream);
            message.IPAddress = DecodeString(stream);
            message.Name = DecodeString(stream);

            return message;
        }

        public override byte[] Encode()
        {
            MemoryStream stream = new MemoryStream();
            EncodeShort(stream, MessageNumber.Item1);
            EncodeShort(stream, MessageNumber.Item2);
            EncodeShort(stream, ConversationId.Item1);
            EncodeShort(stream, ConversationId.Item2);
            EncodeShort(stream, (short)MessageType);
            EncodeString(stream, IPAddress);
            EncodeString(stream, Name);

            return stream.ToArray();
        }
    }
}
