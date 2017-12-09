using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Messages;

namespace MessageTest
{
    [TestClass]
    public class MessageDecodeEncodeTest
    {
        [TestMethod]
        public void BrushStrokeMessage()
        {
            var message = new BrushStrokeMessage()
            {
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                ConversationId = new Tuple<Guid, short>(Guid.NewGuid(), 5),
                CanvasId = 1,
                BrushType = "Solid",
                Points = new List<Tuple<int, int>>()
            };

            message.Points.Add(new Tuple<int, int>(1200, 1300));
            message.Points.Add(new Tuple<int, int>(1201, 1302));
            message.Points.Add(new Tuple<int, int>(1203, 1305));
            message.Points.Add(new Tuple<int, int>(1205, 1306));
            message.Points.Add(new Tuple<int, int>(3, 34));
            message.Points.Add(new Tuple<int, int>(10, 13));

            byte[] bytes = message.Encode();

            Assert.AreEqual(123, bytes[0]);
            Assert.AreEqual(34, bytes[1]);

            var message2 = Message.Decode(bytes);

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
            var message = new CanvasAssignMessage()
            {
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                ConversationId = new Tuple<Guid, short>(Guid.NewGuid(), 2),
                CanvasId = 1,
                DisplayId = 3,
                State = "Good"
            };

            byte[] bytes = message.Encode();

            Assert.AreEqual(123, bytes[0]);

            var message2 = Message.Decode(bytes);

            Assert.AreEqual(message.CanvasId, (message2 as CanvasAssignMessage).CanvasId);
            Assert.AreEqual(message.MessageNumber, (message2 as CanvasAssignMessage).MessageNumber);
            Assert.AreEqual(message.ConversationId, (message2 as CanvasAssignMessage).ConversationId);
            Assert.AreEqual(message.DisplayId, (message2 as CanvasAssignMessage).DisplayId);
            Assert.AreEqual(message.State, (message2 as CanvasAssignMessage).State);
        }
        [TestMethod]
        public void CanvasListMessage()
        {
            var message = new CanvasListMessage()
            {
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                ConversationId = new Tuple<Guid, short>(Guid.NewGuid(), 2),
            };

            byte[] bytes = message.Encode();

            Assert.AreEqual(123, bytes[0]);

            var message2 = Message.Decode(bytes);

            Assert.AreEqual(message.ConversationId, (message2 as CanvasListMessage).ConversationId);
            Assert.AreEqual(message.MessageNumber, (message2 as CanvasListMessage).MessageNumber);
        }
        [TestMethod]
        public void CanvasMessage()
        {
            var message = new CanvasMessage()
            {
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                ConversationId = new Tuple<Guid, short>(Guid.NewGuid(), 2),
                CanvasId = 1
            };

            byte[] bytes = message.Encode();

            Assert.AreEqual(123, bytes[0]);

            var message2 = Message.Decode(bytes);

            Assert.AreEqual(message.CanvasId, (message2 as CanvasMessage).CanvasId);
            Assert.AreEqual(message.MessageNumber, (message2 as CanvasMessage).MessageNumber);
            Assert.AreEqual(message.ConversationId, (message2 as CanvasMessage).ConversationId);
        }
        [TestMethod]
        public void CanvasUnassignMessage()
        {
            var message = new CanvasUnassignMessage()
            {
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                ConversationId = new Tuple<Guid, short>(Guid.NewGuid(), 2),
                CanvasId = 1,
                DisplayId = 3,
                State = "Good"
            };

            byte[] bytes = message.Encode();

            Assert.AreEqual(123, bytes[0]);

            var message2 = Message.Decode(bytes);

            Assert.AreEqual(message.CanvasId, (message2 as CanvasUnassignMessage).CanvasId);
            Assert.AreEqual(message.MessageNumber, (message2 as CanvasUnassignMessage).MessageNumber);
            Assert.AreEqual(message.ConversationId, (message2 as CanvasUnassignMessage).ConversationId);
            Assert.AreEqual(message.DisplayId, (message2 as CanvasUnassignMessage).DisplayId);
            Assert.AreEqual(message.State, (message2 as CanvasUnassignMessage).State);
        }
        [TestMethod]
        public void CreateCanvasMessage()
        {
            var message = new CreateCanvasMessage()
            {
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                ConversationId = new Tuple<Guid, short>(Guid.NewGuid(), 2)
            };

            byte[] bytes = message.Encode();

            Assert.AreEqual(123, bytes[0]);

            var message2 = Message.Decode(bytes);

            Assert.AreEqual(message.MessageNumber, (message2 as CreateCanvasMessage).MessageNumber);
            Assert.AreEqual(message.ConversationId, (message2 as CreateCanvasMessage).ConversationId);
        }
        [TestMethod]
        public void DeleteCanvasMessage()
        {
            var message = new DeleteCanvasMessage()
            {
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                ConversationId = new Tuple<Guid, short>(Guid.NewGuid(), 2),
                CanvasId = 1
            };

            byte[] bytes = message.Encode();

            Assert.AreEqual(123, bytes[0]);

            var message2 = Message.Decode(bytes);

            Assert.AreEqual(message.CanvasId, (message2 as DeleteCanvasMessage).CanvasId);
            Assert.AreEqual(message.MessageNumber, (message2 as DeleteCanvasMessage).MessageNumber);
            Assert.AreEqual(message.ConversationId, (message2 as DeleteCanvasMessage).ConversationId);
        }
        [TestMethod]
        public void DisplayListMessage()
        {
            var message = new DisplayListMessage()
            {
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                ConversationId = new Tuple<Guid, short>(Guid.NewGuid(), 2),
            };

            byte[] bytes = message.Encode();

            Assert.AreEqual(123, bytes[0]);

            var message2 = Message.Decode(bytes);

            Assert.AreEqual(message.ConversationId, (message2 as DisplayListMessage).ConversationId);
            Assert.AreEqual(message.MessageNumber, (message2 as DisplayListMessage).MessageNumber);
        }
        [TestMethod]
        public void GetCanvasListMessage()
        {
            var message = new GetCanvasListMessage()
            {
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                ConversationId = new Tuple<Guid, short>(Guid.NewGuid(), 2),
                AuthToken = Guid.NewGuid().ToByteArray()
            };

            byte[] bytes = message.Encode();

            Assert.AreEqual(123, bytes[0]);

            var message2 = Message.Decode(bytes);

            Assert.AreEqual(message.ConversationId, (message2 as GetCanvasListMessage).ConversationId);
            Assert.AreEqual(message.MessageNumber, (message2 as GetCanvasListMessage).MessageNumber);
            Assert.AreEqual(message.AuthToken.Length, (message2 as GetCanvasListMessage).AuthToken.Length);
        }
        [TestMethod]
        public void GetDisplayListMessage()
        {
            var message = new GetDisplayListMessage()
            {
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                ConversationId = new Tuple<Guid, short>(Guid.NewGuid(), 2),
                AuthToken = Guid.NewGuid().ToByteArray()
            };

            byte[] bytes = message.Encode();

            Assert.AreEqual(123, bytes[0]);

            var message2 = Message.Decode(bytes);

            Assert.AreEqual(message.ConversationId, (message2 as GetDisplayListMessage).ConversationId);
            Assert.AreEqual(message.MessageNumber, (message2 as GetDisplayListMessage).MessageNumber);
            Assert.AreEqual(message.AuthToken.Length, (message2 as GetDisplayListMessage).AuthToken.Length);
        }
        [TestMethod]
        public void RegisterAckMessage()
        {
            var message = new RegisterAckMessage()
            {
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                ConversationId = new Tuple<Guid, short>(Guid.NewGuid(), 2),
                DisplayId = 54
            };

            byte[] bytes = message.Encode();

            Assert.AreEqual(123, bytes[0]);

            var message2 = Message.Decode(bytes);

            Assert.AreEqual(message.ConversationId, (message2 as RegisterAckMessage).ConversationId);
            Assert.AreEqual(message.MessageNumber, (message2 as RegisterAckMessage).MessageNumber);
            Assert.AreEqual(message.DisplayId, (message2 as RegisterAckMessage).DisplayId);
        }
        [TestMethod]
        public void RegisterDisplayMessage()
        {
            var message = new RegisterDisplayMessage()
            {
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                ConversationId = new Tuple<Guid, short>(Guid.NewGuid(), 2),
                IPAddress = "127.0.0.1",
                Name = "Test"
            };

            byte[] bytes = message.Encode();

            Assert.AreEqual(123, bytes[0]);

            var message2 = Message.Decode(bytes);

            Assert.AreEqual(message.ConversationId, (message2 as RegisterDisplayMessage).ConversationId);
            Assert.AreEqual(message.MessageNumber, (message2 as RegisterDisplayMessage).MessageNumber);
            Assert.AreEqual(message.IPAddress, (message2 as RegisterDisplayMessage).IPAddress);
            Assert.AreEqual(message.Name, (message2 as RegisterDisplayMessage).Name);
        }
        [TestMethod]
        public void SubscribeCanvasMessage()
        {
            var message = new SubscriberCanvasMessage()
            {
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                ConversationId = new Tuple<Guid, short>(Guid.NewGuid(), 2),
                DisplayId = 54,
                CanvasId = 32
            };

            byte[] bytes = message.Encode();

            Assert.AreEqual(123, bytes[0]);

            var message2 = Message.Decode(bytes);

            Assert.AreEqual(message.ConversationId, (message2 as SubscriberCanvasMessage).ConversationId);
            Assert.AreEqual(message.MessageNumber, (message2 as SubscriberCanvasMessage).MessageNumber);
            Assert.AreEqual(message.DisplayId, (message2 as SubscriberCanvasMessage).DisplayId);
            Assert.AreEqual(message.CanvasId, (message2 as SubscriberCanvasMessage).CanvasId);
        }
        [TestMethod]
        public void TokenVerifyMessage()
        {
            var message = new TokenVerifyMessage()
            {
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                ConversationId = new Tuple<Guid, short>(Guid.NewGuid(), 2),
                IsAuthorized = false,
                AuthToken = Guid.NewGuid()
            };

            byte[] bytes = message.Encode();

            Assert.AreEqual(123, bytes[0]);

            var message2 = Message.Decode(bytes);

            Assert.AreEqual(message.ConversationId, (message2 as TokenVerifyMessage).ConversationId);
            Assert.AreEqual(message.MessageNumber, (message2 as TokenVerifyMessage).MessageNumber);
            Assert.AreEqual(message.IsAuthorized, (message2 as TokenVerifyMessage).IsAuthorized);
            Assert.AreEqual(message.AuthToken, (message2 as TokenVerifyMessage).AuthToken);
        }

