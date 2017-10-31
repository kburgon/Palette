using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
namespace Messages
{
    [DataContract]
    public class TokenVerifyMessage : Message
    {
        [DataMember]
        public bool IsAuthorized { get; set; }
    }
}
