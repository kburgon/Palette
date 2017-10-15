namespace CommunicationSubsystem.Messages
{
    public class CanvasAssignMessage : AuthMessage
    {
        public int DisplayId { get; set; }
        public int CanvasId { get; set; }
        public string State { get; set; }

    }
}
