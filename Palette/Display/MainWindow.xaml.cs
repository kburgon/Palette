using System.Windows;
using DisplayAppLayer;
using System.Net;
using System;
using System.Drawing;
using System.Windows.Shapes;
using Brushes = System.Windows.Media.Brushes;
using Point = System.Windows.Point;

namespace Display
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static DisplayAppLayer.Display _displayAppLayer;
        private const string DefaultAddress = "127.0.0.1";
        private const string DefaultPort = "0";
        private IPAddress DMAddress;
        private int DMPort;
        private IPAddress CMAddress;
        private int CMPort;
        private int DisplayPort;

        public MainWindow()
        {
            InitializeComponent();

            DisplayPortTextBox.Text = "12200";

            DisplayManagerAddressTextBox.Text = DefaultAddress;
            DisplayManagerPortTextBox.Text = "12250";

            CanvasManagerAddressTextBox.Text = DefaultAddress;
            CanvasManagerPortTextBox.Text = "12345";

            _displayAppLayer = new DisplayAppLayer.Display();

        }

        private static bool IsValidIpAddress(string ip, out IPAddress convertedIp)
        {
            try
            {
                convertedIp = IPAddress.Parse(ip);
                return true;
            }
            catch (Exception)
            {
                convertedIp = null;
                return false;
            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if(!int.TryParse(DisplayPortTextBox.Text, out var DisplayPort))
            {
                MessageBox.Show("Please enter a valid port for the display");
                return;
            }

            if (!int.TryParse(DisplayManagerPortTextBox.Text, out var DMPort) || !IsValidIpAddress(DisplayManagerAddressTextBox.Text, out DMAddress))
            {
                MessageBox.Show("Please enter a valid IP address and port for Display Manager.");
                return;
            }
            if (!int.TryParse(CanvasManagerPortTextBox.Text, out var CMPort) || !IsValidIpAddress(CanvasManagerAddressTextBox.Text, out CMAddress))
            {
                MessageBox.Show("Please enter a valid IP address and port for Canvas Manager.");
                return;
            }
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            _displayAppLayer.StartDispatcher(12200);
            _displayAppLayer.RegisterDisplay();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _displayAppLayer.StopDispatcher();
        }
    }
}
