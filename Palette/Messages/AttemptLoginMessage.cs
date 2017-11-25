using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Messages
{
    public class AttemptLoginMessage : Message
    {
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
