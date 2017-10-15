namespace CommunicationSubsystem.Messages
{
    public abstract class AuthMessage : Message
    {
        public string AuthToken { get; set; }
    }
}
