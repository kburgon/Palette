using System;
using System.IO;

namespace Messages
{
    public class TokenVerifyMessage : Message
    {
        public bool IsAuthorized { get; set; }

        public override Message Decode(MemoryStream stream)
        {
            TokenVerifyMessage message = new TokenVerifyMessage();

            short messageNum1 = DecodeShort(stream);
            short messageNum2 = DecodeShort(stream);
            message.MessageNumber = new Tuple<short, short>(messageNum1, messageNum2);
            short convId1 = DecodeShort(stream);
            short convId2 = DecodeShort(stream);
            message.MessageType = DecodeInt(stream);
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
            EncodeShort(stream, MessageNumber.Item1);
            EncodeShort(stream, MessageNumber.Item2);
            EncodeShort(stream, ConversationId.Item1);
            EncodeShort(stream, ConversationId.Item2);
            EncodeShort(stream, (short)MessageType);
            if (IsAuthorized == true)
                stream.WriteByte(0);
            else
                stream.WriteByte(1);

            return stream.ToArray();
        }
    }
}
