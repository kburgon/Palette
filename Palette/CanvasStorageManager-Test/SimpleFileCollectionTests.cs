using System.IO;
using CanvasStorageManager_Test.DataPersistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedAppLayer.Entitities;
using Shouldly;

namespace CanvasStorageManager_Test
{
    [TestClass]
    public class SimpleFileCollectionTests
    {
        [TestMethod]
        public void CanReadRecords()
        {
            var fileCollection = new SimpleFileCollection<RecordStub>();
            var records = fileCollection.ToList();
            records.ShouldNotBeNull();
        }

        [TestCleanup]
        public void CleanupFile()
        {
            File.Delete("CanvasStorageManager_Test.RecordStub.json");
        }
    }

    public class RecordStub : IHasKey
    {
        public uint Key { get; set; }
    }
}
