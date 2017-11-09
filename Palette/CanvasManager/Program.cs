using System;
using System.Linq;
using System.Net;

namespace CanvasManager
{
    static class Program
    {
        private static CanvasManagerAppLayer.CanvasManager _canvasManagerApp;

        private static void Main(string[] args)
        {
            const int defaultPortNumber = 12345;
            const string defaultAddress = "127.0.0.1";

            _canvasManagerApp = new CanvasManagerAppLayer.CanvasManager();

            if (!args.Any())
            {
                Console.WriteLine($"Starting Canvas Manager on default address: default port {defaultAddress}:{defaultPortNumber}...");
                SetAddressAndPortNumber(defaultAddress, defaultPortNumber);
            }
            else
            {
                try
                {
                    var address = args[0];
                    var portNumber = Convert.ToInt32(args[1]);
                    Console.WriteLine($"Starting Canvas Manager on port {portNumber}...");
                    SetAddressAndPortNumber(address, portNumber);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return;
                }
            }

            WaitForCommand();

            _canvasManagerApp.CloseDispatcher();
        }

        private static void SetAddressAndPortNumber(string address, int portNumber)
        {
            _canvasManagerApp.StartDispatcher(address, portNumber);
        }

        private static void WaitForCommand()
        {
            Console.WriteLine("Type \"address\" to update the address.");
            Console.WriteLine("Type \"port\" to update the port.");
            Console.WriteLine("Type \"exit\" to close application.");

            var hasSentExitCommand = false;
            while (!hasSentExitCommand)
            {
                var command = Console.ReadLine();

                if (command == "exit")
                {
                    hasSentExitCommand = true;
                }
                else if (command == "port")
                {
                    Console.WriteLine("Enter new port: ");
                    var newPort = Console.ReadLine();
                    int port;
                    if (Int32.TryParse(newPort, out port))
                    {
                        _canvasManagerApp.UpdatePort(port);
                    }
                    else
                    {
                        Console.WriteLine("Port must be a number.");
                    }
                }
                else if (command == "address")
                {
                    Console.WriteLine("Enter new address: ");
                    var newAddress = Console.ReadLine();
                    IPAddress address;
                    if(IPAddress.TryParse(newAddress, out address))
                    {
                        _canvasManagerApp.UpdateAddress(address);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Address.");
                    }
                }
                else
                {
                    Console.WriteLine($"Command \"{command}\" not recognized.");
                }
            }
        }
    }
}
