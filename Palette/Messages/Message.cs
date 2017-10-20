namespace CommunicationSubsystem.Messages
{
    public abstract class Message
    {
        protected int MessageID { get; set; }
        protected int MessageType { get; set; }
    }
}
