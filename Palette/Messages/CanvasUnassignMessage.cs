namespace Messages
{
    public class CanvasUnassignMessage : Message
    {
        public int DisplayId { get; set; }
        public int CanvasId { get; set; }
        public string State { get; set; }
    }
}
