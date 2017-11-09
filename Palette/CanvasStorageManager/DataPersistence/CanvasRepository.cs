using System.Collections.Generic;
using SharedAppLayer.Entitities;

namespace CanvasStorageManager.DataPersistence
{
    internal class CanvasRepository
    {
        private readonly FileDataStore _dataStore;

        public CanvasRepository(FileDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public IEnumerable<Canvas> GetAll()
        {
            return _dataStore.Canvases.ToList();
        }

        public Canvas CreateNew()
        {
            var canvas = new Canvas();
            _dataStore.Canvases.Add(canvas);
            return canvas;
        }

        public void Delete(int victimId)
        {
            var victim = new Canvas { CanvasId = victimId };
            _dataStore.Canvases.Remove(victim);
        }
    }
}
