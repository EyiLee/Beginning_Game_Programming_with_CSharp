using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    /// <summary>
    /// A class to convert Fahrenheit to Celsius and back again
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method to convert Fahrenheit to Celsius and back again
        /// </summary>
        /// <param name="args">arguments of command line</param>
        static void Main(string[] args) {
            // Problem 1: Basic Calculations
            Console.Write("Enter temperature (Fahrenheit): ");

            // get the fahrenheit from input
            float originFahrenheit;
            originFahrenheit = float.Parse(Console.ReadLine());

            // convert fahrenheit to celsius
            float celsius = (originFahrenheit - 32) * 5 / 9;
            Console.WriteLine("{0} degrees Fahrenheit is {1} degrees Celsius", originFahrenheit, celsius);

            // convert celsius to fahrenheit
            float fahrenheit = celsius * 9 / 5 + 32;
            Console.WriteLine("{0} degrees Celsius  is {1} degrees Fahrenheit", celsius, fahrenheit);
        }
    }
}
