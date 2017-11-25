using System;
using System.Runtime.Serialization;

namespace Messages
{
    [DataContract]
    public abstract class AuthMessage : Message
    {
        [DataMember]
        public Guid AuthToken { get; set; }
    }
}
