using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    internal static class ExtensionMethods
    {
        public static string[] BubbleSort(this string[] strArr)
        {
            //string temp;

            //// Sort nums first
            //for (int j = 0; j < strArr.Length - 1; j++)
            //{
            //    for (int i = 0; i < strArr.Length - 1; i++)
            //    {
            //        // If number on the left is bigger, swap with number on the right.
            //        // Length - 1, otherwise, it will access a sentinel value, which will throw an index error.
            //        if (String.Compare(strArr[i],strArr[i+1]) > 0)
            //        {
            //            temp = strArr[i + 1];
            //            strArr[i + 1] = strArr[i];
            //            strArr[i] = temp;
            //        }
            //    }
            //}

            for (int i = strArr.Length - 1; i > 0; i--)
            {
                bool swap = false;

                for (int j = 0; j < i; j++)
                {
                    if (string.Compare(strArr[j],strArr[j+1]) > 0)
                    {
                        string temp = strArr[j];
                        strArr[j] = strArr[j + 1];
                        strArr[j + 1] = temp;
                        swap = true;
                    }
                }

                if (swap == false)
                {
                    break;
                }
            }

            return strArr;
        }

        public static int[] BubbleSort(this int[] numArr)
        {
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
                }

                // If there are no swaps, array is sorted. 
                if (swap == false)
                {
                    break;
                }
            }

            return numArr;
        }

        // Object can be used at T as well
        public static T[] RemoveFirst<T>(this T[] arr)
        {
            // initialize return array
            T[] ret = new T[arr.Length - 1];

            // Count 
            int x = 0;

            for (int i = 1; i < arr.Length; i++)
            {
                ret[x++] = arr[i];
            }

            return ret;
        }

        public static T[] Push<T>(this T[] numbers, params T[] newSetOfNumbers)
        {
            // new length
            int len = numbers.Length + newSetOfNumbers.Length;

            // initialize return array
            T[] ret = new T[len];

            // initialize new array counter
            int x = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                ret[x++] = numbers[i];
            }

            for (int i = 0; i < newSetOfNumbers.Length; i++)
            {
                ret[x++] = newSetOfNumbers[i];
            }

            return ret;
        }

        /// <summary>
        /// Takes an array/list of numbers and remove the first number in the list.
        /// Shift all the numbers down one index.
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static IEnumerable<int> Slice(this IEnumerable<int> numbers)
        {
            List<int> ret = new List<int>();

            foreach (var num in numbers)
            {
                ret.Add(num);
            }

            ret.RemoveAt(0);

            return ret;
        }

        /// <summary>
        /// Second overload to Slice that takes in int[] instead of IEnumerable<int>
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int[] Slice(this int[] numbers)
        {
            // Removing the first index so, numbers will contain one number less.
            int[] ret = new int[numbers.Length - 1];
            int x = 0;

            for (int i = 1; i < numbers.Length; i++)
            {
                ret[x] = numbers[i];
                x++;
            }

            return ret;
        }

        /// <summary>
        /// This takes in No parameters.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Slice(this string str)
        {
            return str;
        }

        /// <summary>
        /// Returns an array of char[] after positive start value.
        /// Returns an array of char[] starting from last value - start value.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static string Slice(this string str, int start)
        {
            // Initialize Char[] to be returned
            int length = 0;
            if (start > 0)
            {
                length = str.Length - start;
            }
            if (start < 0)
            {
                length = Math.Abs(start);
            }

            char[] ret = new char[length];

            int x = 0;

            // if start is greater than the array, return empty string
            if (Math.Abs(start) > str.Length)
            {
                return string.Empty;
            }
            
            // Check start value
            if (start == 0)
            {
                return str;
            }

            if (start > 0)
            {
                for (int i = start; i < str.Length; i++)
                {
                    ret[x++] = str[i];
                }
            }
            
            if(start < 0)
            {
                for (int i = str.Length + start; i < str.Length; i++)
                {
                    ret[x++] = str[i];
                }
            }

            return new string(ret);
        }

        /// <summary>
        /// Return an array of characters from starting index to ending index.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="start"> if negative count backwards </param>
        /// <param name="end"> if positive count forward </param>
        /// <returns></returns>
        public static string Slice(this string str, int start, int end)
        {
            // Set Array Length 
            int len = 0;
            int x = 0;

            // Check if end == 0, if so, return empty.
            if (end == 0)
            {
                return string.Empty;
            }

            if (start >= 0 && end > 0 && start < end)
            {
                // - 1 , zero index.
                len = end - start;

                if (len <= 0 || len > str.Length)
                {
                    return string.Empty;
                }

                char[] ret = new char[len];

                for (int i = start; i < end; i++)
                {
                    ret[x++] = str[i];
                }

                return new string(ret);
            }

            if (start <= 0 && end < 0)
            {
                if (start == 0)
                {
                    len = str.Length + end;
                }
                else
                {
                    len = Math.Abs(start) - Math.Abs(end);
                }

                if (len <= 0 || len > str.Length)
                {
                    return string.Empty;
                }

                char[] ret = new char[len];

                if (start == 0)
                {
                    for (int i = start; i < str.Length + end; i++)
                    {
                        ret[x++] = str[i];
                    }
                }
                else
                {
                    for (int i = str.Length + start; i < str.Length + end; i++)
                    {
                        ret[x++] = str[i];
                    }
                }

                return new string(ret);
            }

            if (start >= 0 && end < 0)
            {
                len = str.Length - start + end;

                if (len <= 0 || len > str.Length)
                {
                    return string.Empty;
                }

                char[] ret = new char[len];

                for (int i = start; i < start + len; i++)
                {
                    ret[x++] = str[i];
                }

                return new string(ret);
            }

            if (start <= 0 && end > 0)
            {
                if (start == 0)
                {
                    len = end;
                }
                else
                {
                    len = end - str.Length + Math.Abs(start);
                }

                if (len <= 0 || len > str.Length)
                {
                    return string.Empty;
                }

                char[] ret = new char[len];

                if (start == 0)
                {
                    for (int i = 0; i < len; i++)
                    {
                        ret[x++] = str[i];
                    }
                }
                else
                {
                    for (int i = str.Length + start; i < end; i++)
                    {
                        ret[x++] = str[i];
                    }
                }

                return new string(ret);
            }

            return string.Empty;
        }

        public static int[] Slice(this int[] numArr, int start)
        {
            // Initialize Char[] to be returned
            int length = 0;
            if (start > 0)
            {
                length = numArr.Length - start;
            }
            if (start < 0)
            {
                length = Math.Abs(start);
            }

            int[] ret = new int[length];

            int x = 0;

            // if start is greater than the array, return empty string
            if (Math.Abs(start) > numArr.Length)
            {
                return null;
            }

            // Check start value
            if (start == 0)
            {
                return numArr;
            }

            if (start > 0)
            {
                for (int i = start; i < numArr.Length; i++)
                {
                    ret[x++] = numArr[i];
                }
            }

            if (start < 0)
            {
                for (int i = numArr.Length + start; i < numArr.Length; i++)
                {
                    ret[x++] = numArr[i];
                }
            }

            return ret;
        }
        public static int[] Slice(this int[] numArr, int start, int end)
        {
            // Set Array Length 
            int len = 0;
            int x = 0;

            // Check if end == 0, if so, return empty.
            if (end == 0)
            {
                return null;
            }

            if (start >= 0 && end > 0 && start < end)
            {
                // - 1 , zero index.
                len = end - start;

                if (len <= 0 || len > numArr.Length)
                {
                    return null;
                }

                int[] ret = new int[len];

                for (int i = start; i < end; i++)
                {
                    ret[x++] = numArr[i];
                }

                return ret;
            }

            if (start <= 0 && end < 0)
            {
                if (start == 0)
                {
                    len = numArr.Length + end;
                }
                else
                {
                    len = Math.Abs(start) - Math.Abs(end);
                }

                if (len <= 0 || len > numArr.Length)
                {
                    return null;
                }

                int[] ret = new int[len];

                if (start == 0)
                {
                    for (int i = start; i < numArr.Length + end; i++)
                    {
                        ret[x++] = numArr[i];
                    }
                }
                else
                {
                    for (int i = numArr.Length + start; i < numArr.Length + end; i++)
                    {
                        ret[x++] = numArr[i];
                    }
                }

                return ret;
            }

            if (start >= 0 && end < 0)
            {
                len = numArr.Length - start + end;

                if (len <= 0 || len > numArr.Length)
                {
                    return null;
                }

                int[] ret = new int[len];

                for (int i = start; i < start + len; i++)
                {
                    ret[x++] = numArr[i];
                }

                return ret;
            }

            if (start <= 0 && end > 0)
            {
                if (start == 0)
                {
                    len = end;
                }
                else
                {
                    len = end - numArr.Length + Math.Abs(start);
                }

                if (len <= 0 || len > numArr.Length)
                {
                    return null;
                }

                int[] ret = new int[len];

                if (start == 0)
                {
                    for (int i = 0; i < len; i++)
                    {
                        ret[x++] = numArr[i];
                    }
                }
                else
                {
                    for (int i = numArr.Length + start; i < end; i++)
                    {
                        ret[x++] = numArr[i];
                    }
                }

                return ret;
            }

            return null;
        }

        /// <summary>
        /// Remove the last element of the string
        /// </summary>
        /// <param name="input"> Input String " "</param>
        /// <returns></returns>
        public static string RemoveLastElementofString(this string input)
        {
            char[] ret = new char[input.Length - 1];
            int x = 0;
            for (int i = 0; i < input.Length - 1; i++)
            {
                ret[x] = input[i];
                x++;
            }

            return new string(ret);
        }

        /// <summary>
        /// Converts an int into an int array, indvidual digits, 10 will be [1][0].
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int[] GetIntArray(this int num)
        {
            List<int> listOfInts = new List<int>();
            while (num > 0)
            {
                listOfInts.Add(num % 10);
                num = num / 10;
            }

            listOfInts.Reverse();
            return listOfInts.ToArray();
        }

        public static int ConvertIntArrayToInt(this int[] numbers)
        {
            int ret = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                ret += numbers[i] * Convert.ToInt32(Math.Pow(10, numbers.Length - i - 1));
            }

            return ret;
        }

        /// <summary>
        /// Returns a combine of two IEnumerables into one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        public static List<T> Concatenate<T>(this IEnumerable<T> list, IEnumerable<T> list2)
        {
            List<T> ret = new List<T>();

            foreach (var item in list)
            {
                ret.Add(item);
            }

            foreach (var item in list2)
            {
                ret.Add(item);
            }

            return ret;
        }

        /// <summary>
        /// Returns the results of a merge string together with an array of strings.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strArr"></param>
        /// <returns></returns>
        public static string[] Concatenate(this string str, params string[] strArr)
        {
            string[] ret = new string[strArr.Length + 1];

            // put the first element in to ret
            ret[0] = str;

            // Counter
            int x = 0;

            // second element onwards
            for (int i = 1; i < strArr.Length + 1; i++)
            {
                ret[i] = strArr[x++];
            }

            return ret;
        }

        /// <summary>
        /// Returns a merge of two different string arrays.
        /// </summary>
        /// <param name="oldStrArr"></param>
        /// <param name="strArr"></param>
        /// <returns></returns>
        public static string[] Concatenate(this string[] oldStrArr, params string[] strArr)
        {
            // return string new length
            int len = oldStrArr.Length + strArr.Length;

            // Initialize return string[]
            string[] ret = new string[len];

            // Counter 
            int x = 0;

            for (int i = 0; i < oldStrArr.Length; i++)
            {
                ret[x++] = oldStrArr[i];
            }

            for (int i = 0; i < strArr.Length; i++)
            {
                ret[x++] = strArr[i];
            }

            return ret;
        }
    }
}
