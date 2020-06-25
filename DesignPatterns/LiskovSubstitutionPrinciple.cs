using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    /// <summary>
    /// You should be able to substitute a base type for a subtype.
    /// You can do so with virtual and override keywords.
    /// </summary>
    class LiskovSubstitutionPrinciple
    {
        private static int Area(Rectangle r) => r.Width * r.Height;
        public static void Main()
        {
            Rectangle rc = new Rectangle(3, 4);
            Console.WriteLine(rc);

            Rectangle sq = new Square();
            sq.Width = 4;
            Console.WriteLine($"{sq} has area {Area(sq)}");
        }

        /// <summary>
        /// Protected is private, but can be inherited.
        /// </summary>
        protected class Rectangle
        {
            /// <summary>
            /// Virtual Property to be overriden by derived class
            /// </summary>
            public virtual int Height { get; set; }
            public virtual int Width { get; set; }

            public Rectangle()
            {

            }
            public Rectangle(int height, int width)
            {
                Height = height;
                Width = width;
            }

            public override string ToString() 
                => $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }

        private class Square : Rectangle
        {
            public override int Height 
            { 
                set => base.Height = base.Width = value; 
            }

            public override int Width
            {
                set => base.Height = base.Width = value;
            }
        }
    }


}
