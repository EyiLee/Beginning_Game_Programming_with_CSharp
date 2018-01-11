using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {
        /// <summary>
        /// Variables and Constants
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {
            // Problem 1: Declaring and Using Variables
            int Age = 23;
            Console.WriteLine(Age);

            // Problem 2: Declaring and Using Constants and Variables
            const int MaxScore = 100;
            int Score = new Random().Next(0, 100);
            float Percent = (float)Score / MaxScore;
            Console.WriteLine(Percent);
        }
    }
}
