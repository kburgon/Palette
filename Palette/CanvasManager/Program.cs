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

            _canvasManagerApp = new CanvasManagerAppLayer.CanvasManager();

            if (!args.Any())
            {
                Console.WriteLine($"Starting Canvas Manager on default port {defaultPortNumber}...");
                SetPortNumber(defaultPortNumber);
            }
            else
            {
                try
                {
                    var address = args[0];
                    var portNumber = Convert.ToInt32(args[1]);
                    Console.WriteLine($"Starting Canvas Manager on port {portNumber}...");
                    SetPortNumber(portNumber);
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

        private static void SetPortNumber(int portNumber)
        {
            _canvasManagerApp.StartDispatcher(portNumber);
        }

        private static void WaitForCommand()
        {
            Console.WriteLine("Type \"S_address\" to set Storage Manager's address.");
            Console.WriteLine("Type \"S_port\" to set Storage Manager's port.");
            Console.WriteLine("Type \"port\" to set Canvas Manager's port.");
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
                        _canvasManagerApp.UpdateCanvasManagerPort(port);
                    }
                    else
                    {
                        Console.WriteLine("Port must be a number.");
                    }
                }
                else if (command == "S_port")
                {
                    Console.WriteLine("Enter new port: ");
                    var newPort = Console.ReadLine();
                    int port;
                    if (Int32.TryParse(newPort, out port))
                    {
                        _canvasManagerApp.UpdateStorageManagerPort(port);
                    }
                    else
                    {
                        Console.WriteLine("Port must be a number.");
                    }
                }
                else if (command == "S_address")
                {
                    Console.WriteLine("Enter new address: ");
                    var newAddress = Console.ReadLine();
                    IPAddress address;
                    if(IPAddress.TryParse(newAddress, out address))
                    {
                        _canvasManagerApp.UpdateStorageManagerAddress(address);
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
