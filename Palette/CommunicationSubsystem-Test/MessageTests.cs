using System;
using System.Collections.Generic;
using CommunicationSubsystem.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnassignMessage
    {
        [TestMethod]
        public void BrushStrokeMessageTest()
        {
            var brushStroke = new BrushStrokeMessage();
            brushStroke.Points = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(3, 4)
            };

            Assert.AreEqual(new Tuple<int, int>(1, 2), brushStroke.Points.First());
        }
    }
}
