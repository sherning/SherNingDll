using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    public class UnderstandingRecursion
    {
        public static void Main()
        {
            Console.WriteLine("SumRange(Num = 4), Answer = " + SumRange(4) + "\n");
            Console.WriteLine("Factorial(Num = 4), Answer = " + Factorial(4) + "\n");
            Console.WriteLine("CollectingOddNumbers(Num = 123456789), Answer = " + string.Join(" ", CollectOddNumbers(123456789)) + "\n");

            Console.WriteLine("CollectingOddNumbers(Num[] = 1,2,3,4,5,6,7,8,9), Answer = "
                + string.Join(" ", CollectOddNumbers(1, 2, 3, 4, 5, 6, 7, 8, 9)) + "\n");

            Console.WriteLine("Power(exponents = 6, base number = 2), Answer = " + Power(6, 2) + "\n");

            Console.WriteLine("ProductOfArray(1,2,3,4,5), Answer = " + ProductOfArray(1, 2, 3, 4, 5) + "\n");

            Console.WriteLine("RecursiveRange(num = 10), Answer = " + RecursiveRange(10) + "\n");

            Console.WriteLine("Fib(position = 10), Answer = " + Fib(4) + "\n");

            Console.WriteLine("Reverse(string \"abcdefg\", Answer = " + Reverse("abcdefg") + "\n");

            Console.WriteLine("isPalindrome(string \"cattottac\"), Answer = " + isPalindrome("cattottac") + "\n");

            Console.WriteLine("SomeRecursive(i => i % 2 == 0, 1,2,3,4,5), Answer = " + SomeRecursive(i => i % 2 == 0, 1, 2, 3, 4, 5) + "\n");

            Console.WriteLine("CapitalizeFirstLetter(str[] \"jonny\",\"wilson\",\"peter\"), Answer = " 
                + string.Join(" ",CapitaliseFirstLetter("johnny", "wilson", "peter")) + "\n");

            Console.WriteLine("NestedEvenSum(\"John\", 10, \"Sally\",\"Paul\", 21, 30), Answer : "
                + NestedEvenSum("John", 10, "Sally", "Paul", 21, 30) + "\n");

            Console.WriteLine("CapitalizeWords(str[] \"jonny\",\"wilson\",\"peter\"), Answer = "
                + string.Join(" ", CapitalizeWords("johnny", "wilson", "peter")) + "\n");

            Console.WriteLine("StringifyNumbers(\"John\", 10, \"Sally\",\"Paul\", 21, 30), Answer : "
                + StringifyNumbers("John", 10, "Sally", "Paul", 21, 30) + "\n");

            Console.WriteLine("CollectStrings(\"John\", 10, \"Sally\",\"Paul\", 21, 30), Answer : "
                + string.Join(" ",CollectStrings("John", 10, "Sally", "Paul", 21, 30)) + "\n");

            Test();
        }

        private static IEnumerable<string> CollectStrings(params object[] objs)
        {
            List<string> strList = new List<string>();

            // end recursion
            if (objs.Length == 0)
            {
                // Cannot return null here.
                return strList;
            }

            // Check if object is string
            if (objs[0] is string str)
            {
                strList.Add(str);
            }

            // Concatenate combines two list into one
            return strList.Concatenate(CollectStrings(objs.RemoveFirst()));
        }

        /// <summary>
        /// Returns a filtered and combined string of numbers into a single string 
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        private static string StringifyNumbers(params object[] objs)
        {
            string num = null;

            if (objs.Length == 0)
            {
                return string.Empty;
            }

            if (objs[0].GetType() == typeof(int))
            {
                num = objs[0].ToString();
            }

            return string.Join("", num + StringifyNumbers(objs.RemoveFirst()));
        }
        /// <summary>
        /// Takes an array of objects and return the sum of integers which are even.
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        private static int NestedEvenSum(params object[] objs)
        {
            // sum
            int sum = 0;

            // Define base case
            if (objs.Length == 0)
            {
                return 0;
            }

            if (objs[0].GetType() == typeof(int) && (int)objs[0] % 2 == 0)
            {
                sum = (int)objs[0];
            }

            return sum + NestedEvenSum(objs.RemoveFirst());
        }

        private static string[] CapitalizeWords(params string[] words)
        {
            // Return Array
            string[] ret = new string[words.Length];

            // int count
            int x = 0;

            // Helper Method
            void Helper(string[] helper)
            {
                // if string[] is empty, exit recursion loop
                if (helper.Length == 0)
                {
                    return;
                }

                char[] charArr = new char[helper[0].Length];

                // The first string in helper
                for (int i = 0; i < helper[0].Length; i++)
                {
                    // Capitalize word
                    charArr[i] = char.ToUpper(helper[0][i]);
                }

                // Add each element to the return array.
                ret[x++] = new string(charArr);

                // Enter recursion loop
                Helper(helper.RemoveFirst());
            }

            // Initialise the first time
            Helper(words);

            // Call helper method.
            return ret;
        }
        /// <summary>
        /// Return an array of string, with the first letter of each string Capitalized
        /// </summary>
        /// <param name="strArr"></param>
        /// <returns></returns>
        private static string[] CapitaliseFirstLetter(params string[] strArr)
        {
            // Return Array
            string[] ret = new string[strArr.Length];

            // int count
            int x = 0;

            // Helper Method
            void Helper(string[] helper)
            {
                // if string[] is empty
                if (helper.Length == 0)
                {
                    return;
                }

                char[] charArr = new char[helper[0].Length];

                // The first string in helper
                for (int i = 0; i < helper[0].Length; i++)
                {
                    // Check for the first letter.
                    if (i == 0)
                    {
                        charArr[i] = char.ToUpper(helper[0][i]);
                        continue;
                    }

                    charArr[i] = helper[0][i];
                }

                // Add the first element of to the return array.
                ret[x++] = new string(charArr);

                // Enter recursion
                Helper(helper.RemoveFirst());
            }

            // Initialise the first time
            Helper(strArr);

            // Call helper method.
            return ret;
        }

        private static int SumRange(int num)
        {
            // Define base case.
            if (num == 1)
            {
                return 1;
            }

            // Check the call stack for the function call...
            // Return 4 + ... waiting for next return call
            // Return .. 3 + ... waiting ...
            // Return .. 2 + ... waiting ...
            // Return 1
            return num + SumRange(--num);
        }
        private static int Factorial(int num)
        {
            // This is the base case
            if (num == 1)
            {
                return 1;
            }

            // This is the recursive portion
            return num * Factorial(--num);
        }

        private static IEnumerable<int> CollectOddNumbers(int number)
        {
            // use recusion helper method, a method inside a method.
            List<int> ret = new List<int>();

            void helper(int[] helperInput)
            {
                // If array is empty and without length
                // This is the base case.
                if (helperInput.Length == 0)
                {
                    return;
                }

                // Check if number is odd
                if (helperInput[0] % 2 != 0)
                {
                    // Yes, add number to the list ret
                    ret.Add(helperInput[0]);
                }

                // Remove one element and shift the index back and pass it to helper method.
                // Think of it as A--; 
                helper(helperInput.Slice());
            }

            // Convert number into an array.
            helper(number.GetIntArray());

            return ret;
        }

        /// <summary>
        /// CollectOddNumbers Second overload that takes in a Params of int[], instead of single number.
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        private static IEnumerable<int> CollectOddNumbers(params int[] numbers)
        {
            // Everytime CollectOddNumbers is called, the list will be resetted to empty.
            List<int> ret = new List<int>();

            // Define base case, which is an empty array.
            if (numbers.Length == 0)
            {
                return ret;
            }

            // Check if the first number is odd, if yes, add to ret.
            if (numbers[0] % 2 != 0)
            {
                ret.Add(numbers[0]);
            }

            // remove the first element in the input array.
            int[] temp = numbers.Slice();

            // ret = [1].CollectOddNumbers(2,3,4,5,6,7,8,9) ...
            ret = ret.Concatenate(CollectOddNumbers(temp));

            return ret;
        }

        private static int Power(int exponents, int bases)
        {
            // Define base case
            if (exponents == 0)
            {
                return 1;
            }

            // return Power
            return bases *= Power(exponents - 1, bases);
        }

        private static int ProductOfArray(params int[] numbers)
        {
            // Define base case.
            if (numbers.Length == 0)
            {
                return 1;
            }

            // Call stack of 6 times ProductOfArray is called (Exclude Once from Main)
            // The first number is called, then removed and the new array called recursively
            return numbers[0] * ProductOfArray(numbers.Slice());
        }

        private static int RecursiveRange(int num)
        {
            // Define base case
            if (num == 0)
            {
                return 0;
            }

            // return recursive range
            return num + RecursiveRange(--num);
        }

        /// <summary>
        /// Return the poistion (1 base) of the number in the sequence
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private static int Fib(int position)
        {
            if (position <= 2)
            {
                return 1;
            }

            return Fib(position - 1) + Fib(position - 2);
        }

        private static string Reverse(string inputString)
        {
            // Define base case.
            // Consider the case if has 0 elements
            if (inputString.Length == 0)
            {
                return "";
            }

            // First consider if the case has only one elements
            // Second case consider if the case has two elements
            return inputString[inputString.Length - 1] + Reverse(inputString.RemoveLastElementofString());
        }

        /// <summary>
        /// If reverse string of input string is the same, Palindrome returns true
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool isPalindrome(string str)
        {
            //// Compare reverse string = input string, if correct = true
            //if (input == Reverse(input))
            //{
            //    return true;
            //}

            //return false;

            // Textbook Solution :
            // if left only one element in the center, means left = right, for odd number of char[]
            if (str.Length == 1)
            {
                return true;
            }

            // if left only two elements in the center, means left = right, for even number of char[]
            if (str.Length == 2)
            {
                return str[0] == str[1];
            }

            // Check the first element vs last element
            if (str[0] == str.Slice(-1)[0])
            {
                // 1 , -1 removes the first and last elements
                return isPalindrome(str.Slice(1, -1));
            }

            return false;
        }

        private static bool SomeRecursive(Func<int,bool> condition, params int[] numbers)
        {
            if (numbers.Length == 0)
            {
                return false;
            }

            if (condition(numbers[0]))
            {
                return true;
            }

            return SomeRecursive(condition, numbers.Slice());
        }
       
        // Cannot be done for C#
        //private static int[] Flatten(params int[] numbers)
        //{
        //    int[] ret = new int[numbers.Length];
        //    Type valueType = numbers.GetType();

        //    for (int i = 0; i < numbers.Length; i++)
        //    {
        //        if (valueType.IsArray)
        //        {
        //            ret = (int[])ret.Concat(Flatten(numbers[i]));
        //        }
        //        else
        //        {
        //            ret.Push(numbers[i]);
        //        }
        //    }

        //    return ret;
        //}
        private static void Test()
        {
           // string str = "Johny";
            string[] strArr = { "Peter", "Jane", "Donald" };

            Console.WriteLine(string.Join(" ",strArr.Concatenate("Dan","Havey","Mike").RemoveFirst()));
        }
    }
}
