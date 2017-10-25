using System;
using System.Collections.Generic;
using System.Linq;
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

namespace PaletteAdminClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
    }
}
