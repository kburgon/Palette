using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Messages;

namespace CommunicationSubsystem
{
    public class Envelope
    {
        public IPEndPoint RemoteEP { get; set; }
        public Message Message { get; set; }
    }
}
