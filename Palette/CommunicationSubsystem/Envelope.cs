using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using CommunicationSubsystem.Messages;

namespace CommunicationSubsystem
{
    public class Envelope
    {
        public IPEndPoint remoteEP { get; set; }
        public Message message { get; set; }
    }
}
