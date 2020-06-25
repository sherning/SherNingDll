using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    static class QuickSortApplication
    {
        public static void Main()
        {
            // Generate a random list of UNIQUE numbers.
            Random random = new Random();
            List<int> numList = new List<int>();
            int x = 0;
            while (x < 7)
            {
                int temp = random.Next(1, 43);
                if (numList.Contains(temp) == false)
                {
                    numList.Add(temp);
                    x++;
                }
            }

            // convert list to Array.
            int[] numArr = numList.ToArray();

            Console.WriteLine("NumArr before sorting");
            numArr.PrintArray();
            Console.WriteLine("NumArr after sorting");
            numArr.QuickSort();
            numArr.PrintArray();
        }

        private static void PrintArray(this int[] numArr)
        {
            Console.WriteLine("[ " + string.Join(", ", numArr) + " ]");
        }

        private static void QuickSort(this int[] numArr)
        {
            QuickSort(numArr, 0, numArr.Length - 1);
        }

        private static void QuickSort(int[] numArr, int startIndex, int endIndex)
        {
            if (startIndex < endIndex)
            {
                int pivot = Partition(numArr, startIndex, endIndex);
                QuickSort(numArr, startIndex, pivot - 1);
                QuickSort(numArr, pivot + 1, endIndex);
            }
        }
        private static int Partition(int[] numArr, int startIndex, int endIndex)
        {
            // Set the pivot to the last element of numArr
            int pivot = numArr[endIndex];

            // Create a postion pointer to track the pivot.
            int pivotPosition = startIndex;

            // endIndex - 1, as the pivot is place there.
            for (int i = startIndex; i < endIndex; i++)
            {
                if (numArr[i] <= pivot)
                {
                    // swap
                    numArr.Swap(i, pivotPosition);
                    pivotPosition++;
                }
            }

            // swap pivot into correct position
            numArr.Swap(endIndex, pivotPosition);

            return pivotPosition;
        }

        private static void Swap(this int[] numArr, int a, int b)
        {
            int temp = numArr[a];
            numArr[a] = numArr[b];
            numArr[b] = temp;
        }
    }
}
