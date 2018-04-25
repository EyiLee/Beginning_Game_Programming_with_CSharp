using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    class Program
    {
        static void Main(string[] args)
        {
            // list of data
            List<int> data = new List<int>();

            // read number from user input
            int input = 0;
            while (input != -1)
            {
                input = int.Parse(Console.ReadLine());

                if (input != -1)
                {
                    // add user input into the list
                    data.Add(input);
                }
            }

            // find the maximum value in the list
            int maximum = 0;
            foreach (int num in data)
            {
                if (num > maximum)
                {
                    maximum = num;
                }
            }
            Console.WriteLine("Maximum value in the list: {0}", maximum);

            // calculate the average of the values in the list
            int sum = 0;
            foreach (int num in data)
            {
                sum += num;
            }
            Console.WriteLine("Average value of the list: {0}", sum / data.Count);

            Console.ReadLine();
        }
    }
}
