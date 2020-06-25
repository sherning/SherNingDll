using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    /// <summary>
    /// Learning BIG O notation.
    /// </summary>
    public class BigONotation
    {
        public static void Main()
        {
            TestAddingMethod();
        }

        #region Notes
        /* Big O Notation
         * - is a way of counting how the run time of an algorithm
         * grows as the inputs grow.
         * 
         * - BigO could be Linear, quadratic, contant, something entirely different.
         * 
         * 
         */
        #endregion
        #region Testing Methods
        private static void TestAddingMethod()
        {
            // Time is not the best method to determine which is better.
            // As different computer will have different outcomes
            // Sometimes same computer will have different results too.
            // Therefore, it is the number of steps a computer take to run will be a better
            // determinant to which is a better algorithm.

            Stopwatch timer = new Stopwatch();
            Stopwatch timer2 = new Stopwatch();

            timer.Start();
            AddMethodOne(1_000_000_000);
            timer.Stop();

            timer2.Start();
            AddMethodTwo(1_000_000_000);
            timer2.Stop();

            // V Slow
            Console.WriteLine(timer.ElapsedTicks / (float)Stopwatch.Frequency + " Sec");
            // Super Fast
            Console.WriteLine(timer2.ElapsedTicks / (float)Stopwatch.Frequency + " Sec");

        }
        #endregion

        #region Helper Methods
        private static int AddMethodOne(int n)
        {
            int total = 0;

            for (int i = 0; i < n; i++)
            {
                total += n;
            }

            return total;
        }

        private static int AddMethodTwo(int n)
        {
            return n * (n + 1) / 2;
        }
        #endregion

    }
}
