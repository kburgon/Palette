namespace Messages
{
    public abstract class Message
    {
        protected int MessageID { get; set; }
        public int MessageType { get; protected set; }
    }
}
