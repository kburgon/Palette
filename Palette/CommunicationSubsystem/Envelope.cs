using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Messages;

namespace CommunicationSubsystem
{
    /// <summary>
    /// This class is used to store a message and endpoint
    /// </summary>
    public class Envelope
    {
        public IPEndPoint RemoteEP { get; set; }
        public Message Message { get; set; }
    }
}
