using System;
using System.Collections.Generic;
using System.IO;

namespace Messages
{
    public class BrushStrokeMessage : Message
    {
        public BrushStrokeMessage()
        {
            MessageType = 1;
        }

        public int CanvasId { get; set; }
        public string BrushType { get; set; }
        public List<Tuple<int, int>> Points { get; set; }

        public override Message Decode(MemoryStream stream)
        {
            BrushStrokeMessage message = new BrushStrokeMessage();
            short messageNum1 = DecodeShort(stream);
            short messageNum2 = DecodeShort(stream);
            message.MessageNumber = new Tuple<short, short>(messageNum1, messageNum2);
            short convId1 = DecodeShort(stream);
            short convId2 = DecodeShort(stream);
            message.MessageType = DecodeShort(stream);
            message.CanvasId = DecodeShort(stream);
            message.BrushType = DecodeString(stream);
            int count = DecodeShort(stream);

            while(count >= 1)
            {
                int point1 = DecodeShort(stream);
                int point2 = DecodeShort(stream);
                Tuple<int, int> point = new Tuple<int, int>(point1, point2);
                Points.Add(point);
                count--;
            }

            return message;
        }

        public override byte[] Encode()
        {
            MemoryStream stream = new MemoryStream();
            EncodeShort(stream, MessageNumber.Item1);
            EncodeShort(stream, MessageNumber.Item2);
            EncodeShort(stream, ConversationId.Item1);
            EncodeShort(stream, ConversationId.Item2);
            EncodeShort(stream, (short)this.MessageType);
            EncodeShort(stream, (short)this.CanvasId);
            EncodeString(stream, BrushType);
            EncodeShort(stream, (short)Points.Count);
            foreach(Tuple<int, int> point in Points)
            {
                EncodeShort(stream, (short)point.Item1);
                EncodeShort(stream, (short)point.Item2);
            }

            return stream.ToArray();
        }
    }
}
