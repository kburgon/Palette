using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
namespace Messages
{
    [DataContract]
    public class DeleteCanvasMessage : Message
    {
        //public DeleteCanvasMessage()
        //{
        //    MessageType = 7;
        //}
        [DataMember]
        public int CanvasId { get; set; }
        //public override byte[] Encode()
        //{
        //    MemoryStream stream = new MemoryStream();
        //    EncodeShort(stream, MessageNumber.Item1);
        //    EncodeShort(stream, MessageNumber.Item2);
        //    EncodeShort(stream, ConversationId.Item1);
        //    EncodeShort(stream, ConversationId.Item2);
        //    EncodeShort(stream, (short)MessageType);
        //    EncodeShort(stream, (short)CanvasId);

        //    return stream.ToArray();
        //}

        //public override Message Decode(MemoryStream stream)
        //{
        //    DeleteCanvasMessage message = new DeleteCanvasMessage();

        //    short messageNum1 = DecodeShort(stream);
        //    short messageNum2 = DecodeShort(stream);
        //    message.MessageNumber = new Tuple<short, short>(messageNum1, messageNum2);
        //    short convId1 = DecodeShort(stream);
        //    short convId2 = DecodeShort(stream);
        //    message.ConversationId = new Tuple<short, short>(convId1, convId2);
        //    message.MessageType = DecodeInt(stream);
        //    message.CanvasId = DecodeInt(stream);

        //    return message;
        //}
    }
}
