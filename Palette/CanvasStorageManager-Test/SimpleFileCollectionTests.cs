using System;
using System.IO;
using System.Linq.Expressions;
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

        [TestMethod]
        public void CanAddRecord()
        {
            var fileCollection = new SimpleFileCollection<RecordStub>();
            var testRecord = new RecordStub {TestValue = "This is a test"};
            fileCollection.Add(testRecord);
            var records = fileCollection.ToList();
            records.ShouldContain(Is(testRecord));
        }

        private static Expression<Func<RecordStub, bool>> Is(RecordStub testRecord)
        {
            return r =>
                r.Key == testRecord.Key &&
                r.TestValue == testRecord.TestValue;
        }

        [TestMethod]
        public void IncrementsKey()
        {
            var fileCollection = new SimpleFileCollection<RecordStub>();
            var testRecord1 = new RecordStub();
            var testRecord2 = new RecordStub();
            fileCollection.Add(testRecord1);
            fileCollection.Add(testRecord2);
            testRecord1.Key.ShouldBe(0u);
            testRecord2.Key.ShouldBe(1u);
        }

        [TestMethod]
        public void CanUpdateRecord()
        {
            const string previousValue = "Test 1";
            const string newValue = "Test 2";
            var fileCollection = new SimpleFileCollection<RecordStub>();
            var testRecord = new RecordStub {TestValue = previousValue};
            fileCollection.Add(testRecord);
            var addedRecord = fileCollection.SingleOrDefault(Matches(testRecord));
            addedRecord.TestValue.ShouldBe(previousValue);

            testRecord.TestValue = newValue;
            fileCollection.Update(testRecord);
            var updatedRecord = fileCollection.SingleOrDefault(Matches(testRecord));

            updatedRecord.TestValue.ShouldBe(newValue);

        }

        private static Func<RecordStub, bool> Matches(RecordStub testRecord)
        {
            return r => r.Key == testRecord.Key;
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

        public string TestValue { get; set; }
    }
}
