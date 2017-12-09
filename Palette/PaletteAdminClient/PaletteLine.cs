using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using Brushes = System.Windows.Media.Brushes;

namespace Display
{
    internal class PaletteLine
    {
        private readonly Line _line = new Line();
        private bool _isDrawing;

        internal PaletteLine()
        {
            _line.Stroke = Brushes.LightSlateGray;
        }

        internal void AddTo(StackPanel displayCanvas)
        {
            if (displayCanvas.Children.Contains(_line))
                displayCanvas.Children.Add(_line);
        }

        internal void StartDrawing(Grid displayCanvas, Point firstPoint)
        {
            if (displayCanvas.Children.Contains(_line))
                displayCanvas.Children.Remove(_line);
            _line.X1 = firstPoint.X;
            _line.Y1 = firstPoint.Y;
            _line.X2 = firstPoint.X;
            _line.Y2 = firstPoint.Y;
            displayCanvas.Children.Add(_line);
            _isDrawing = true;
        }

        internal void Update(Point secondPoint)
        {
            if (!_isDrawing) return;
            _line.X2 = secondPoint.X;
            _line.Y2 = secondPoint.Y;
        }

        internal void StopDrawing()
        {
            _isDrawing = false;
        }

        public Line GetPoints()
        {
            return _line;
        }
    }
}
