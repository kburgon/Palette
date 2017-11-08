using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using SharedAppLayer.Entitities;

namespace CanvasStorageManager_Test.DataPersistence
{
    internal class SimpleFileCollection<T> where T : IHasKey, new()
    {
        private static readonly string FileName =
            typeof(T).FullName + ".json";

        public SimpleFileCollection()
        {
            lock (FileName)
            {
                if (File.Exists(FileName)) return;
                File.Create(FileName).Close();
                WriteFileJson(new FileJson<T>
                {
                    CurrentId = 0,
                    Records = new List<T>()
                });
            }
        }

        public List<T> ToList()
        {
            lock (FileName)
            {
                var fileJson = ReadFileJson();
                return fileJson.Records;
            }
        }

        private static FileJson<T> ReadFileJson()
        {
            var fileStream = new StreamReader(FileName, Encoding.UTF8);
            var rawJson = fileStream.ReadToEnd();
            var fileJson = JsonConvert.DeserializeObject<FileJson<T>>(rawJson);
            fileStream.Close();
            return fileJson;
        }

        private static void WriteFileJson(FileJson<T> fileJson)
        {
            var fileStream = new StreamWriter(FileName);
            var rawJson = JsonConvert.SerializeObject(fileJson);
            fileStream.Write(rawJson);
            fileStream.Close();
        }
    }

    internal class FileJson<T>
    {
        public uint CurrentId { get; set; }
        public List<T> Records { get; set; }
    }
}
