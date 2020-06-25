using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    public class UnderstandingList
    {
        // ------------------------------ Static Array ------------------------------ //

        // A static array is a fixed length container containing
        // n elements INDEXABLE from the range 0 to n-1
        // Indexable: Each slot inside the array can be reference with a number

        // Dynamic Array vs List ?

        public static void Main()
        {
            //Test_Optimisation();
            //Test_InsertMethods();
            //Test_TrimExcessCapacity();
            //Test_RemoveMethods();
            //Test_CustomEnumerator();
            Test_SmallFuctions();
        }

        private static void Test_SortMethods()
        {

        }

        private static void Test_Optimisation()
        {
            CustomList<int> list = new CustomList<int>();
            List<int> list2 = new List<int>();
            int[] aBunchOfItems = Enumerable.Range(0, 10000000).ToArray();

            list.AddRange(aBunchOfItems);
            list2.AddRange(aBunchOfItems);

            Console.WriteLine("Inserting..");

            // using stopwatch to test code speed
            Stopwatch timer = new Stopwatch();
            Stopwatch timer2 = new Stopwatch();


            timer.Start();
            list.InsertRange(5, aBunchOfItems);
            timer.Stop();

            timer2.Start();
            list2.InsertRange(5, aBunchOfItems);
            timer2.Stop();

            // Time taken to run.
            Console.WriteLine(timer.ElapsedTicks / (float)Stopwatch.Frequency + " Sec");
            Console.WriteLine(list.Count);
            Console.WriteLine();
            Console.WriteLine(timer2.ElapsedTicks / (float)Stopwatch.Frequency + " Sec");
            Console.WriteLine(list2.Count);
        }

        /// <summary>
        /// Test List misc functions
        /// </summary>
        private static void Test_SmallFuctions()
        {
            Console.WriteLine("\n---- Functions Test ----\n");

            // Create a new custom list
            CustomList<int> list = new CustomList<int>();
            CustomList<int> list2 = new CustomList<int>();

            for (int i = 0; i < 20; i++)
            {
                list2.Add(i);
            }
            list2.TrimExcessCapacity();

            int x = 0;
            int y = 0;
            int currentCapacity = list.Capacity;
            // Populate the list with 100 numbers 1 to 100 using while loop
            while (x < 101)
            {
                if (currentCapacity < list.Capacity)
                {
                    list.CapacityChanged += (s, e) => Console.WriteLine("Resizing() New Capacity : " + e.Capacity);
                    currentCapacity = list.Capacity;
                }
                ++x;
                list.Add(x);
            }
           
            Console.WriteLine();

            // Check list of numbers
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j] % 11 == 0)
                    {
                        // Returns to the first for loop.
                        break;
                    }
                    if (list[y] == 101)
                    {
                        // 101 will not be printed
                        // It will break and go to outer.
                        goto Outer;
                    }

                    Console.Write(list[y++] + " ");

                    //else
                    //{
                    //    Console.Write(list[y++] + " ");
                    //}
                }
                Console.WriteLine();
            }
            Outer:


            //Console.WriteLine("\nDoes the list contains: 99? \n" + list.Contains(99));
            Console.WriteLine("\nDoes the list contains: 88? \n" + list.Exists(i => i == 88));
            Console.WriteLine("\nFind if 50 in the list \n" + list.Find(i => i > 49));
            Console.WriteLine("\nFind and print all the numbers less than 3 OR greater than 10: \n" + string.Join(" ", list2.FindAll(i => i < 3 || i > 10)));
            Console.WriteLine("\nWhat is the index of the empty cell in the list? :\n" + list.LastEmptyCell());

            // alternative to lamda expression directly
            bool TestCondition(int num) => num == 21 ? true : false;
            Console.WriteLine("\nDoes list contain the number 21? : \n" + list2.TrueForAll(TestCondition));

            // Test foreach function
            Console.WriteLine("\nTesting foreach function");
            list2.ForEach(i => Console.Write(i + " "));
            Console.WriteLine();

            // Test ConvertAll funciton
            Console.WriteLine("\nTest ConvertAll Function");
            list2.ConvertAll(i => i.ToString());
            //list2.ForEach(Console.WriteLine);
            Console.WriteLine();

            // To array test
            Console.WriteLine("\nTest ToArray Method");
            int[] newArray = new int[list2.Count];
            newArray = list2.ToArray();
            Console.WriteLine(string.Join(" ",newArray));
            Console.WriteLine();

            // Check if list is empty after clearing
            Console.WriteLine("\nClearing the list now: \n");
            list.Clear();

            // Check list of numbers
            y = 0;
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j] % 11 == 0)
                    {
                        break;
                    }
                    else
                    {
                        // default will not be printed.
                        Console.Write(list[y++] + " ");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("is the list empty? : " + list.isEmpty());
        }

        /// <summary>
        /// Test Insert Method
        /// </summary>
        private static void Test_InsertMethods()
        {
            Console.WriteLine("\n---- Insert Method Test ----\n");
            int[] numlist = new int[] { 1, 2, 3, 4, 5 };
            CustomList<int> list = new CustomList<int>();

            Console.WriteLine("\nList before Insertion");
            for (int i = 0; i < 20; i++)
            {
                list.Add(i);
                Console.Write(list[i] + " ");
            }

            Console.WriteLine("\nInserting 100 at index 10.");
            list.Insert(10, 100);

            Console.WriteLine("\n Checking list if number is inserted");
            foreach (int item in list)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine("\n Testing InsertRange Method");
            list.InsertRange(10, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110);
             
            Console.WriteLine("\n Checking list if number range is inserted");
            foreach (int item in list)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

            Console.WriteLine("\nCalling the GetRange method");

            // New method to iterate and print a range using string.join
            Console.WriteLine(string.Join(" ", list.GetRange(5,5)));
            Console.WriteLine();
        }

        /// <summary>
        /// Test Trim Excess Capacity
        /// </summary>
        private static void Test_TrimExcessCapacity()
        {
            Console.WriteLine("\n---- Trim Excess Capacity Test ----\n");
            int[] numlist = new int[] { 1, 3, 5, 7 };

            CustomList<int> list = new CustomList<int>(10);
            Console.WriteLine("Capacity before trimming: " + list.Capacity + " Count: " + list.Count);

            for (int i = 0; i < numlist.Length; i++)
            {
                // Allocate one integer value using list index, does not increase count. 
                // Ienumerator requires count to work.
                list.Add(numlist[i]);
            }

            foreach (int num in list)
            {
                Console.Write(num + " ");
            }

            Console.WriteLine();
            list.TrimExcessCapacity();

            Console.WriteLine("Capacity after trimming: " + list.Capacity + " Count: " + list.Count);
        }

        private static void Test_RemoveMethods()
        {
            Console.WriteLine("\n---- RemoveAt Test ----\n");
            CustomList<int> list = new CustomList<int>(10);

            Console.WriteLine("List before RemoveAt: ");
            for (int i = 0; i < 20; i = i + 2) 
            {
                // Allocate one integer value using list index, does not increase count. 
                // Ienumerator requires count to work.
                list.Add(i);
            }
            Console.WriteLine("\nCount: " + list.Count + " Capacity: " + list.Capacity);
            foreach (int num in list)
            {
                Console.Write(num + " ");
            }

            Console.WriteLine();
            //list.RemoveAt(5);
            //list.RemoveRange(3, 1);
            list.RemoveAll(i => i < 5);

            Console.WriteLine("\nCount: " + list.Count + " Capacity: " + list.Capacity);
            foreach (int num in list)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine("\n");
        }

        private static void Test_CustomEnumerator()
        {
            Console.WriteLine("\n---- Custom Enumerator Test ----\n");

            CustomList<string> list = new CustomList<string>();

            list.Add("Bobby");
            list.Add("Christal");
            list.Add("Jannice");
            list.Add("Anna");

            // This is the breakdown of what happens in a foreach loop
            Console.WriteLine("Calling the Custom Enumerator Function");
            IEnumerator<string> rator = list.MyCustomEnumerator();
            try
            {
                while (rator.MoveNext())
                {
                    object cache = rator.Current;
                    Console.WriteLine(cache);
                }
            }
            finally
            {
                // Dispose method is always called at the end of foreach loop
                rator.Dispose();
            }
        }
    }
}

