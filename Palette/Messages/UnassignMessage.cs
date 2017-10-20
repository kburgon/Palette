namespace CommunicationSubsystem.Messages
{
    public class UnassignMessage : AuthMessage
    {
        public int DisplayId { get; set; }
        public string State { get; set; }
    }
}
