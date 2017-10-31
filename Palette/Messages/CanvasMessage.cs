using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
namespace Messages
{
    [DataContract]
    public class CanvasMessage : AuthMessage
    {

        [DataMember]
        public int CanvasId { get; set; }
    }
}
