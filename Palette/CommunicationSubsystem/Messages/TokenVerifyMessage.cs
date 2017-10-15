namespace CommunicationSubsystem.Messages
{
    class TokenVerifyMessage : Message
    {
        public bool IsAuthorized { get; set; }
    }
}
