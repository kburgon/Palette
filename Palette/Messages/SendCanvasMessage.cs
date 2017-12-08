using SharedAppLayer.Entitities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Messages
{
    [DataContract]
    public class SendCanvasMessage : Message
    {
        [DataMember]
        public Canvas Canvas;
    }
}
