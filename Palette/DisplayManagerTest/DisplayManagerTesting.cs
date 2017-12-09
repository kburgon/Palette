using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Messages;
using DisplayManagerAppLayer;
using System.Net;
using System.Collections.Generic;

namespace DisplayManagerTest
{
    [TestClass]
    public class DisplayManagerTesting
    {

        [TestMethod]
        public void AddDisplayTest()
        {
            DisplayManagerAppLayer.DisplayManager displayManager = new DisplayManagerAppLayer.DisplayManager();
            displayManager.AddDisplay("127.0.0.1");

            Assert.AreEqual(1, displayManager.GetDisplayCount());

            displayManager.AddDisplay("127.0.0.1");
            displayManager.AddDisplay("127.0.0.1");
            displayManager.AddDisplay("127.0.0.1");
            displayManager.AddDisplay("127.0.0.1");

            Assert.AreEqual(5, displayManager.GetDisplayCount());

            List<string> displayList = (List<string>)displayManager.GenerateIdList();

            Assert.AreEqual("1", displayList[0]);
            Assert.AreEqual("2", displayList[1]);
            Assert.AreEqual("3", displayList[2]);
            Assert.AreEqual("4", displayList[3]);
            Assert.AreEqual("5", displayList[4]);
        }

        [TestMethod]
        public void RemoveDisplayTest()
        {
            DisplayManagerAppLayer.DisplayManager displayManager = new DisplayManagerAppLayer.DisplayManager();
            displayManager.AddDisplay("127.0.0.1");
            displayManager.AddDisplay("127.0.0.1");
            displayManager.AddDisplay("127.0.0.1");
            displayManager.AddDisplay("127.0.0.1");

            Assert.AreEqual(4, displayManager.GetDisplayCount());

            displayManager.RemoveDisplay(3);
            Assert.AreEqual(3, displayManager.GetDisplayCount());

            List<string> list = (List<string>)displayManager.GenerateIdList();

            Assert.AreEqual("1", list[0]);
            Assert.AreEqual("2", list[1]);
            Assert.AreNotEqual("3", list[2]);
            Assert.AreEqual("4", list[2]);
        }

        [TestMethod]
        public void RemoveDisplayBadTest()
        {
            DisplayManagerAppLayer.DisplayManager displayManager = new DisplayManagerAppLayer.DisplayManager();
            displayManager.AddDisplay("127.0.0.1");
            displayManager.AddDisplay("127.0.0.1");
            displayManager.AddDisplay("127.0.0.1");
            displayManager.AddDisplay("127.0.0.1");

            Assert.AreEqual(4, displayManager.GetDisplayCount());

            displayManager.RemoveDisplay(7);
            Assert.AreNotEqual(3, displayManager.GetDisplayCount());
            Assert.AreEqual(4, displayManager.GetDisplayCount());
        }
    }
}
