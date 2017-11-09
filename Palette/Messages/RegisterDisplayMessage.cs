using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
namespace Messages
{
    [DataContract]
    public class RegisterDisplayMessage : Message
    {

        [DataMember]
        public string IPAddress { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
