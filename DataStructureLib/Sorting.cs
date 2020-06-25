using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    internal static class Sorting
    {
        public static void Main()
        {
            //while (true)
            //{
            //    //BubbleSort_Test();
            //    //SelectionSort_Test();
            //    //InsertionSort_Test();
            //    

            //    Console.WriteLine("Try Again? Y / N");
            //    if (Console.ReadLine().ToLower() == "n")
            //    {
            //        break;
            //    }
            //}

            //MergeSortTest();
            //QuickSortTest();
            //InsertionSort_Test();
            RadixSortTest();
            CountSortTest();
        }

        #region Count Sort
        // Implement counting sort algorithm
        private static void CountSortTest()
        {
            int[] num0to9 = new int[] { 1, 3, 7, 3, 2, 4, 5 };
            Console.WriteLine("\nBefore Count Sort");
            Console.WriteLine("[ " + string.Join(", ", num0to9) + " ]");
            num0to9.CountSort();
            Console.WriteLine("\nAfter Count Sort");
            Console.WriteLine("[ " + string.Join(", ", num0to9) + " ]");
        }
        private static void CountSort(this int[] numArr)
        {
            numArr.CountSort(1);
        }
        private static void CountSort(this int[] numArr, int exp)
        {
            // Define output array.
            int[] ret = new int[numArr.Length];

            // Define count array. 
            // There are only 10 digits.
            int[] count = new int[10];

            // Initialize all elements of count to 0
            for (int i = 0; i < count.Length; i++)
            {
                count[i] = 0;
            }

            // Add the first 
            for (int i = 0; i < numArr.Length; i++)
            {
                // Define an exponent of base 10
                // 10^0 = 1, for the first case exp = 1
                count[(numArr[i] / exp) % 10]++;
            }

            Console.WriteLine("Moving the appropriate numbers into the correct count box.");
            Console.WriteLine("[ " + string.Join(", ", count) + " ]");

            // Count got to start from 1, otherwise, 
            // an index out of range exception will occur.
            // Add the sum of all previous values
            for (int i = 1; i < count.Length; i++)
            {
                count[i] = count[i] + count[i - 1];
            }

            Console.WriteLine("Check the sum of adjacent count box");
            Console.WriteLine("[ " + string.Join(", ", count) + " ]");

            // Put back the numbers in order.
            // First in first out.
            // To make it stable, must operate in reverse order ??
            // Consider 115,804 and 385 in this order.
            // do in reverse will place 385 first from the back of the array.
            // then count-- will place 115 before 385 which will preserve the order.
            // otherwise, if 115 is placed first, the order will be 385, then 115. 
            // which will not be stable.
            // for single digit or non-recurring patterns, it will not affect.
            for (int i = numArr.Length - 1; i >= 0; i--)
            {
                // Why the need to minus 1 ?
                // Because Array are Zero index, so you need to shift count back by one.
                ret[count[(numArr[i] / exp) % 10] - 1] = numArr[i];

                // reduce the count value inside of count for every repeating num
                count[(numArr[i] / exp) % 10]--;
            }

            Console.WriteLine("Putting the values into the ret[]");
            Console.WriteLine("[ " + string.Join(", ", ret) + " ]");

            // Transfer the return value back to the original array.
            // In this case the original array is returned.
            for (int i = 0; i < ret.Length; i++)
            {
                numArr[i] = ret[i];
            }

        }
        #endregion

        #region Radix Sort
        private static void RadixSortTest()
        {
            List<int> listOfRandomNumbers = new List<int>();
            Random random = new Random();

            int numberOfNums = 0;

            // Create 20 random unique numbers
            while (numberOfNums < 5)
            {
                int randomNum = random.Next(1, 1000);

                // if list does not contain number, add to the list
                if(!listOfRandomNumbers.Contains(randomNum))
                {
                    listOfRandomNumbers.Add(randomNum);
                    numberOfNums++;
                }
            }

            int[] numArr = listOfRandomNumbers.ToArray();

            Console.WriteLine("Before radix sort: ");
            Console.WriteLine("[ " + string.Join(", ", numArr) + " ]");
            Console.WriteLine(MostDigits(numArr));
            numArr.RadixSort();
            Console.WriteLine("After radix sort: ");
            Console.WriteLine("[ " + string.Join(", ", numArr) + " ]"); 
        }

        private static void RadixSort(this int[] numArr)
        {
            // Get the maximum number of digits.
            //int m = MostDigits(numArr);
            //int mx = m == 1 ? 1 : (int)Math.Pow(10, m - 1);

            int mx = numArr[0];

            for (int i = 0; i < numArr.Length; i++)
            {
                if (numArr[i] > mx)
                {
                    mx = numArr[i];
                }
            }

            Console.WriteLine("MX: " + mx);

            // Exponent starts with 10^0 = 1
            // To end the for loop ...
            for (int exp = 1; mx / exp > 0; exp *= 10)
            {
                numArr.CountSort(exp);
            }
        }

        private static int GetDigits(this int num, int index)
        {
            // Define base case.
            if (index == 0 || num == 0)
            {
                return num % 10;
            }

            // Get remainder
            int remainder = num / 10;

            return remainder.GetDigits(--index);
        }

        private static int GetDigit(this int num, int index)
        {
            return (Math.Abs(num) / (int)Math.Pow(10, index)) % 10;
        }

        private static int DigitCount(int num)
        {
            // Need this special case, otherwise Log10(0) will return infinity
            if (num == 0)
            {
                return 1;
            }

            return ((int)Math.Log10(Math.Abs(num))) + 1;
        }

        private static int MostDigits(int[] numArr)
        {
            int count = 0;

            for (int i = 0; i < numArr.Length; i++)
            {
                count = Math.Max(count, DigitCount(numArr[i]));
            }

            return count;
        }

        #endregion

        #region Quick Sort
        private static void QuickSortTest()
        {
            Console.WriteLine("Before QuickSort: ");
            int[] numArr = new int[] { 5, 2, 1, 8, 4, 7, 6, 3 };
            Console.WriteLine("[ " + string.Join(", ", numArr) + " ]\n");

            Console.WriteLine("After QuickSort: ");
            numArr.QuickSort();
            Console.WriteLine("[ " + string.Join(", ",numArr) + " ]\n");
            //Console.WriteLine("\nPivot Helper index: " + PivotHelper(0, numArr.Length, numArr));
        }
        private static void QuickSort(this int[] numArr)
        {
            QuickSort(numArr, 0, numArr.Length);
        }

        private static void QuickSort(int[] numArr, int start, int end)
        {
            if (start < end)
            {
                int pivotIndex = Pivot(numArr, start, end);
                QuickSort(numArr, start, pivotIndex - 1);
                QuickSort(numArr, pivotIndex + 1, end);
            }
        }

        private static int Pivot(int[] numArr, int start, int end)
        {
            // Track the position of the pivot
            int pivotIndex = start;

            // Pivot value
            int pivotValue = numArr[start];

            // Start + 1, no need to evaluate the pivot.
            for (int i = start + 1; i < end; i++)
            {
                if (numArr[i] <= pivotValue)
                {
                    // increase the position of where the pivot is supposed to be.
                    pivotIndex++;

                    // if current is smaller than pivot, swap in situ.
                    // else, swap the larger value with smaller value.
                    numArr.Swap(i, pivotIndex);
                }
            }

            // Swap the pivot into the correct position.
            numArr.Swap(start, pivotIndex);

           // Console.WriteLine(string.Join(", ",numArr));

            return pivotIndex;
        }

        // starts by using the end. where as pivot starts by using start.
        private static int Partition(int[] numArr, int start, int end)
        {
            int p = numArr[end];
            int positionIndex = start - 1;

            for (int i = start; i < end; i++)
            {
                if (numArr[i] <= p)
                {
                    positionIndex++;

                    // Swap values
                    numArr.Swap(positionIndex, i);
                }
            }

            numArr.Swap(positionIndex + 1, end);
            Console.WriteLine("[ " + string.Join(", ", numArr) + " ]\n");

            return positionIndex + 1;
        }

        

        // My first try.
        private static int PivotHelper(int start, int end, params int[] numArr)
        {
            // To keep track of where the pivot should be.
            int pivotPosition = 0;

            // Create a variable to store pivot position value
            int pivotValue = numArr[start];

            // Track if previous value is less than pivot.
            bool PivotFlag = true;
           
            // Since pivotPosition is 0, the first element to loop is 1
            for (int i = start + 1; i < end; i++)
            {
                // if current is less or equal to pivot
                if (pivotValue >= numArr[i] && PivotFlag == true)
                {
                    // add to current pivot position
                    pivotPosition++;
                    continue;
                }

                // if current value is less or equal to pivot and previous value is larger than pivot
                if (pivotValue >= numArr[i] && PivotFlag == false)
                {
                    // swap the value.
                    int temp = numArr[i];
                    numArr[i] = numArr[pivotPosition];
                    numArr[pivotPosition] = temp;

                    // Add to pivot position
                    pivotPosition++;

                    // Reset pivot flag to true
                    PivotFlag = true;
                    continue;
                }

                // if pivot position is larger than current
                // and current is larger than previous value which is less than pivot value
                if (   pivotValue < numArr[i] 
                    && numArr[i] > numArr[i-1] 
                    && numArr[i-1] <= pivotValue)
                {
                    PivotFlag = false;
                    continue;
                }

                // pivot value is larger than current and previous value is not a value
                // that is smaller than pivot. so flag to false.
                if (pivotValue < numArr[i])
                {
                    PivotFlag = false;
                    continue;
                }
            }

            int temp1 = numArr[pivotPosition];
            numArr[pivotPosition] = numArr[start];
            numArr[start] = temp1;

            Console.WriteLine(string.Join(", ",numArr));

            return pivotPosition;
        }
        #endregion

        #region Merge Sort
        private static void MergeSortTest()
        {
            // Generate a random list of UNIQUE numbers.
            Random random = new Random();
            List<int> numList = new List<int>();
            int x = 0;
            while (x < 20)
            {
                int temp = random.Next(0, 100);
                if (numList.Contains(temp) == false)
                {
                    numList.Add(temp);
                    x++;
                }
            }

            // convert list to Array.
            int[] numArr = numList.ToArray();

            Console.WriteLine("\nMerge Sort");
            Console.WriteLine("Array before sorting : " + string.Join(" ", numArr));
            
            // Why do you need a temp array to store the return value of Merge Sort ??
            int[] tempArr = numArr.MergeSorting();
            Console.WriteLine("Array after sorting  : " + string.Join(" ", tempArr));
        }
        
        /// <summary>
        /// Uses recursion.
        /// </summary>
        /// <param name="numArr"></param>
        /// <returns></returns>
        private static int[] MergeSorting(this int[] numArr)
        {
            if (numArr.Length <= 1)
            {
                return numArr;
            }

            int mid = (int)Math.Floor(numArr.Length / 2.0);
            int[] left = MergeSorting(numArr.Slice(0, mid));
            int[] right = MergeSorting(numArr.Slice(mid));

            return MergingSortedArray(left, right);
        }
       
        /// <summary>
        /// Merge two SORTED arrays together.
        /// </summary>
        /// <param name="numArrA"></param>
        /// <param name="numArrB"></param>
        /// <returns></returns>
        private static int[] MergingSortedArray(int[] numArrA, params int[] numArrB)
        {
            // Create an empty array & set array length to the combined lengths
            int[] ret = new int[numArrA.Length + numArrB.Length];

            // Pointers
            // a for Array A
            int a = 0;

            // b for Array B
            int b = 0;

            // x for return Array
            int x = 0;

            // Loop until either Array A or B reaches it Array Limit.
            while (a < numArrA.Length && b < numArrB.Length)
            {
                if (numArrA[a] < numArrB[b])
                {
                    // push array A value to ret[] and increment a and x.
                    ret[x++] = numArrA[a++];
                }
                else
                {
                    ret[x++] = numArrB[b++];
                }
            }

            // Array B reached limit, add remaining of A to ret[]
            while (a < numArrA.Length)
            {
                ret[x++] = numArrA[a++];
            }

            // Array A reached limit, add remaining of B to ret[]
            while (b < numArrB.Length)
            {
                ret[x++] = numArrB[b++];
            }

            return ret;
        }
        #endregion

        #region Insertion Sort
        private static void InsertionSort_Test()
        {
            // Generate a random list of UNIQUE numbers.
            Random random = new Random();
            List<int> numList = new List<int>();
            int x = 0;
            while (x < 10)
            {
                int temp = random.Next(0, 99);
                if (numList.Contains(temp) == false)
                {
                    numList.Add(temp);
                    x++;
                }
            }

            // convert list to Array.
            int[] numArr = numList.ToArray();

            Console.WriteLine("\nInsertion Sort");
            Console.WriteLine("Before Insertion Sort: \n" + "[ " + string.Join(", ", numArr) + " ]\n");

            numArr.InsertionSort();
            Console.WriteLine("After Insertion Sort: \n" + "[ " + string.Join(", ", numArr) + " ]\n");

        }
        private static int[] InsertionSort(this int[] numArr)
        {
            int steps = 0;

            for (int i = 1; i < numArr.Length; i++)
            {
                // Scan throught the sorted part of the array, 
                // and insert num into correctly sorted position
                for (int j = 0; j < i + 1; j++)
                {
                    if (numArr[j] > numArr[i])
                    {
                        // insert numArr[i] 
                        int temp = numArr[i];
                        numArr[i] = numArr[j];
                        numArr[j] = temp;
                    }

                    steps++;
                }
            }

            Console.WriteLine("Number of steps taken : " + steps);
            return numArr;
        }
        #endregion

        #region Selection Sort
        private static void SelectionSort_Test()
        {
            Random random = new Random();
            List<int> numList = new List<int>();
            int x = 0;
            while (x < 20)
            {
                int temp = random.Next(0, 100);
                if (numList.Contains(temp) == false)
                {
                    numList.Add(temp);
                    x++;
                }
            }

            int[] numArr = numList.ToArray();

            Console.WriteLine("\nSelection Sort");
            Console.WriteLine("Array before sorting : " + string.Join(" ", numArr));
            numArr.SelectionSort();
            Console.WriteLine("Array after sorting : " + string.Join(" ", numArr));
        }
        private static int[] SelectionSort(this int[] numArr)
        {
            int steps = 0;
            for (int i = 0; i < numArr.Length; i++)
            {
                // The first element of the array.
                int lowest = i;

                for (int j = i + 1; j < numArr.Length; j++)
                {
                    // the first value of the array.
                    if (numArr[j] < numArr[lowest])
                    {
                        // a swap should take place here.
                        lowest = j;
                    }

                    steps++;
                }

                // this is as good as saying swap is true.
                if (i != lowest)
                {
                    int temp = numArr[i];
                    numArr[i] = numArr[lowest];
                    numArr[lowest] = temp;
                }
            }

            Console.WriteLine("Number of steps taken : " + steps);
            return numArr;
        }
        #endregion

        #region Bubble Sort
        private static void BubbleSort_Test()
        {
            //int[] numArr = new int[] { 8, 9, 1, 2, 3, 14, 19, 20, 31 };
            //Console.WriteLine("Array before sorting : " + string.Join(" ", numArr));
            //Console.WriteLine("\nBubble Sort Normal.");
            //numArr.BubbleSort_Normal();
            //Console.WriteLine("Array after sorting : " + string.Join(" ", numArr));

            //Console.WriteLine("\nBubble Sort Better.");
            //int[] numArr2 = new int[] { 8, 9, 1, 2, 3, 14, 19, 20, 31 };
            //numArr2.BubbleSort_Better();
            //Console.WriteLine("Array after sorting : " + string.Join(" ", numArr2));

            Random random = new Random();
            List<int> numList = new List<int>();
            int x = 0;
            while (x < 20)
            {
                int temp = random.Next(0, 100);
                if (numList.Contains(temp) == false)
                {
                    numList.Add(temp);
                    x++;
                }
            }

            int[] numArr3 = numList.ToArray();

            Console.WriteLine("\nBubbleSort Best");
            Console.WriteLine("Array before sorting : " + string.Join(" ", numArr3));
            numArr3.BubbleSort_Best();
            Console.WriteLine("Array after sorting : " + string.Join(" ", numArr3));
        }

        private static int[] BubbleSort_Normal(this int[] numArr)
        {
            int steps = 0;

            for (int i = 0; i < numArr.Length - 1; i++)
            {
                for (int j = 0; j < numArr.Length - 1; j++)
                {
                    int temp = 0;

                    // Swap method.
                    if (numArr[j] > numArr[j + 1])
                    {
                        temp = numArr[j];
                        numArr[j] = numArr[j + 1];
                        numArr[j + 1] = temp;
                    }

                    steps++;
                }
            }

            Console.WriteLine("Number of steps taken : " + steps);
            // reference type, you make changes to original copy
            return numArr;
        }

        private static int[] BubbleSort_Better(this int[] numArr)
        {
            int steps = 0;

            // The last element is always sorted, for next past ignore last element.
            for (int i = numArr.Length - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    // Swap method.
                    if (numArr[j] > numArr[j + 1])
                    {
                        int temp = numArr[j];
                        numArr[j] = numArr[j + 1];
                        numArr[j + 1] = temp;
                    }

                    steps++;
                }
            }

            Console.WriteLine("Number of steps taken : " + steps);
            // reference type, you make changes to original copy
            return numArr;
        }

        private static int[] BubbleSort_Best(this int[] numArr)
        {
            int steps = 0;

            // The last element is always sorted, for next past ignore last element.
            for (int i = numArr.Length - 1; i > 0; i--)
            {
                bool swap = false;
                for (int j = 0; j < i; j++)
                {
                    // Swap method.
                    if (numArr[j] > numArr[j + 1])
                    {
                        int temp = numArr[j];
                        numArr[j] = numArr[j + 1];
                        numArr[j + 1] = temp;
                        swap = true;
                    }

                    steps++;
                }

                // If there are no swaps, array is sorted. 
                if (swap == false)
                {
                    break;
                }
            }

            Console.WriteLine("Number of steps taken : " + steps);
            // reference type, you make changes to original copy
            return numArr;
        }

        #endregion

        #region Helper Methods
        private static void Swap(this int[] numArr, int from, int to)
        {
            int temp = numArr[from];
            numArr[from] = numArr[to];
            numArr[to] = temp;
        }
        #endregion
    }
}
