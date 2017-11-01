using System;
using System.Linq;

namespace CanvasManager
{
    class Program
    {
        private static CanvasManagerAppLayer.CanvasManager _canvasManagerApp;

        static void Main(string[] args)
        {
            if (args.Count() < 1)
            {
                var defaultPortNumber = 12345;
                Console.WriteLine($"Starting Canvas Manager on default port {defaultPortNumber}...");
                SetPortNumber(defaultPortNumber);
            }
            else
            {
                try
                {
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

            WaitForExitCommand();

            _canvasManagerApp.CloseDispatcher();
        }

        private static void SetPortNumber(int portNumber)
        {
            _canvasManagerApp = new CanvasManagerAppLayer.CanvasManager();
            _canvasManagerApp.StartDispatcher(portNumber);
        }

        private static void WaitForExitCommand()
        {
            Console.WriteLine("Type \"exit\" to close application.");

            var hasSentExitCommand = false;
            while (!hasSentExitCommand)
            {
                var command = Console.ReadLine();

                if (command == "exit")
                {
                    hasSentExitCommand = true;
                }
                else
                {
                    Console.WriteLine($"Command \"{command}\" not recognized.");
                }
            }
        }
    }
}
