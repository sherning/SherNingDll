using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    static class MultiDimensionArray
    {
        public static void Main()
        {
            int[][] matrix =
            {
                //--------- row ---------//
                new int[]{10,20,30,40},   //c
                new int[]{15,25,35,45},   //o    
                new int[]{27,29,37,48},   //l
                new int[]{32,33,39,51},   //s
            };

            matrix.Search(32);
            matrix.Search(53);
        }

        /// <summary>
        /// Only works for a SORTED matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="x"></param>
        public static void Search(this int[][] matrix, int x)
        {
            // Pointer to row
            int row = 0;

            // Pointer to column
            int col = matrix.Length - 1;

            while (row < matrix.Length && col >= 0)
            {
                if (matrix[row][col] == x)
                {
                    Console.WriteLine($"{x} is found at: [{row},{col}]");
                    return;
                }

                if (matrix[row][col] > x)
                {
                    // One step left
                    col--;
                }
                else
                {
                    // One step right
                    row++;
                }
            }

            Console.WriteLine($"{x} is not found in the matrix.");
        }
    }
}
