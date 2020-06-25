using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    public class UnderstandingProblemSolving
    {
        #region Notes
        /*
         *   Algorithms
         * - A set of steps to accomplish a certain task.
         * 
         *   How do you improve ?
         * - Devise a plan for solving problems
         * - Master common problem solving patterns
         * 
         *   Problem Solving
         * - Step 1 : Understand the problem
         * - Step 2 : Explore concrete examples
         * - Step 3 : Break it down
         * - Step 4 : Solve or simplify
         * - Step 5 : Look back and refactor
         */
        #endregion
        public static void Main()
        {
            Question1("Hello! My name is Sher Ning =)");
            Console.WriteLine(IsTheSame(new int[] { 1, 2, 3 }, new int[] { 4, 1, 9 }));
            Console.WriteLine(IsTheSame(new int[] { 1, 2, 3 }, new int[] { 1, 9 }));
            Console.WriteLine(IsTheSame(new int[] { 1, 2, 1 }, new int[] { 4, 4, 1 }));
            Console.WriteLine("is reverse order?  " + isReverseOrder("aaz", "zza"));
            Console.WriteLine("is reverse order?  " + isReverseOrder(" ", " "));
            Console.WriteLine("is reverse order?  " + isReverseOrder("anaGram", "margana"));
            Console.WriteLine("is reverse order?  " + isReverseOrder("rat ", " tar"));
            Console.WriteLine();
            Console.WriteLine("is reverse order?  " + isAnagram("aaz", "zza"));
            Console.WriteLine("is reverse order?  " + isAnagram(" ", " "));
            Console.WriteLine("is reverse order?  " + isAnagram("anagram", "nagaram"));
            Console.WriteLine("is reverse order?  " + isAnagram("rat ", " car"));
            Console.WriteLine();
            isAnInversePair(-3, -2, -1, 0, 1, 2, 3);
            isAnInversePair(-2, -1, 0, 1, 2, 3);
            Console.WriteLine();
            Console.WriteLine(string.Join(" ", RemoveRepeatedValues(1, 1, 2, 3, 4, 4, 5, 6, 6, 7)));
            Console.WriteLine(string.Join(" ", RemoveRepeatedValuesUnsorted(5, 4, 4, 3, 3, 2, 2, 1)));
            Console.WriteLine(string.Join(" ", RemoveRepeatedValuesUnsorted(3, 3, 5, 5, 8, 8, 7, 6, 5, 3, 2, 2, 1, 1, 0)));
            Console.WriteLine();
            Console.WriteLine("Max Sub Array Sum: " + MaxSubArraySum(2, 1, 2, 3, 4, 5));
            Console.WriteLine();
            Console.WriteLine(SameFrequency(3589578, 5779385));
            Console.WriteLine(string.Join(" ", GetIntArray(321)));
            Console.WriteLine();
            Console.WriteLine("Are there duplicates? " + AreThereDuplicates(0,1,2,4,3,7,9,8,9));
            Console.WriteLine();
            Console.WriteLine("AveragePair: " + AveragePair(8,1,3,3,5,6,7,10,12,19));
            Console.WriteLine("AveragePair: " + AveragePair(8.5,5,3,8,9,2));
            Console.WriteLine();
            Console.WriteLine("Is Subsequence: " + IsSubsequence("run","abcreuoon"));
            Console.WriteLine();
            Console.WriteLine("max sub array sum sorted: " + MaxSubArraySumSorted(2,5,3,8,9,2));
            Console.WriteLine("Min Sub Array Length: " + MinSubArrayLength(18,2,3,6,6,5,1));
            Console.WriteLine();
            Console.WriteLine("Find Longest Substring: " + FindLongestSubstring("mossmossmoss"));
        }

        private static void Question1(string str)
        {
            // Break a string down and return the count of each character
            Dictionary<char, int> charCount = new Dictionary<char, int>();

            //char[] characterList = new char[str.Length];
            char[] characterList = str.ToLower().ToCharArray();

            foreach (char c in characterList)
            {
                // stuct methods static
                if (IsLetterOrDigit(c) == false)
                {
                    // skip if it is not a letter or digit
                    continue;
                }

                if (charCount.ContainsKey(c))
                {
                    charCount[c]++;
                }
                else
                {
                    charCount.Add(c, 1);
                }

            }

            Console.WriteLine(string.Join(" ", charCount));

            foreach (var item in charCount)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }
        }

        private static bool IsLetterOrDigit(char c)
        {
            int charCodeAt = (int)c;

            // Check for numbers 0 -9
            // Check for lower case alphabets
            // Check for upper case alphabets
            // from Ascii table
            if (!(charCodeAt >= 48 && charCodeAt <= 59)
            && !(charCodeAt >= 65 && charCodeAt <= 90)
            && !(charCodeAt >= 97 && charCodeAt <= 122))
            {
                return false;
            }

            // if character is letter (upper and lower) or digit
            return true;
        }

        private static bool IsTheSame(int[] a, int[] b)
        {
            int results1 = 0;
            int results2 = 0;

            // check if array length is the same
            if (a.Length == b.Length)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    //results1 += a[i] * a[i];
                    results1 += (int)Math.Pow(a[i], 2);
                    results2 += b[i];
                }
            }
            else
            {
                return false;
            }

            // Check if the sum of results1 and results2 are the same.
            return results1 == results2 ? true : false;
        }

        private static bool isReverseOrder(string a, string b)
        {
            char[] results1;
            char[] results2;
            int x = b.Length - 1;

            results1 = a.ToLower().ToCharArray();
            results2 = b.ToLower().ToCharArray();

            if (a.Length != b.Length)
            {
                return false;
            }

            for (int i = 0; i < a.Length; i++)
            {
                if (results1[i] != results2[x])
                {
                    return false;
                }
                x--;
            }

            // means all the elements are opposite from one another.
            return true;
        }

        private static bool isAnagram(string a, string b)
        {
            char[] results1;
            char[] results2;
            int sumOfOne = 0;
            int sumOfTwo = 0;

            results1 = a.ToLower().ToCharArray();
            results2 = b.ToLower().ToCharArray();

            if (a.Length != b.Length)
            {
                return false;
            }

            for (int i = 0; i < a.Length; i++)
            {
                sumOfOne += results1[i];
                sumOfTwo += results2[i];
            }

            return sumOfOne == sumOfTwo ? true : false;
        }

        private static void isAnInversePair(params int[] a)
        {
            int x = a.Length - 1;

            for (int i = 0; i < a.Length; i++)
            {
                // Check for inverse pair, and take the first pair
                if (a[i] == -a[x])
                {
                    Console.WriteLine("[{0}, {1}]", a[i], a[x]);
                    return;
                }
            }

            Console.WriteLine("No inverse pair found!");
        }

        /// <summary>
        /// Remove repeated values when inputs are already sorted. 
        /// Using multiple pointers on the same array.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private static int[] RemoveRepeatedValues(params int[] a)
        {
            if (a.Length == 0)
            {
                return null;
            }

            if (a.Length < 2)
            {
                Console.WriteLine("Please enter a number list of at least 2 values.");
                return null;
            }

            int x = 0;
            // Pointer starts on array aways from start value.
            int y = 1;
            int[] ret = new int[a.Length];

            // Count number of elements added to new array. 1 is default.
            int Count = 1;

            // Start - index 0 of return value.
            ret[x] = a[x];

            // try in any order and with order 2 methods
            while (true)
            {
                if (ret[x] == a[y])
                {
                    y++;
                }
                else
                {
                    //a[x] != a[y]
                    ret[++x] = a[y];
                    y++;
                    Count++;
                }

                // array index is always Length - 1,
                // But, in this situation, y > x
                if (y == a.Length)
                {
                    break;
                }
            }

            // Resize the array and remove the zeroes.
            int[] temp = new int[Count];

            for (int i = 0; i < Count; i++)
            {
                temp[i] = ret[i];
            }

            ret = temp;

            return ret;
        }
        /// <summary>
        /// Return a sorted array of integers while removing duplicated values.
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        private static int[] RemoveRepeatedValuesUnsorted(params int[] nums)
        {
            if (nums.Length == 0)
            {
                return null;
            }

            int[] temp;

            // Sort the input array
            temp = BubbleSort(nums);

            return RemoveRepeatedValues(temp);
        }

        /// <summary>
        /// Returns a sorted(Ascending) array of integers.
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        private static int[] BubbleSort(int[] nums)
        {
            int temp;

            // Sort nums first
            for (int j = 0; j < nums.Length - 1; j++)
            {
                for (int i = 0; i < nums.Length - 1; i++)
                {
                    // If number on the left is bigger, swap with number on the right.
                    // Length - 1, otherwise, it will access a sentinel value, which will throw an index error.
                    if (nums[i] > nums[i + 1])
                    {
                        temp = nums[i + 1];
                        nums[i + 1] = nums[i];
                        nums[i] = temp;
                    }
                }
            }

            // then apply remove repeated values method.
            return nums;
        }

        private static int MaxSubArraySum(int a, params int[] b)
        {
            // in case series consists of negative numbers.
            //double sum = double.NegativeInfinity;
            int sum = 0;
            int tempSum = 0;

            // in the event there is no b input
            if (b.Length == 0)
            {
                Console.WriteLine("Please enter at least one value for b: ");
                return 0;
            }

            // in the event there is no a input
            if (a == 0)
            {
                Console.WriteLine("Please enter a positive non-zero value for a : ");
                return 0;
            }

            // My Method - got to use negative infinity
            //for (int i = 0; i <= b.Length - a; i++)
            //{
            //    for (int j = i; j < a + i; j++)
            //    {
            //        tempSum += b[j];
            //    }

            //    if (tempSum > sum)
            //    {
            //        sum = tempSum;
            //    }

            //    tempSum = 0;
            //}

            // Sliding window method
            for (int i = 0; i < a; i++)
            {
                sum += b[i];
            }

            // sum up the first 'a' numbers
            tempSum = (int)sum;

            for (int j = a; j < b.Length; j++)
            {
                // subtract the number before, and add the number after
                tempSum = tempSum - b[j - a] + b[j];
                sum = Math.Max(sum, tempSum);
            }

            // return sum, need to cast from double to int. 
            return (int)sum;
        }

        /// <summary>
        /// Returns true if the first and the second integer value contains the same set of numbers.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static bool SameFrequency(int a, int b)
        {
            int[] ArrayA = GetIntArray(a);
            int[] ArrayB = GetIntArray(b);

            // There are 10 digits 0 to 9.
            int[] CountA = new int[10];
            int[] CountB = new int[10];

            // if array length is different, it wont have the same frequency.
            if (ArrayA.Length != ArrayB.Length)
            {
                return false;
            }

            // Load the Count Array for each repeated number.
            for (int i = 0; i < ArrayA.Length; i++)
            {
                if (CountA[ArrayA[i]] == 0)
                {
                    CountA[ArrayA[i]] = 1;
                }
                else
                {
                    CountA[ArrayA[i]]++;
                }

                if (CountB[ArrayB[i]] == 0)
                {
                    CountB[ArrayB[i]] = 1;
                }
                else
                {
                    CountB[ArrayB[i]]++;
                }
            }

            // Check and see if the two Count Arrays Carry the same value.
            for (int i = 0; i < CountA.Length; i++)
            {
                if (CountA[i] != CountB[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// return true if there are duplicates in the number list.
        /// </summary>
        /// <param name="numlist"></param>
        /// <returns></returns>
        private static bool AreThereDuplicates(params int[] numlist)
        {
            if (numlist.Length == 1)
            {
                return true;
            }

            int[] temp = new int[numlist.Length];

            temp = BubbleSort(numlist);

            for (int i = 0; i < temp.Length - 1; i++)
            {
                if (temp[i] == temp[i+1])
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// Takes an integer and breaks it into a IntArray
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static int[] GetIntArray(int num)
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

        /// <summary>
        /// Go through a number list and find the pair whose average equals to num.
        /// </summary>
        /// <param name="num"></param>
        /// <param name="numList"></param>
        /// <returns></returns>
        private static bool AveragePair(double num, params int[] numList)
        {
            int start = 0;
            int end = numList.Length -1;

            if (numList.Length == 0)
            {
                Console.WriteLine("Please enter a series of integers");
                return false;
            }

            if (num == 0)
            {
                Console.WriteLine("Please enter a valid value");
                return false;
            }

            // my solution
            while (true)
            {
                double avg = (double)(numList[start] + numList[end]) / 2;

                if (avg == num)
                {
                    Console.WriteLine("avg " + avg + " == " + "num " + num);
                    return true;
                }

                if (start == end - 1)
                {
                    end--;
                    start = -1;
                    if (end == 1)
                    {
                        return false;
                    }
                }

                start++;
            }
        }
        /// <summary>
        /// Does string B contains the characters of string A in sequence ?
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static bool IsSubsequence(string a , string b)
        {
            char[] arrayA = a.ToCharArray();
            char[] arrayB = b.ToCharArray();

            int x = 0;
            int y = 0;

            if (a.Length == 0 || b.Length == 0)
            {
                Console.WriteLine("Please fill in both inputs with a valid string.");
                return false;
            }

            while (y <= arrayB.Length - 1)
            {
                if (arrayA[x] == arrayB[y])
                {
                    x++;
                    y++;
                }
                else
                {
                    y++;
                }

                // if x manage to complete to the end of it's string.
                if (x == arrayA.Length)
                {
                    return true;
                }
            }

            return false;
        }

        private static int MaxSubArraySumSorted(int length, params int[] numbers)
        {
            if (numbers.Length < length)
            {
                Console.WriteLine("Please enter a list of numbers that is greater than the length");
                return 0;
            }

            int maxValue = Int32.MinValue;
            int currentRunningSum = 0;

            // Sliding window approach
            for (int i = 0; i < numbers.Length; i++)
            {
                currentRunningSum += numbers[i];

                // Once window is built, then this will activate.
                if (i >= length - 1)
                {
                    maxValue = Math.Max(maxValue, currentRunningSum);
                    currentRunningSum -= numbers[i - (length - 1)];
                }
            }

            return maxValue;
        }

        private static int MinSubArrayLength(int condition, params int[] numbers)
        {
            // there is two types one is exactly condition or more than or equals to condition.
            // dynamic window approach
            int total = 0;
            int start = 0;
            int end = 0;
            int minLength = Int32.MaxValue;

            while (start < numbers.Length)
            {
                if (total < condition && end < numbers.Length)
                {
                    total += numbers[end];
                    end++;
                }
                else if (total >= condition)
                {
                    minLength = Math.Min(minLength, end - start);
                    total -= numbers[start];
                    start++;
                }
                else
                {
                    break;
                }
            }

            return minLength == Int32.MaxValue ? 0 : minLength;
        }

        private static int SmallestSubArray(int targetSum, params int[] numbers)
        {
            // dynamic sliding window approach to solving problems
            int minWindowSize = Int32.MinValue;
            int currentWindowSum = 0;
            int windowStart = 0;

            for (int windowEnd = 0; windowEnd < numbers.Length; windowEnd++)
            {
                currentWindowSum += numbers[windowEnd];

                while (currentWindowSum >= targetSum)
                {
                    minWindowSize = Math.Min(minWindowSize, windowEnd - windowStart + 1);
                    currentWindowSum -= numbers[windowStart];
                    windowStart++;
                }
            }

            return minWindowSize == Int32.MinValue ? 0 : minWindowSize;
        }

        private static int FindLongestSubstring(string str)
        {
            // Check if string is empty
            if (str == null)
            {
                Console.WriteLine("Please enter a valid string: ");
                return 0;
            }

            // Number of characters in ASCII table
            const int NO_OF_CHARS = 256;

            int n = str.Length;

            // length of current substring 
            int cur_len = 1;

            // result 
            int max_len = 1;

            // previous index 
            int prev_index;

            int i;
            int[] visited = new int[NO_OF_CHARS];

            /* Initialize the visited array as -1, -1 is  
            used to indicate that character has not been  
            visited yet. */
            for (i = 0; i < NO_OF_CHARS; i++)
            {
                visited[i] = -1;
            }

            /* Mark first character as visited by storing the 
                index of first character in visited array. */
            visited[str[0]] = 0;

            /* Start from the second character. First character is 
            already processed (cur_len and max_len are initialized 
            as 1, and visited[str[0]] is set */
            for (i = 1; i < n; i++)
            {
                // previous index = inital stored value, or overriden value
                prev_index = visited[str[i]];

                /* If the current character is not present in 
                the already processed substring or it is not 
                part of the current NRCS, then do cur_len++ 
                Second part of the condition, is to evaluate when current char is already not -1
                i -cur_len is the starting index of the new substring.*/
                if (prev_index == -1 || i - cur_len > prev_index)
                    cur_len++;

                /* If the current character is present in currently 
                considered NRCS, then update NRCS to start from 
                the next character of the previous instance. */
                else
                {
                    /* Also, when we are changing the NRCS, we 
                    should also check whether the length of the 
                    previous NRCS was greater than max_len or 
                    not.*/
                    if (cur_len > max_len)
                        max_len = cur_len;

                    cur_len = i - prev_index;
                }

                // update the index of current character 
                visited[str[i]] = i;
            }

            // Compare the length of last NRCS with max_len and 
            // update max_len if needed 
            if (cur_len > max_len)
                max_len = cur_len;

            return max_len;
        }
    }
}
