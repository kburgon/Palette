using System;
using System.IO;
using System.Net;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;

namespace Messages
{
    [DataContract]
    public class Message
    {
        static Message()
        {
            AddMessageType(typeof(BrushStrokeMessage));
            AddMessageType(typeof(CanvasAssignMessage));
            AddMessageType(typeof(CanvasListMessage));
            AddMessageType(typeof(CanvasMessage));
            AddMessageType(typeof(CanvasUnassignMessage));
            AddMessageType(typeof(CreateCanvasMessage));
            AddMessageType(typeof(DeleteCanvasMessage));
            AddMessageType(typeof(GetCanvasListMessage));
            AddMessageType(typeof(DisplayListMessage));
            AddMessageType(typeof(RegisterAckMessage));
            AddMessageType(typeof(RegisterDisplayMessage));
            AddMessageType(typeof(SubscriberCanvasMessage));
            AddMessageType(typeof(GetDisplayListMessage));
            AddMessageType(typeof(TokenVerifyMessage));
        }

        [DataMember]
        public Tuple<Guid, short> MessageNumber { get; set; }
        [DataMember]
        public Tuple<Guid, short> ConversationId { get; set; }
        //[DataMember]
        //protected int MessageType { get; set; }
        private static List<Type> _types = new List<Type>();

        public static void AddMessageType(Type type)
        {
            if (!_types.Contains(type))
                _types.Add(type);
        }

        public byte[] Encode()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Message), _types);
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, this);

            return stream.ToArray();
        }

        public static Message Decode(byte[] b)
        {
            Message newMessage = null;
            if(b != null)
            {
                try
                {
                    MemoryStream stream = new MemoryStream(b);
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Message), _types);
                    newMessage = serializer.ReadObject(stream) as Message;
                }
                catch(Exception e)
                {

                }
            }

            return newMessage;
        }

        //public void EncodeString(MemoryStream stream, string s)
        //{
        //    byte[] b = Encoding.BigEndianUnicode.GetBytes(s);
        //    EncodeShort(stream, (short)b.Length);
        //    stream.Write(b, 0, b.Length);
        //}

        //public void EncodeShort(MemoryStream stream, short s)
        //{
        //    byte[] b = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(s));
        //    stream.Write(b, 0, b.Length);
        //}

        //public int DecodeInt(MemoryStream stream)
        //{
        //    byte[] b = new byte[2];
        //    int numOfBytes = stream.Read(b, 0, b.Length);
        //    if (numOfBytes != b.Length)
        //        throw new ApplicationException("Decode Short Failed");
        //    return (int)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(b, 0));
        //}

        //public short DecodeShort(MemoryStream stream)
        //{
        //    byte[] b = new byte[2];
        //    int numOfBytes = stream.Read(b, 0, b.Length);
        //    if (numOfBytes != b.Length)
        //        throw new ApplicationException("Decode Short Failed");
        //    return IPAddress.NetworkToHostOrder(BitConverter.ToInt16(b, 0));
        //}

        //public string DecodeString(MemoryStream stream)
        //{
        //    string message = String.Empty;
        //    int messageLength = DecodeShort(stream);
        //    if (messageLength > 0)
        //    {
        //        byte[] b = new byte[messageLength];
        //        int numOfBytes = stream.Read(b, 0, b.Length);
        //        if (numOfBytes != messageLength)
        //            throw new ApplicationException("Decode String Failed");
        //        message = Encoding.BigEndianUnicode.GetString(b, 0, b.Length);
        //    }
        //    return message;
        //}

        //public byte DecodeByte(MemoryStream stream)
        //{
        //    byte[] b = new byte[1];
        //    byte result;
        //    int numOfBytes = stream.Read(b, 0, 1);
        //    if (numOfBytes != b.Length)
        //        throw new ApplicationException("Decode Byte Failed");
        //    result = b[0];

        //    return result;
        //}
    }
}
