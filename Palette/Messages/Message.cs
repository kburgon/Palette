using System;
using System.IO;
using System.Net;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using log4net;

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
            AddMessageType(typeof(AttemptLoginMessage));
            AddMessageType(typeof(DeleteUserMessage));
            AddMessageType(typeof(CreateUserMessage));
            AddMessageType(typeof(GetUserListMessage));
            AddMessageType(typeof(UserListMessage));
        }

        [DataMember]
        public Tuple<Guid, short> MessageNumber { get; set; }
        [DataMember]
        public Tuple<Guid, short> ConversationId { get; set; }

        private static readonly List<Type> _types = new List<Type>();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Message));

        public static void AddMessageType(Type type)
        {
            if (!_types.Contains(type))
                _types.Add(type);
        }

        public byte[] Encode()
        {
            Logger.InfoFormat("Encoding message: {0} {1}", MessageNumber.Item1, MessageNumber.Item2);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Message), _types);
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, this);

            return stream.ToArray();
        }

        public static Message Decode(byte[] b)
        {
            Message newMessage = null;
            if (b == null)
                return null;
                try
                {
                    MemoryStream stream = new MemoryStream(b);
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Message), _types);
                    newMessage = serializer.ReadObject(stream) as Message;
                }
                catch(Exception e)
                {
                    Logger.DebugFormat("Failed to decode message: {0}", e);
                }

            return newMessage;
        }
    }
}
