using SharedAppLayer.Entitities;

namespace CanvasStorageManager.DataPersistence
{
    internal class FileDataStore
    {
        public SimpleFileCollection<Canvas> Canvases
            = new SimpleFileCollection<Canvas>();
    }
}
