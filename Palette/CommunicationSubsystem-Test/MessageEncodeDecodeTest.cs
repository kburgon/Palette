using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Messages;

namespace UnitTestProject1
{
    [TestClass]
    public class MessageDecodeEncodeTest
    {
        [TestMethod]
        public void BrushStrokeMessage()
        {
            BrushStrokeMessage message = new BrushStrokeMessage();
            message.MessageNumber = new Tuple<short, short>(1, 1);
            message.ConversationId = new Tuple<short, short>(2, 1);
            message.CanvasId = 1;
            message.Points.Add(new Tuple<int, int>(1200, 1300));
            message.Points.Add(new Tuple<int, int>(1201, 1302));
            message.Points.Add(new Tuple<int, int>(1203, 1305));
            message.Points.Add(new Tuple<int, int>(1205, 1306));
            message.Points.Add(new Tuple<int, int>(3, 34));
            message.Points.Add(new Tuple<int, int>(10, 13));
            message.BrushType = "Solid";

            byte[] bytes = message.Encode();

            Assert.AreEqual(123, bytes[0]);

            Message message2 = Message.Decode(bytes);

            Assert.AreEqual(message.MessageNumber, (message2 as BrushStrokeMessage).MessageNumber);
            Assert.AreEqual(message.ConversationId, (message2 as BrushStrokeMessage).ConversationId);
            Assert.AreEqual(message.BrushType, (message2 as BrushStrokeMessage).BrushType);
            Assert.AreEqual(message.CanvasId, (message2 as BrushStrokeMessage).CanvasId);
            Assert.AreEqual(message.Points[0].Item1, (message2 as BrushStrokeMessage).Points[0].Item1);
            Assert.AreEqual(message.Points[0].Item2, (message2 as BrushStrokeMessage).Points[0].Item2);
            Assert.AreEqual(message.Points[2].Item1, (message2 as BrushStrokeMessage).Points[2].Item1);
            Assert.AreEqual(message.Points[2].Item2, (message2 as BrushStrokeMessage).Points[2].Item2);
            Assert.AreEqual(message.Points[5].Item1, (message2 as BrushStrokeMessage).Points[5].Item1);
            Assert.AreEqual(message.Points[5].Item2, (message2 as BrushStrokeMessage).Points[5].Item2);

        }
        [TestMethod]
        public void CanvasAssignMessage()
        {

        }
        [TestMethod]
        public void CanvasListMessage()
        {
        }
        [TestMethod]
        public void CanvasMessage()
        {
        }
        [TestMethod]
        public void CanvasUnassignMessage()
        {
        }
        [TestMethod]
        public void CreateCanvasMessage()
        {
        }
        [TestMethod]
        public void DeleteCanvasMessage()
        {
        }
        [TestMethod]
        public void DisplayListMessage()
        {
        }
        [TestMethod]
        public void GetCanvasListMessage()
        {
        }
        [TestMethod]
        public void GetDisplayListMessage()
        {
        }
        [TestMethod]
        public void RegisterAckMessage()
        {
        }
        [TestMethod]
        public void RegisterDisplayMessage()
        {
        }
        [TestMethod]
        public void SubscribeCanvasMessage()
        {
        }
        [TestMethod]
        public void TokenVerifyMessage()
        {
        }
    }
}
