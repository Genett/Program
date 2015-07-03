using System;
using System.Threading;

namespace Simulation
{
    class Simulation
    {
        private int sleepTime = 10;

        public int SleepTime { set { sleepTime = value; } }

        public int number; // Main variable 123


        /* States of simulation */
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
            while (true)
            {
                if (currentState == State.Running)
                {
                    number++;


                    Cell cell = new Cell();  //create a new cell

                    string[] gene = cell.Genome;
                    string[] protein = cell.Protein; //get the cell's properties
                    string[] mRNA = cell.mRNA;

                    Console.WriteLine("Cell " + number.ToString() + ":"); //identify the cell
                    
                    Console.WriteLine("DNA: " + cell.DNA + "\n\n"); //print its DNA

                    for (int i = 0; i < gene.Length; i++) {
                        Console.WriteLine("Gene " + (i+1) + ": " + gene[i] + "\n"); //print its genome
                    }
                    Console.WriteLine("\n");

                    for (int i = 0; i < mRNA.Length; i++) {
                        Console.WriteLine("mRNA " + (i + 1) + ": " + mRNA[i] + "\n"); //print its mRNA's
                    }
                    Console.WriteLine("\n");

                    for (int i = 0; i < protein.Length; i++) {
                        Console.WriteLine("Protein " + (i+1) + ": " + protein[i] + "\n"); //print its proteins
                    }

                    Console.WriteLine("\n");

                    Thread.Sleep(1000*sleepTime); // Delay for "sleepTime" second(s)
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
