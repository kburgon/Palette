using System.Collections.Generic;
using System.Runtime.Serialization;
using AuthManagerAppLayer;

namespace Messages
{
    [DataContract]
    public class UserListMessage : AuthMessage
    {
        [DataMember]
        public List<User> Users { get; set; }
    }
}
