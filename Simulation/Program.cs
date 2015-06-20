using System;

namespace Simulation
{
	class Program
	{
		private static Simulation simulation;

		static void Main()
		{
			// Welcome message
			Console.WriteLine(
				@"Welcome to THE SIMULATION

Commands:
1. ""run"" - Run the simulation (A 'number' variable will increment each second)
2. ""stop"" - Stop the simulation
3. ""print_number"" - Print the 'number' variable
4. ""clean_number"" - Clean the 'number' variable
==================================================
");

			// Start Simulation
			simulation = new Simulation();

			// Input commands
			while(true)
			{
				string input = Console.ReadLine();
				Command(input);
			}
		}

		static void Command(string cmd)
		{
			switch(cmd)
			{
			case "run":
				simulation.Run();
			break;

			case "stop":
				simulation.Stop();
			break;

			case "print_number":
				Console.WriteLine(simulation.number);
			break;

			case "clean_number":
				simulation.number = 0;
				Console.WriteLine(simulation.number);
			break;

			default:
				Console.WriteLine("\"" + cmd + "\" command not found");
			break;
			}
		}
	}
}