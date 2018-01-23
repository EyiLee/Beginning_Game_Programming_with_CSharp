using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    /// <summary>
    /// A class to determine whether or not the block should be lit
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method to determine whether or not the block should be lit
        /// </summary>
        /// <param name="args">arguments of command line</param>
        static void Main(string[] args) {
            // read the information of the block
            Console.WriteLine("Enter the information of the block by the format below");
            Console.WriteLine("<pyramid slot number>,<block letter>,<whether or not the block should be lit>");
            Console.Write(":");
            string information = Console.ReadLine();

            // extract the pyramid slot number 
            int numberIndex = information.IndexOf(",");
            int number = int.Parse(information.Substring(0, numberIndex));

            // slice the information from original information
            information = information.Substring(numberIndex + 1);

            // extract the block letter
            int letterIndex = information.IndexOf(",");
            char letter = char.Parse(information.Substring(0, letterIndex));

            // slice the information from original information
            information = information.Substring(letterIndex + 1);

            // extract whether the block should be lit
            bool lit = bool.Parse(information);

            // print the information
            Console.WriteLine("{0}, {1}, {2}", number, letter, lit);
        }
    }
}
