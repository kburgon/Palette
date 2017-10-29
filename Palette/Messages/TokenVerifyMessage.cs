using System.IO;

namespace CommunicationSubsystem.Messages
{
    class TokenVerifyMessage : Message
    {
        public bool IsAuthorized { get; set; }

        public override Message Decode(MemoryStream stream)
        {
            TokenVerifyMessage message = new TokenVerifyMessage();

            message.MessageID = DecodeShort(stream);
            message.MessageType = DecodeShort(stream);
            byte Auth = DecodeByte(stream);
            if (Auth == 0)
                message.IsAuthorized = true;
            else
                message.IsAuthorized = false;

            return message;
        }

        public override byte[] Encode()
        {
            MemoryStream stream = new MemoryStream();
            EncodeShort(stream, (short)MessageID);
            EncodeShort(stream, (short)MessageType);
            if (IsAuthorized == true)
                stream.WriteByte(0);
            else
                stream.WriteByte(1);

            return stream.ToArray();
        }
    }
}
