using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Messages
{
    [DataContract]
    public class BrushStrokeMessage : Message
    {
        //public BrushStrokeMessage()
        //{
        //    Points = new List<Tuple<int, int>> { };
        //}

        [DataMember]
        public int CanvasId { get; set; }
        [DataMember]
        public string BrushType { get; set; }
        [DataMember]
        public List<Tuple<int, int>> Points { get; set; }
    }
}
