using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OddBallProjects
{
    public class RefKeyword
    {
        public static void Main()
        {
            // you can have a empty constructor for struct.
            ComplexS cs = new ComplexS();
            cs.Imaginary = 4;
            cs.Real = 2;
            Console.WriteLine("Complex number CS = " + cs.Real + " + i" + cs.Imaginary + "\n");
            Console.WriteLine("Updating CS: " );

            // Similar to console.writeline, you need to put the struct name in front of the method.
            ComplexS.Update(cs);
            Console.WriteLine("Complex number CS = " + cs.Real + " + i" + cs.Imaginary);

            // with ref keyword for value types, then cs will be updated.
            ComplexS.UpdateRef(ref cs);

            Console.WriteLine("\nUpdating with ref operator in method: ");
            // With override to string
            Console.WriteLine("Complex number CS = " + cs);


            // But for cc, you need to fill in the constructor, unless you define a 
            // parameterless constructor in the class.
            ComplexC cc = new ComplexC(2,4);
            Console.WriteLine("\nComplex CC: \n" + cc);

            // Return new object instead of updating it
            ComplexC.ReturnNewObj(cc);
            Console.WriteLine("\nComplex CC: \n" + cc);
            //Console.WriteLine("\nComplex tempCC: \n" + tempCC);

            // got to be explicit. As it is not inside the class itself.
            ComplexC.Update(cc);
            Console.WriteLine("\nUpdate Complex CC: ");

            // Since CC is a reference type, ref keyword will not be necessary, and the values will be updated.
            Console.WriteLine(cc);
        }

        // Understanding the difference struct and class vs ref
        private struct ComplexS
        {
            public int Real { get; set; }
            public int Imaginary { get; set; }

            public ComplexS(int real, int imaginary)
            {
                this.Real = real;
                this.Imaginary = imaginary;
            }

            // Can struct have ref ?
            public static void Update(ComplexS obj)
            {
                obj.Real += 5;
                obj.Imaginary += 5;
            }

            public static void UpdateRef(ref ComplexS obj)
            {
                obj.Real += 5;
                obj.Imaginary += 5;
            }

            public override string ToString()
            {
                return "Complex number CS = " + Real + " + i" + Imaginary;
            }
        }

        /// <summary>
        /// Only RefKeyword class has access to ComplexC class.
        /// and can access its public methods but only within RefKeyword Class.
        /// </summary>
        private class ComplexC
        {
            public int Real { get; set; }
            public int Imaginary { get; set; }

            public ComplexC(int real, int imaginary)
            {
                this.Real = real;
                this.Imaginary = imaginary;
            }

            // Can struct have ref ?
            public static void Update(ComplexC obj)
            {
                obj.Real += 5;
                obj.Imaginary += 5;
            }

            public static void ReturnNewObj(ComplexC obj)
            {
                // no difference, because you are creating a new pointer to a new object.
                // so original object is not affected.
                obj = new ComplexC(10, 10);
            }
            public override string ToString()
            {
                return "Complex number CC = " + Real + " + i" + Imaginary;
            }
        }
    }
}
