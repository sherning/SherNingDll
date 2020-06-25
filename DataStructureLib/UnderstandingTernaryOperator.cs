using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    public class UnderstandingTernaryOperator
    {
        public static void Main()
        {
            ConditionalOperator();
            ConditonalRefExpression();
            ConditionalIfElse();
        }

        private static void ConditionalOperator()
        {
            // Local method, no need for access modifiers
            // think of it this way: 
            // is this condition true ? yes : no
            double sinc(double x) => x != 0.0 ? Math.Sin(x) / x : 1;

            Console.WriteLine("Conditional Operator Output: ");
            Console.WriteLine("Sinc(0.1) : " + sinc(0.1));
            Console.WriteLine("Sinc(0.0) : " + sinc(0.0));
        }

        private static void ConditonalRefExpression()
        {
            // Can assign reference to a ref local, ref readonly local
            // ref return value or ref method parameter

            var smallArray = new int[] { 1, 2, 3, 4, 5 };
            var largeArray = new int[] { 10, 20, 30, 40, 50 };

            //int refValue = 0;
            int index = 7;

            ref int refValue = ref ((index < 5) ? ref smallArray[index] : ref largeArray[index - 5]);
            Console.WriteLine("Reference value : " + refValue);

            // ref return value
            index = 2;
            ((index < 5) ? ref smallArray[index] : ref largeArray[index - 5]) = 100;

            // Another alternative to foreach => console.write
            Console.WriteLine(string.Join(" ", smallArray));
            Console.WriteLine(string.Join(" ", largeArray));
        }

        private static void ConditionalIfElse()
        {
            int input = new Random().Next(-5, 5);

            string classify;

            // Conditional if-else statement
            classify = (input >= 0) ? "Positive value : " + input : "Negative value : " + input;
            Console.WriteLine(classify);
        }
    }
}
