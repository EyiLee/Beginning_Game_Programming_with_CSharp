using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Program
    {
        /// <summary>
        /// Calculations
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {
            // Problem 1: Basic Calculations
            float OriginFahrenheit;
            Console.Write("Enter temperature (Fahrenheit): ");
            OriginFahrenheit = float.Parse(Console.ReadLine());

            float Celsius = (OriginFahrenheit - 32) * 5 / 9;
            Console.WriteLine("{0} degrees Fahrenheit is {1} degrees Celsius", OriginFahrenheit, Celsius);

            float Fahrenheit = Celsius * 9 / 5 + 32;
            Console.WriteLine("{0} degrees Celsius  is {1} degrees Fahrenheit", Celsius, Fahrenheit);
        }
    }
}
