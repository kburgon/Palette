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
                    var portNumber = Convert.ToInt32(args[0]);
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
            PrintInstructions();

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
                        Console.WriteLine("Display Manager's port successfully updated.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to update the Display Manager's port.");
                        Console.WriteLine("Port must be a number.");
                    }

                    PrintInstructions();
                }
                else if (command == "update_storage_port")
                {
                    Console.WriteLine("Enter new port: ");
                    var newPort = Console.ReadLine();
                    int port;
                    if (Int32.TryParse(newPort, out port))
                    {
                        _canvasManagerApp.UpdateStorageManagerPort(port);
                        Console.WriteLine("Storage Manager's port successfully updated.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to update Storage Manager's port.");
                        Console.WriteLine("Port must be a number.");
                    }

                    PrintInstructions();
                }
                else if (command == "update_storage_endpoint")
                {
                    Console.WriteLine("Enter Storage Manager's new address: ");
                    var newAddress = Console.ReadLine();

                    Console.WriteLine("Enter Storage Manager's new port: ");
                    var newPort = Console.ReadLine();

                    IPAddress address;
                    var port = 0;
                    if(Int32.TryParse(newPort, out port) && IPAddress.TryParse(newAddress, out address))
                    {
                        _canvasManagerApp.UpdateStorageManagerEndpoint(port, address);
                        Console.WriteLine("Storage Manager's endpoint successfully updated.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to update Storage Manager's endpoint.");
                        Console.WriteLine("Bad Address or Port.");
                    }

                    PrintInstructions();
                }
                else if (command == "update_auth_endpoint")
                {
                    Console.WriteLine("Enter the Auth Manager's new address: ");
                    var newAddress = Console.ReadLine();

                    Console.WriteLine("Enter the Auth Manager's new port: ");
                    var newPort = Console.ReadLine();

                    var port = 0;
                    IPAddress address;
                    if (Int32.TryParse(newPort, out port) && IPAddress.TryParse(newAddress, out address))
                    {
                        _canvasManagerApp.UpdateAuthManagerEndPoint(port, address);
                        Console.WriteLine("Auth Manager's endpoint successfully updated.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to update Auth Manager's endpoint");
                        Console.WriteLine("Bad Address or Port.");
                    }

                    PrintInstructions();
                }
                else if (command == "update_auth_port")
                {
                    Console.WriteLine("Enter the Auth Manager's new port: ");
                    var newPort = Console.ReadLine();
                    var port = 0;

                    if (Int32.TryParse(newPort, out port))
                    {
                        _canvasManagerApp.UpdateAuthManagerPort(port);
                        Console.WriteLine("Auth Manager's port successfully updated.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to update Auth Manager's port.");
                        Console.WriteLine("Port must be a number.");
                    }

                    PrintInstructions();
                }
                else
                {
                    Console.WriteLine($"Command \"{command}\" not recognized.");
                }
            }
        }

        private static void PrintInstructions()
        {
            Console.WriteLine("Type \"update_storage_endpoint\" to set Storage Manager's endpoint.");
            Console.WriteLine("Type \"update_storage_port\" to set Storage Manager's port.");
            Console.WriteLine("Type \"update_auth_endpoint\" to change the auth manager's endpoint.");
            Console.WriteLine("Type \"update_auth_port\" to change the auth manager's port.");
            Console.WriteLine("Type \"port\" to set Canvas Manager's port.");
            Console.WriteLine("Type \"exit\" to close application.");
        }
    }
}
