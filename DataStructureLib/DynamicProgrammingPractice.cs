using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    class DynamicProgrammingPractice
    {
        public static void Main()
        {

        }

        #region Drop Egg Problem

        private static void DropEggTest()
        {

        }

        private static int DropEgg(int eggs, int floors)
        {
            // no floors cannot test
            if (floors == 0 || floors == 1) return floors;

            // No eggs cannot test
            if (eggs == 0) return 0;

            // only one egg
            if (eggs == 1) return floors;

            int results = int.MaxValue;

            // start from first floor, converge them to base cases to get the answers.
            for (int i = 1; i <= floors; i++)
            {
                // do the remaining floors above the current floor i which it does not break.
                int eggSurvives = DropEgg(eggs, floors - i);

                // do the remaining floors below the currenbt floor i which it breaks.
                int eggBreaks = DropEgg(eggs - 1, i - 1);


                results = Math.Min(results, Math.Max(eggSurvives, eggBreaks) + 1);
            }

            return results;
        }
        #endregion
    }
}
