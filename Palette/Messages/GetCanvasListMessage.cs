using System.Runtime.Serialization;

namespace Messages
{
    [DataContract]
    public class GetCanvasListMessage : AuthMessage
    {
        public override MessageType MessageType => MessageType.CanvasListMessage;
    }
}
