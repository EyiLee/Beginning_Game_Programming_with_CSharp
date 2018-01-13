using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment1
{
    /// <summary>
    /// A class to calculate the distance between two points and the angle between those points
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method to calculate the distance between two points and the angle between those points
        /// </summary>
        /// <param name="args">arguments of command line</param>
        static void Main(string[] args) {
            Console.WriteLine("welcome");

            float point1X;
            float point1Y;

            // get the x of point1 from user
            Console.Write("Point 1 X: ");
            point1X = float.Parse(Console.ReadLine());

            // get the y of point1 from user
            Console.Write("Point 1 Y: ");
            point1Y = float.Parse(Console.ReadLine());

            float point2X;
            float point2Y;

            // get the x of point2 from user
            Console.Write("Point 2 X: ");
            point2X = float.Parse(Console.ReadLine());

            // get the y of point2 from user
            Console.Write("Point 2 Y: ");
            point2Y = float.Parse(Console.ReadLine());

            float deltaX = point2X - point1X;
            float deltaY = point2Y - point1Y;

            // calculate the distance between the two points
            double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            // calculate the angle we'd have to move in to go from point 1 to point 2
            double radians = Math.Atan2(deltaY, deltaX);

            // convert radians to degrees
            double angle = radians * (180 / Math.PI);

            // print the distance between two points and the angle between those points
            Console.WriteLine("distance: {0}", distance.ToString("f3"));
            Console.WriteLine("angle: {0} degrees", angle.ToString("f3"));
        }
    }
}
