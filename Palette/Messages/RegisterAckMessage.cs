﻿using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
namespace Messages
{
    [DataContract]
    public class RegisterAckMessage : Message
    {
        [DataMember]
        public int DisplayId { get; set; }
    }
}
