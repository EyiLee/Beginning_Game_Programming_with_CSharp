using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    /// <summary>
    /// A class to print the choice of user from menu
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method to print the choice of user from menu
        /// </summary>
        /// <param name="args">arguments of command line</param>
        static void Main(string[] args) {
            // print the menu
            Console.WriteLine("**************");
            Console.WriteLine("Menu:");
            Console.WriteLine("N – New Game");
            Console.WriteLine("L – Load Game");
            Console.WriteLine("O – Options");
            Console.WriteLine("Q – Quit");
            Console.WriteLine("**************");

            // read the choice of user
            Console.Write(": ");
            char command = Console.ReadKey().KeyChar;
            Console.WriteLine();

            // the statements of commands
            if (command == 'N') {
                Console.WriteLine("Create a new game!");
            } else if (command == 'L') {
                Console.WriteLine("Loading game...");
            } else if (command == 'O') {
                Console.WriteLine("Change the settings.");
            } else if (command == 'Q') {
                Console.WriteLine("Bye!");
            }

            // read the choice of user
            Console.Write(": ");
            command = Console.ReadKey().KeyChar;
            Console.WriteLine();

            // the statements of commands
            switch (command) {
                case 'N':
                    Console.WriteLine("Create a new game!");
                    break;
                case 'L':
                    Console.WriteLine("Loading game...");
                    break;
                case 'O':
                    Console.WriteLine("Change the settings.");
                    break;
                case 'Q':
                    Console.WriteLine("Bye!");
                    break;
                default:
                    Console.WriteLine("Unknown command!");
                    break;
            }
        }
    }
}
