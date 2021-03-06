﻿using System.Runtime.Serialization;

namespace Messages
{
    [DataContract]
    public abstract class AuthMessage : Message
    {
        [DataMember]
        public string AuthToken { get; set; }
    }
}
