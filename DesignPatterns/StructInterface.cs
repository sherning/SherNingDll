using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class StructInterface
    {
        public static void Main()
        {
            
            Point point = new Point(5, 3);
            point.Display();

            // calling struct implicit default constructor.
            Points points = new Points();
            points.Display();
        }
        struct Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public void Display()
            {
                Console.WriteLine($"X = {X}, Y = {Y}");
            }
        }
        struct Points
        {
            public int X { get; set; }
            public int Y { get; set; }

            public void Display()
            {
                Console.WriteLine($"X = {X}, Y = {Y}");
            }
        }
    }
}
