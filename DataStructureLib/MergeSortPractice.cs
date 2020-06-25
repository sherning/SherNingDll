using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    class MergeSortPractice
    {
        public static void Main()
        {
            int[] numArr = { 5, 4, 3, 2, 1 };
            Console.WriteLine(string.Join(", ", numArr));
            Console.WriteLine();
            MergeSort(numArr);
            Console.WriteLine(string.Join(", ", numArr));
        }
     
        private static void MergeSort(params int[] numArr)
        {
            MergeSort(numArr, 0, numArr.Length - 1);
        }

        private static void MergeSort(int[] numArr, int left, int right)
        {
            if (left < right)
            {
                // Find the middle index of the array.
                // Return index will be floored automatically.
                int middle = (left + right) / 2;
                MergeSort(numArr, left, middle);
                MergeSort(numArr, middle + 1, right);
                Merge(numArr, left, middle, right);
            }
        }

        private static void Merge(int[] numArr, int left, int middle, int right)
        {
            // Calcuate the length required for each temp array.
            // Do note that indices are zero index
            int leftLen = middle - left + 1;
            int rightLen = right - middle;

            // Create temp arrays
            int[] L = new int[leftLen];
            int[] R = new int[rightLen];

            // Fill in temp ararys
            for (int i = 0; i < leftLen; i++)
            {
                L[i] = numArr[left + i];
            }

            for (int i = 0; i < rightLen; i++)
            {
                R[i] = numArr[middle + 1 + i];
            }

            // Pointer to numArr
            int k = 0;

            // Pointer to L[]
            int l = 0;

            // Pointer to R[]
            int r = 0;

            // If either left or right pointer reaches to the end
            // of its respective array, exit while loop.
            while (l < leftLen && r < rightLen)
            {
                if (L[l] <= R[r])
                {
                    numArr[left + k] = L[l];
                    l++;
                }
                else
                {
                    numArr[left + k] = R[r];
                    r++;
                }

                k++;
            }

            while (r < rightLen)
            {
                numArr[left + k] = R[r];
                r++;
                k++;
            }

            while (l < leftLen)
            {
                numArr[left + k] = L[l];
                l++;
                k++;
            }
        }
    }
}
