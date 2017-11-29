using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
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

            _adminClient = new AdminClient
            {
                CreatedCanvasIdHandler = HandleCanvasIdUpdate,
                DeleteCanvasHandler = HandleCanvasDelete
            };
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

        private void HandleCanvasIdUpdate(int canvasId)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new CreatedCanvasIdHandler(HandleCanvasIdUpdate), canvasId);
                return;
            }

            MessageBox.Show($"Canvas with ID {canvasId} has been created.");
        }

        private void HandleCanvasDelete(int canvasId)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new DeleteCanvasHandler(HandleCanvasDelete), canvasId);
                return;
            }

            MessageBox.Show($"Canvas with ID {canvasId} has been deleted.");
        }

        private void HandleCanvasListRequest(IEnumerable<SharedAppLayer.Entitities.Canvas> canvases)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new GetCanvasListHandler(HandleCanvasListRequest), canvases);
                return;
            }

            CanvasIdListBox.ItemsSource = canvases;
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
            AdminClientUsersTab.Visibility = Visibility.Visible;
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

            _adminClient.CanvasManagerIpAddress = CanvasManagerIpAddressTextBox.Text;
            _adminClient.CanvasManagerPortNumber = Convert.ToInt32(CanvasManagerPort.Text);
            _adminClient.StartDispatcher(port);
            _adminClient.RequestCanvasList();
        }

        private void CreateCanvasButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(CanvasManagerPort.Text, out var port) || !IsValidIpAddress(CanvasManagerIpAddressTextBox.Text))
            {
                MessageBox.Show("Please enter a valid IP address and port.");
                return;
            }

            _adminClient.CanvasManagerIpAddress = CanvasManagerIpAddressTextBox.Text;
            _adminClient.CanvasManagerPortNumber = Convert.ToInt32(CanvasManagerPort.Text);
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

            _adminClient.CanvasManagerIpAddress = CanvasManagerIpAddressTextBox.Text;
            _adminClient.CanvasManagerPortNumber = Convert.ToInt32(CanvasManagerPort.Text);
            _adminClient.StartDispatcher(port);

            try
            {
                var canvas = ((SharedAppLayer.Entitities.Canvas) CanvasIdListBox.SelectedItem);
                _adminClient.DeleteCanvas(canvas.CanvasId);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No canvas was selected to delete.");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _adminClient.CloseDispatcher();
        }
    }
}
