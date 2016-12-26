using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment1
{
    /// <summary>
    /// Program to calculate the distance and angle between two points
    /// using Pythagorean Theorem.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Get the x and y values for two points A and B
        /// </summary>
        /// <param name="args">value x and y args</param>
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome, our first guest!");
            Console.WriteLine("This program will calculate the distance and angle between" +
                              " two points using Pythagorean Theorem.");

            Console.Write("Enter x value for point A: ");
            float pointAValueX = float.Parse(Console.ReadLine());

            Console.Write("Enter y value for point A: ");
            float pointAValueY = float.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine("Great job!");
            Console.Write("Now enter x value for point B: ");
            float pointBValueX = float.Parse(Console.ReadLine());

            Console.Write("Enter y value for point B: ");
            float pointBValueY = float.Parse(Console.ReadLine());

            // Calculate the distance between x values of points A and B
            float distanceX = pointBValueX - pointAValueX;

            // Calculate the distance between y values of points A and B
            float distanceY = pointBValueY - pointAValueY;

            // Calculate the distance between points A and B
            double squareDistance = Math.Pow(distanceX, 2) + Math.Pow(distanceY, 2);
            double distance = Math.Sqrt(squareDistance);

            // Calculate the angle between points A and B
            double alphaInDegrees = Math.Atan2(distanceY, (double)distanceX) * 180 / Math.PI;
            double alpha = Math.Atan((double)distanceY / distanceX) * 180 / Math.PI;
            double degrees = (Math.Floor(alpha));
            double minutes = (alpha - Math.Floor(alpha)) * 60.0;
            double seconds = (minutes - Math.Floor(minutes)) * 60.0;

            // Get rid of fractional part
            minutes = Math.Floor(minutes);
            seconds = Math.Floor(seconds);

            Console.WriteLine();
            Console.WriteLine("Distance between points: " + distance.ToString("F3",
                  CultureInfo.InvariantCulture) + " points");
            Console.WriteLine("Angle between points: " + alphaInDegrees.ToString("F3",
                  CultureInfo.InvariantCulture) + " ("
                                        + degrees + "°" 
                                        + minutes + "´"
                                        + seconds + "´´)"
                             );
            Console.WriteLine();

        }
    }
}
