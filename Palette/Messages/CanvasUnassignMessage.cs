using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
namespace Messages
{
    [DataContract]
    public class CanvasUnassignMessage : Message
    {

        [DataMember]
        public int DisplayId { get; set; }
        [DataMember]
        public int CanvasId { get; set; }
        [DataMember]
        public string State { get; set; }

    }
}
