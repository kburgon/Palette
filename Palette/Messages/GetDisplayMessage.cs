using System;
using System.IO;

namespace Messages
{
    public class GetDisplayMessage : AuthMessage
    {
        public GetDisplayMessage()
        {
            MessageType = 10;
        }

        public override Message Decode(MemoryStream stream)
        {
            GetDisplayMessage message = new GetDisplayMessage();
            short messageNum1 = DecodeShort(stream);
            short messageNum2 = DecodeShort(stream);
            message.MessageNumber = new Tuple<short, short>(messageNum1, messageNum2);
            short convId1 = DecodeShort(stream);
            short convId2 = DecodeShort(stream);
            message.MessageType = DecodeInt(stream);
            message.AuthToken = DecodeString(stream);

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
            EncodeString(stream, AuthToken);

            return stream.ToArray();
        }
    }
}
