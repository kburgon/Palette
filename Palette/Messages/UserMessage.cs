using System.Runtime.Serialization;

namespace Messages
{
    [DataContract]
    public class UserMessage : AuthMessage
    {
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
