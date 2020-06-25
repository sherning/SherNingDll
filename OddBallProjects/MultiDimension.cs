using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OddBallProjects
{
    class MultiDimension
    {
        public static void Main()
        {
            // Declaration and Initialization of  
            // Jagged array with 4 2-D arrays 
            int[][,] jagged_arr1 = new int[4][,] {new int[, ] {{1, 3}, {5, 7}},
                                    new int[, ] {{0, 2}, {4, 6}, {8, 10}},
                                    new int[, ] {{7, 8}, {3, 1}, {0, 6}},
                                    new int[, ] {{11, 22}, {99, 88}, {0, 9}}};

            // Display the array elements: 
            // Length method returns the number of 
            // arrays contained in the jagged array 
            for (int i = 0; i < jagged_arr1.Length; i++)
            {

                int x = 0;

                // GetLength method takes integer x which  
                // specifies the dimension of the array 
                for (int j = 0; j < jagged_arr1[i].GetLength(x); j++)
                {

                    // Rank is used to determine the total  
                    // dimensions of an array  
                    for (int k = 0; k < jagged_arr1[j].Rank; k++)
                        Console.Write("Jagged_Array[" + i + "][" + j + ", " + k + "]: "
                                                    + jagged_arr1[i][j, k] + " ");
                    Console.WriteLine();
                }
                x++;
                Console.WriteLine();
            }
        }
    }
}
