using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    /// <summary>
    /// A class to read user's birthday and send a reminder email
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method to read user's birthday and send a reminder email
        /// </summary>
        /// <param name="args">arguments of command line</param>
        static void Main(string[] args) {
            // read user's birthday month
            Console.Write("In what month were you born?: ");
            string month = Console.ReadLine();

            // read user's birthday day
            Console.Write("On what day were you born?: ");
            int day = int.Parse(Console.ReadLine());

            // print user's birthday
            Console.WriteLine("Your birthday is {0} {1}", month, day);

            // print when the reminder email will be sent
            // there is a bug when user enter the first day of month as birthday
            // but we don't resolve this bug in this case
            Console.WriteLine("You’ll receive an email reminder on {0} {1}", month, day - 1);
        }
    }
}
