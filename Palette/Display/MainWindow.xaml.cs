﻿using System.Windows;
using DisplayAppLayer;
using System.Net;
using System;

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

            DisplayPortTextBox.Text = DefaultPort;

            DisplayManagerAddressTextBox.Text = DefaultAddress;
            DisplayManagerPortTextBox.Text = DefaultPort;

            CanvasManagerAddressTextBox.Text = DefaultAddress;
            CanvasManagerPortTextBox.Text = DefaultPort;

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
    }
}
