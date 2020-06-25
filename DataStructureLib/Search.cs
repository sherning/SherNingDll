using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    /// <summary>
    /// Extension methods to searching algorithms
    /// </summary>
    public static class Search
    {
        public static void Main()
        {
            string[] dataSet = new string[] { "Alexis", "Johnny", "Peter", "Betsy", "Alan", "Samantha", "Claudia" };

            Console.WriteLine(dataSet.LinearSeach("Alexis"));
            Console.WriteLine(LinearSeach("Johnny", dataSet));

            Console.WriteLine("\nBinarySeach Test");
            Console.WriteLine("Dataset before sorting: \n" + string.Join(" ", dataSet) + "\n");
            Console.WriteLine("Dataset before sorting: \n" + string.Join(" ", dataSet.BubbleSort()) + "\n");
            Console.WriteLine("BinarySeach(\"Samantha\"), answer: " + BinarySearch("Samantha",dataSet));

            Console.WriteLine("\nRepeated String Patterns");
            Console.WriteLine(RepeatedPatternsInString("lorie loled haha lolololol lol lol","lol"));

        }

        private static int RepeatedPatternsInString(string str, string pattern)
        {
            int ret = 0;

            for (int i = 0; i < str.Length - 1; i++) 
            {
                // no need to worry about overflow, max of j < pattern.length
                for (int j = 0; j < pattern.Length; j++)
                {
                    if (pattern[j] != str[j + i])
                    {
                        // where it breaks to ? 
                        // answer: out of the inner for loop.
                        break;
                    }
                    
                    // Completed one pattern.
                    if (j == pattern.Length - 1)
                    {
                        ret++;
                    }
                }
            } // break to here.

            return ret;
        }

        /// <summary>
        /// Extension method for Binary Search.
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        private static int BinarySearch(this string[] arr, string search)
        {
            // Need to sort the array first. For binary sort to work.
            arr = arr.BubbleSort();

            // Local variables aren't initialized, members are. Therefore, must assign before use.
            int start = 0;
            int end = arr.Length - 1;
            int middle;

            while (start <= end)
            {
                // Always round down (floor) by default, 4.5 = 4.
                middle = (start + end) / 2;

                // If seach is found, return index == middle.
                if (String.Compare(search, arr[middle]) == 0)
                {
                    return middle;
                }

                if (String.Compare(arr[middle], search) < 0)
                {
                    // why must plus 1 ? Because the middle is redundant.
                    start = middle + 1;
                    continue;
                }

                // Index of middle is greater than index of search.
                if (String.Compare(arr[middle], search) > 0)
                {
                    // Because the middle number is redundant.
                    end = middle - 1;
                    continue;
                }
            }

            return -1;
        }
        /// <summary>
        /// Accepts a sorted array and a search value and returns the index of the value.
        /// If value is non-existent, return -1.
        /// </summary>
        /// <param name="search"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        private static int BinarySearch(string search, params string[] arr)
        {
            // Need to sort the array first. For binary sort to work.
            arr = arr.BubbleSort();

            // Local variables aren't initialized, members are. Therefore, must assign before use.
            int start = 0;
            int end = arr.Length - 1;
            int middle;

            while (start <= end)
            {
                // Always round down (floor) by default, 4.5 = 4.
                middle = (start + end) / 2;

                // If seach is found, return index == middle.
                if (String.Compare(search,arr[middle]) == 0)
                {
                    return middle;
                }

                if (String.Compare(arr[middle],search) < 0)
                {
                    // why must plus 1 ? Because the middle is redundant.
                    start = middle + 1;
                    continue;
                }

                // Index of middle is greater than index of search.
                if (String.Compare(arr[middle], search) > 0)
                { 
                    // Because the middle number is redundant.
                    end = middle - 1;
                    continue;
                }
            }

            return -1;
        }

        /// <summary>
        /// Linear Search Method
        /// </summary>
        /// <param name="input"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        private static int LinearSeach(string input, params string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == input)
                {
                    // return the index of the input
                    return i;
                }
            }

            // if not found return -1
            return -1;
        }
        /// <summary>
        /// Linear Search Extension Method
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        private static int LinearSeach(this string[] arr, string search)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == search)
                {
                    // return the index of the input
                    return i;
                }
            }

            // if not found return -1
            return -1;
        }

    }
}
