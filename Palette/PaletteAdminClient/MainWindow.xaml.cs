using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AdminClientAppLayer;

namespace PaletteAdminClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static AdminClient _adminClient;

        public MainWindow()
        {
            InitializeComponent();

            _adminClient = new AdminClient();
        }

        private static bool IsValidIpAddress(string ip)
        {
            try
            {
                var convertedIp = IPAddress.Parse(ip);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void ConnectCanvasButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelCanvasConnectButton_Click(object sender, RoutedEventArgs e)
        {
            CanvasIdConnectGrid.Visibility = Visibility.Hidden;
            OpenCanvasEntryButton.Visibility = Visibility.Visible;
        }

        private void OpenCanvasEntryButton_Click(object sender, RoutedEventArgs e)
        {
            CanvasIdConnectGrid.Visibility = Visibility.Visible;
            OpenCanvasEntryButton.Visibility = Visibility.Hidden;
        }

        private void CancelAdminLoginButton_Click(object sender, RoutedEventArgs e)
        {
            AdminLoginCredentialsGrid.Visibility = Visibility.Hidden;
            OpenAdminLoginCredentialsButton.Visibility = Visibility.Visible;
        }

        private void SubmitAdminLoginButton_Click(object sender, RoutedEventArgs e)
        {
            PaletteClientTabItem.Visibility = Visibility.Visible;
            AdminClientTabItem.Visibility = Visibility.Visible;
        }

        private void OpenAdminLoginCredentialsButton_Click(object sender, RoutedEventArgs e)
        {
            AdminLoginCredentialsGrid.Visibility = Visibility.Visible;
            OpenAdminLoginCredentialsButton.Visibility = Visibility.Hidden;
        }

        private void GetCanvasListButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(CanvasManagerPort.Text, out var port) || !IsValidIpAddress(CanvasManagerIpAddressTextBox.Text))
            {
                MessageBox.Show("Please enter a valid IP address and port.");
                return;
            }

            _adminClient.StartDispatcher(port);
            _adminClient.CreateCanvas();
        }

        private void CreateCanvasButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(CanvasManagerPort.Text, out var port) || !IsValidIpAddress(CanvasManagerIpAddressTextBox.Text))
            {
                MessageBox.Show("Please enter a valid IP address and port.");
                return;
            }

            _adminClient.StartDispatcher(port);
            _adminClient.CreateCanvas();
        }

        private void DeleteCanvasButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(CanvasManagerPort.Text, out var port) || !IsValidIpAddress(CanvasManagerIpAddressTextBox.Text))
            {
                MessageBox.Show("Please enter a valid IP address and port.");
                return;
            }

            _adminClient.StartDispatcher(port);
            _adminClient.DeleteCanvas();
        }
    }
}
