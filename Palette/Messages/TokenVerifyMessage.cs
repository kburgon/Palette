using System.Runtime.Serialization;

namespace Messages
{
    [DataContract]
    public class TokenVerifyMessage : AuthMessage
    {
        [DataMember]
        public bool IsAuthorized { get; set; }
    }
}
