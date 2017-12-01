using System;
using System.Linq;
using log4net;
using log4net.Config;

namespace AuthManager
{
    class Program
    {
        private static ILog Logger = LogManager.GetLogger(typeof(Program));
        private static AuthManagerAppLayer.AuthManager _authManager;

        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            const int defaultPortNumber = 12001;
            int portNumber;

            _authManager = new AuthManagerAppLayer.AuthManager();

            if (!args.Any())
            {
                Console.WriteLine($"Starting Auth Manager on default port {defaultPortNumber}.");
                portNumber = defaultPortNumber;
            }
            else
            {
                try
                {
                    portNumber = Convert.ToInt32(args[0]);
                    Console.WriteLine($"Starting Auth Manager on port {portNumber}.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return;
                }
            }

            _authManager.StartDispatcher(portNumber);
            HandleCommands();
            _authManager.CloseDispatcher();
        }

        private static void HandleCommands()
        {
            Console.WriteLine("Type \"port [number]\" to set Auth Manager's port.");
            Console.WriteLine("Type \"exit\" to close application");
            var shouldExit = false;
            while (!shouldExit)
            {
                shouldExit = DetermineCommand();
            }
        }

        private static bool DetermineCommand()
        {
            var command = Console.ReadLine()?.Split(' ') ?? new[] { string.Empty };
            if (command.First() == "exit")
            {
                return true;
            }
            if (command.First() == "port")
            {
                UpdateLocalPort(command);
            }
            else
            {
                Console.WriteLine($"Command \"{command}\" not recognized.");
            }

            return false;
        }

        private static void UpdateLocalPort(string[] command)
        {
            if (Int32.TryParse(command.Last(), out var port))
            {
                _authManager.UpdateLocalPort(port);
            }
            else
            {
                Console.WriteLine($"Argument {command.Last()} invalid.  Port must be a number.");
            }
        }
    }
}
