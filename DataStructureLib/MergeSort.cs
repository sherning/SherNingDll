using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    internal static class MergeSort
    {

        // it is legal to : new CountingClass() without a variable to store the refence to memory.
        // Basically, you created it and abandon it for garbage collection.
        // Note: stack is Last in, First out, in other words, things in the middle cannot be deleted, without LIFO
        public static void Main()
        {
            //int[] arr = { 12, 11, 13, 5, 6, 7 };
            int[] arr = { 5, 29, 1 };

            Console.WriteLine("Given Array: ");

            // WHy you can call PrintArray without the class prefix
            // PrintArray is a member of the class. 
            // So essentially you are calling the member of the class within the class.
            // 'This'
            PrintArray(arr);

            Sort(arr);

            Console.WriteLine("\nSorted Array");
            PrintArray(arr);

            // Test to determine if return type is affecting the array.
            // or the processes in the method itself.
            if (true)
            {
                arr.VoidTest();
                PrintArray(arr);

                // If there is no need to capture the value,
                // you do need a variable to capture the return value.
                arr = arr.retTest();
                PrintArray(arr);

                arr.retVoidTest();
                PrintArray(arr);
            }
            
        }
        // Void vs return type test on Array
        private static void VoidTest(this int[] numArr)
        {
            for (int i = 0; i < numArr.Length; i++)
            {
                numArr[i] = 1;
            }
        }

        private static int[] retTest(this int[] numArr)
        {
            int[] ret = new int[numArr.Length];

            for (int i = 0; i < numArr.Length; i++)
            {
                ret[i] = 2;
            }

            // This returns a new pointer to a new memory location.
            // Does not change the original array location.
            // But...

            numArr = ret;
            return numArr;
        }

        private static int[] retVoidTest(this int[] numArr)
        {
            // This also proves that ararys are reference types
            // AS the actual memory location is changed.
            for (int i = 0; i < numArr.Length; i++)
            {
                numArr[i] = 3;
            }

            return null;
        }

        private static void Sort(int[] arr)
        {
            Sort(arr, 0, arr.Length - 1);
        }

        // Understand how to use void in Recursion.
        // The original arr is passed into merge.
        private static void Merge(int[] arr, int l, int m, int r)
        {
            // Find sizes of two subarrays to be merged 
            int n1 = m - l + 1;
            int n2 = r - m;
 
            /* Create temp arrays */
            int[] L = new int[n1];
            int[] R = new int[n2];

            /*Copy data to temp arrays*/
            // Did not touch the original array.
            // Only reference it for COPYING.
            for (int i = 0; i < n1; ++i)
                L[i] = arr[l + i];

            for (int j = 0; j < n2; ++j)
                R[j] = arr[m + 1 + j];


            /* Merge the temp arrays */

            // Initial indexes of first and second subarrays 
            int x = 0, y = 0;

            // Initial index of merged sub array 
            int k = l;
            while (x < n1 && y < n2)
            {
                if (L[x] <= R[y])
                {
                    arr[k] = L[x];
                    x++;
                }
                else
                {
                    // Change the original array.
                    arr[k] = R[y];
                    y++;
                }
                k++;
            }

            /* Copy remaining elements of L[] if any */
            while (x < n1)
            {
                arr[k] = L[x];
                x++;
                k++;
            }

            /* Copy remaining elements of R[] if any */
            while (y < n2)
            {
                arr[k] = R[y];
                y++;
                k++;
            }
        }
        /// <summary>
        /// Without using slice, how do you use sort recursively ?
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="l">starting index</param>
        /// <param name="r">ending index, ending index cannot be > array length - 1</param>
        private static void Sort(int[] arr, int l, int r)
        {
            // Start index < end index.
            // for most cases will be start = 0, end = arr.Length
            if (l < r)
            {
                // Find the middle point 
                // Remainder for division will also be floored.
                // The combination of finding m and l < r is the key to recursion.
                int m = (l + r) / 2;

                // Sort first and second halves 
                // Sort will continue until m = 0 before sort(arr, m+ 1, r) is called.
                Sort(arr, l, m);
                Sort(arr, m + 1, r);

                // Merge the sorted halves 
                Merge(arr, l, m, r);
            }
        }

        private static void PrintArray(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
        }
    }

}
