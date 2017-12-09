using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DisplayManagerAppLayer;
using DisplayAppLayer;
using System.Net;

namespace DisplayTests
{
    [TestClass]
    public class DisplayTest
    {
        [TestMethod]
        public void UpdateDisplayManagerEndPointTest()
        {
            Display display = new Display();

            display.UpdateDisplayManagerAddress("127.0.1.1");

            Assert.AreNotEqual(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12250), display.DisplayManagerEP);
            Assert.AreEqual(new IPEndPoint(IPAddress.Parse("127.0.1.1"), 12250), display.DisplayManagerEP);

            display.UpdateDisplayManagerPort(12342);
            Assert.AreNotEqual(new IPEndPoint(IPAddress.Parse("127.0.1.1"), 12250), display.DisplayManagerEP);
            Assert.AreEqual(new IPEndPoint(IPAddress.Parse("127.0.1.1"), 12342), display.DisplayManagerEP);
        }

        [TestMethod]
        public void UpdateCanvasManagerEndPointTest()
        {
            Display display = new Display();

            display.UpdateCanvasManagerAddress("127.0.1.1");

            Assert.AreNotEqual(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345), display.CanvasManagerEP);
            Assert.AreEqual(new IPEndPoint(IPAddress.Parse("127.0.1.1"), 12345), display.CanvasManagerEP);

            display.UpdateCanvasManagerPort(12342);
            Assert.AreNotEqual(new IPEndPoint(IPAddress.Parse("127.0.1.1"), 12345), display.CanvasManagerEP);
            Assert.AreEqual(new IPEndPoint(IPAddress.Parse("127.0.1.1"), 12342), display.CanvasManagerEP);
        }
    }
}
