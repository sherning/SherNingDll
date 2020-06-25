using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OddBallProjects
{
    class ReferenceVsValue
    {
        /// <summary>
        /// Why values stored in variables get unwinded after the end of a method ?
        /// They are popped of the stack, at the end of the method, as compared to 
        /// members of the class which exist on the heap.
        /// Null is only for reference type you cant use it for value types.
        /// </summary>
        private class Fraction
        {
            // Static variable is non-dynamic
            // It is not located on either heap nor stack.
            // It is fixed memory at compile time created by the compiler.
            // Member variables default to default value by constructor.
            public static int NumInstances;
            public int Numerator { get; set; }
            public int Denominator { get; set; }

            /// <summary>
            /// Default Contructor
            /// </summary>
            public Fraction()
            {
                // Everytime the constructor is called, it will increment.
                ++NumInstances;
                Console.WriteLine("Calling default constructor");
            }
            public Fraction(int numerator, int denominator)
            {
                Numerator = numerator;
                Denominator = denominator;

                // Note that we have two constructors in this class.
                // But when instanitating, it will only call one.
                ++NumInstances;
                Console.WriteLine("Calling overload constructor");
            }
        }
        
        public static void Main()
        {
            // fract1 is the address to the memory on the heap, 
            // the new keyword creates a space of memory on the heap.
            Fraction fract1 = new Fraction(1,2);

            // Assign fract1 address to fract2 address
            Fraction fract2 = fract1;

            // fract1 has a new address and a new memory space, when the new keyword is used.
            fract1 = new Fraction(5, 5);

            // Assign fract1 new address to fract 2.
            fract2 = fract1;
            Console.WriteLine(fract2.Numerator);
            Console.WriteLine(Fraction.NumInstances);

        }
    }
}
