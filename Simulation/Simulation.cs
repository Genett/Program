using System;
using System.Threading;

namespace Simulation
{
	public class Simulation
	{
		public int number; // Main variable


		/* States of The Simulation */
		private enum State
		{
			Running, // Running state
			Stopped // Stopped state
		}

		private State currentState; // Current state

		public Simulation()
		{
			Thread thread = new Thread(Live); // Create a new thread
			thread.Start();

			number = 0;

			// Default state
			currentState = State.Stopped;
		}
			
		private void Live()
		{
			while(true)
			{
				if (currentState == State.Running)
				{
					number++;
					Thread.Sleep(1000); // Delay for 1 second
				}
			}
		}

		public void Run()
		{
			currentState = State.Running;

			Console.WriteLine("Done.");
		}

		public void Stop()
		{
			currentState = State.Stopped;

			Console.WriteLine("Done.");
		}
	}
}