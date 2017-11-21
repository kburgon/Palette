using System;
using System.Linq;
using System.Net;

namespace DisplayManager
{
    class Program
    {
        private static DisplayManagerAppLayer.DisplayManager _displayManagerApp;
        static void Main(string[] args)
        {
            const int defaultPortNumber = 12345;
            Console.WriteLine("Display Manager Started.");
            _displayManagerApp = new DisplayManagerAppLayer.DisplayManager();

            if (!args.Any())
            {
                Console.WriteLine("Using default port: {0}", defaultPortNumber);
                SetPort(defaultPortNumber);
            }
            else if(args.Count() == 1)
            {
                try
                {
                    var portNumber = 0;
                    if (Int32.TryParse(args[0], out portNumber))
                    {
                        SetPort(portNumber);
                        Console.WriteLine("Using port: {0}", portNumber);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error: {0}", e);
                }
            }

            GetCommands();
        }
        private static void PrintInstructions()
        {
            Console.WriteLine("Type \"port\" to update the Display Manager's port.");
            Console.WriteLine("Type \"update_auth_endpoint\" to change the auth manager's endpoint.");
            Console.WriteLine("Type \"update_auth_port\" to change the auth manager's port.");
            Console.WriteLine("Type \"remove\" to remove a display's address.");
            Console.WriteLine("Type \"exit\" to exit the Display Manager.");
        }

        private static void GetCommands()
        {
            bool exit = false;
            string command = String.Empty;
            PrintInstructions();
            while(!exit)
            {
                command = Console.ReadLine();

                if(command == "port")
                {
                    Console.WriteLine("Enter the Display Manager's new port: ");
                    var newPort = Console.ReadLine();

                    var port = 0;
                    if (Int32.TryParse(newPort, out port))
                    {
                        _displayManagerApp.UpdateDisplayManagerPort(port);
                        Console.WriteLine("Display Manager's port successfully updated.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to update the Display Manager's port.");
                        Console.WriteLine("Bad port number.");
                    }
                    PrintInstructions();
                }
                else if(command == "remove")
                {
                    Console.WriteLine("Enter the display Id of the display that you wish to delete: ");
                    var id = Console.ReadLine();

                    var displayId = 0;
                    if (Int32.TryParse(id, out displayId))
                    {
                        _displayManagerApp.RemoveDisplay(displayId);
                    }
                    else
                    {
                        Console.WriteLine($"Failed to remove display {id}.");
                        Console.WriteLine("Display id does not exist.");
                    }


                    PrintInstructions();
                }
                else if(command == "update_auth_endpoint")
                {
                    Console.WriteLine("Enter the Auth Manager's new address: ");
                    var newAddress = Console.ReadLine();

                    Console.WriteLine("Enter the Auth Manager's new port: ");
                    var newPort = Console.ReadLine();

                    var port = 0;
                    IPAddress address;
                    if (Int32.TryParse(newPort, out port) && IPAddress.TryParse(newAddress, out address))
                    {
                        _displayManagerApp.UpdateAuthManagerEndPoint(port, address);
                        Console.WriteLine("Auth Manager's endpoint successfully updated.");
                    }
                    else
                        Console.WriteLine("Failed to update Auth Manager's endpoint");

                    PrintInstructions();
                }
                else if (command == "update_auth_port")
                {
                    Console.WriteLine("Enter the Auth Manager's new port: ");
                    var newPort = Console.ReadLine();
                    var port = 0;

                    if (Int32.TryParse(newPort, out port))
                    {
                        _displayManagerApp.UpdateAuthManagerPort(port);
                        Console.WriteLine("Auth Manager's port successfully updated.");
                    }
                    else
                        Console.WriteLine("Failed to update Auth Manager's port.");

                    PrintInstructions();
                }
                else if(command == "exit")
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine($"Command \"{command}\" not recognized.");
                    PrintInstructions();
                }
            }
        }

        private static void SetPort(int port)
        {

        }
    }
}
