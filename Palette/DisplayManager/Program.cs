using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine("Type \"add\" to add a new display's address.");
            Console.WriteLine("Type \"remove\" to remove a display's address.");
            Console.WriteLine("Type \"edit\" to edit a display's address.");
            Console.WriteLine("Type \"update_auth_endpoint\" to change the auth manager's endpoint.");
            Console.WriteLine("Type \"update_auth_port\" to change the auth manager's port.");
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
                if(command == "add")
                {
                    PrintInstructions();
                }
                else if(command == "remove")
                {
                    PrintInstructions();
                }
                else if(command == "edit")
                {
                    PrintInstructions();
                }
                else if(command == "exit")
                {
                    exit = true;
                }
                else if(command == "update_auth_endpoint")
                {
                    Console.WriteLine("Enter the Auth Manager's new address: ");
                    var newAddress = Console.ReadLine();

                    Console.WriteLine("Enter the Auth Manager's new port: ");
                    var newPort = Console.ReadLine();

                    var port = 0;
                    if (Int32.TryParse(newPort, out port))
                        _displayManagerApp.UpdateAuthManagerEndPoint(port, newAddress);

                    PrintInstructions();
                }
                else if (command == "update_auth_port")
                {
                    PrintInstructions();
                }
                else
                {
                    Console.WriteLine("Command not recognized.");
                    PrintInstructions();
                }
            }
        }

        private static void SetPort(int port)
        {

        }
    }
}
