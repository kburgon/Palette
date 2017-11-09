using System.Collections.Generic;
using System.Runtime.Serialization;
using SharedAppLayer.Entitities;

namespace Messages
{
    [DataContract]
    public class CanvasListMessage : Message
    {
        [DataMember]
        public IEnumerable<Canvas> Canvases { get; set; }
    }
}
