using System;
using System.Runtime.Serialization;

namespace Messages
{
    [DataContract]
    public class TokenVerifyMessage : Message
    {
        [DataMember]
        public bool IsAuthorized { get; set; }
        [DataMember]
        public Guid AuthToken { get; set; }
    }
}
