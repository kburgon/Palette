using System.Collections.Generic;
using System.Runtime.Serialization;
using SharedAppLayer.Entitities;

namespace Messages
{
    [DataContract]
    public class CanvasListMessage : Message
    {
        public override MessageType MessageType => MessageType.CanvasListMessage;

        [DataMember]
        public IEnumerable<Canvas> Canvases { get; set; }
    }
}
