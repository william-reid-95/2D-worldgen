// See https://aka.ms/new-console-template for more information

using System;
using MyUtilities;

namespace Tonbale // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            World world = new World(10, 10);
            world.SmoothWorld(1);

            world.PrintMap();

            bool running = true;
            string input;
            while(running)
            {
                input = GetInput();
                if (input == "quit")
                {
                    running = false;
                    break;
                }
                else if (input == "1")
                {
                    for (int i = 0; i < 100; i++)
                    {
                        Console.WriteLine($"{RandomGen.RandomBool()}");
                    }
                }

            }    
        }

        public static string GetInput()
        {
            Console.WriteLine("please enter a valid command:");
            string? input = null;
            while (input == null)
            {
                input = Console.ReadLine();
            }
            return input;
        }
    }
    

    
}