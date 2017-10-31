using System;

namespace Messages
{
    public abstract class Message
    {
        public Tuple<short, short> MessageNumber { get; set; }
        public Tuple<short, short> ConversationId { get; set; }
    }
}