        [TestMethod]
        public void AttemptLoginMessage()
        {
            var message = new AttemptLoginMessage
            {
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                ConversationId = new Tuple<Guid, short>(Guid.NewGuid(), 2),
                Username = "test",
                Password = "password"
            };

            byte[] bytes = message.Encode();

            Assert.AreEqual(123, bytes[0]);

            var message2 = Message.Decode(bytes);

            Assert.AreEqual(message.ConversationId, (message2 as AttemptLoginMessage).ConversationId);
            Assert.AreEqual(message.MessageNumber, (message2 as AttemptLoginMessage).MessageNumber);
            Assert.AreEqual(message.Username, (message2 as AttemptLoginMessage).Username);
            Assert.AreEqual(message.Password, (message2 as AttemptLoginMessage).Password);
        }

        [TestMethod]
        public void CreateUserMessage()
        {
            var message = new CreateUserMessage
            {
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                ConversationId = new Tuple<Guid, short>(Guid.NewGuid(), 2),
                Username = "test",
                Password = "password",
                AuthToken = new Guid().ToByteArray()
            };

            byte[] bytes = message.Encode();

            Assert.AreEqual(123, bytes[0]);

            var message2 = Message.Decode(bytes);

            Assert.AreEqual(message.ConversationId, (message2 as CreateUserMessage).ConversationId);
            Assert.AreEqual(message.MessageNumber, (message2 as CreateUserMessage).MessageNumber);
            Assert.AreEqual(message.Username, (message2 as CreateUserMessage).Username);
            Assert.AreEqual(message.Password, (message2 as CreateUserMessage).Password);
            Assert.AreEqual(message.AuthToken.Length, (message2 as CreateUserMessage).AuthToken.Length);
        }

        [TestMethod]
        public void DeleteUserMessage()
        {
            var message = new DeleteUserMessage
            {
                MessageNumber = new Tuple<Guid, short>(Guid.NewGuid(), 1),
                ConversationId = new Tuple<Guid, short>(Guid.NewGuid(), 2),
                Username = "test",
                Password = "password",
                AuthToken = Guid.NewGuid().ToByteArray()
            };

            byte[] bytes = message.Encode();

            Assert.AreEqual(123, bytes[0]);

            var message2 = Message.Decode(bytes);

            Assert.AreEqual(message.ConversationId, (message2 as DeleteUserMessage).ConversationId);
            Assert.AreEqual(message.MessageNumber, (message2 as DeleteUserMessage).MessageNumber);
            Assert.AreEqual(message.Username, (message2 as DeleteUserMessage).Username);
            Assert.AreEqual(message.Password, (message2 as DeleteUserMessage).Password);
            Assert.AreEqual(message.AuthToken.Length, (message2 as DeleteUserMessage).AuthToken.Length);
        }
    }
}
