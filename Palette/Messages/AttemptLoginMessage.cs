using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Messages
{
    public class AttemptLoginMessage : UserMessage
    {
        public Guid UserToken { get; set; }
    }
}
