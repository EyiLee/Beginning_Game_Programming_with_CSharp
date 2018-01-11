using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    /// <summary>
    /// A class to calculate the percentage of score
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method to calculate the percentage of score
        /// </summary>
        /// <param name="args">arguments of command line</param>
        static void Main(string[] args) {
            // Problem 1: Declaring and Using Variables
            int age = 23;
            Console.WriteLine(age);

            // Problem 2: Declaring and Using Constants and Variables
            const int MaxScore = 100;
            int score = new Random().Next(0, 100);
            float percent = (float)score / MaxScore;
            Console.WriteLine(percent);
        }
    }
}
