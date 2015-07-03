using System;

namespace Simulation
{
    class Program
    {

        // 123123213
        private static Simulation simulation;

        static void Main()
        {
            // Welcome message
            Console.WriteLine(
                @"Welcome to THE SIMULATION

Commands:
1. ""run"" - Run the simulation (Virtual cells will be created and their proteins will be dispayed)
2. ""stop"" - Stop the simulation
==================================================
");

            // Start Simulation
            simulation = new Simulation();

            // Input commands
            while (true)
            {
                string input = Console.ReadLine();
                Command(input);
            }
        }

        static void Command(string cmd)
        {
            switch (cmd)
            {
                case "run":
                    simulation.Run();
                    break;

                case "stop":
                    simulation.Stop();
                    break;

                default:
                    Console.WriteLine("\"" + cmd + "\" command not found");
                    break;
            }
        }

    }
}
