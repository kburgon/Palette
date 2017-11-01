using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Messages
{
    [DataContract]
    public class CanvasListMessage : Message
    {
        [DataMember]
        public IEnumerable<string> Canvases { get; set; }
    }
}
