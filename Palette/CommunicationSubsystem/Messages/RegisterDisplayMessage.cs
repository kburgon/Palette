namespace CommunicationSubsystem.Messages
{
    public class RegisterDisplayMessage : Message
    {
        public string IPAddress { get; set; }
        public string Name { get; set; }
    }
}
