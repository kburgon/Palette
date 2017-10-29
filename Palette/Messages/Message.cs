using System;
using System.IO;
using System.Net;
using System.Text;

namespace CommunicationSubsystem.Messages
{
    public abstract class Message
    {
        protected int MessageID { get; set; }
        protected int MessageType { get; set; }

        public abstract byte[] Encode();
        public abstract Message Decode(MemoryStream stream);

        public void EncodeString(MemoryStream stream, string s)
        {
            byte[] b = Encoding.BigEndianUnicode.GetBytes(s);
            EncodeShort(stream, (short)b.Length);
            stream.Write(b, 0, b.Length);
        }

        public void EncodeShort(MemoryStream stream, short s)
        {
            byte[] b = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(s));
            stream.Write(b, 0, b.Length);
        }

        public int DecodeShort(MemoryStream stream)
        {
            byte[] b = new byte[2];
            int numOfBytes = stream.Read(b, 0, b.Length);
            if (numOfBytes != b.Length)
                throw new ApplicationException("Decode Short Failed");
            return (int)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(b, 0));
        }

        public string DecodeString(MemoryStream stream)
        {
            string message = String.Empty;
            int messageLength = DecodeShort(stream);
            if (messageLength > 0)
            {
                byte[] b = new byte[messageLength];
                int numOfBytes = stream.Read(b, 0, b.Length);
                if (numOfBytes != messageLength)
                    throw new ApplicationException("Decode String Failed");
                message = Encoding.BigEndianUnicode.GetString(b, 0, b.Length);
            }
            return message;
        }

        public byte DecodeByte(MemoryStream stream)
        {
            byte[] b = new byte[1];
            byte result;
            int numOfBytes = stream.Read(b, 0, 1);
            if (numOfBytes != b.Length)
                throw new ApplicationException("Decode Byte Failed");
            result = b[0];

            return result;
        }
    }
}
