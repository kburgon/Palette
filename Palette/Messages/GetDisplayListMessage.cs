using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
namespace Messages
{
    [DataContract]
    public class GetDisplayListMessage : Message
    {
        //public GetDisplayListMessage()
        //{
        //    MessageType = 8;
        //}
        [DataMember]
        public IEnumerable<string> Displays { get; set; }
        //public override Message Decode(MemoryStream stream)
        //{
        //    GetDisplayListMessage message = new GetDisplayListMessage();
        //    short messageNum1 = DecodeShort(stream);
        //    short messageNum2 = DecodeShort(stream);
        //    message.MessageNumber = new Tuple<short, short>(messageNum1, messageNum2);
        //    short convId1 = DecodeShort(stream);
        //    short convId2 = DecodeShort(stream);
        //    message.MessageType = DecodeInt(stream);

        //    return message;
        //}

        //public override byte[] Encode()
        //{
        //    MemoryStream stream = new MemoryStream();
        //    EncodeShort(stream, MessageNumber.Item1);
        //    EncodeShort(stream, MessageNumber.Item2);
        //    EncodeShort(stream, ConversationId.Item1);
        //    EncodeShort(stream, ConversationId.Item2);
        //    EncodeShort(stream, (short)MessageType);

        //    return stream.ToArray();
        //}
    }
}
