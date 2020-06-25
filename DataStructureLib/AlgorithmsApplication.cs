using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    class AlgorithmsApplication
    {
        public static void Main()
        {
            GridPuzzle puzzle = new GridPuzzle();
            puzzle.GetUserInput(1, 2, 3, 14, 5, 6, 9);
            puzzle.IsSolvable();

            
            

        }

        #region 4 X 4 Grid Puzzle
        /*Psuedo code
         * arr[0] = don't use.
         * Step 1: Solve arr[1] and arr[2] first
         * Step 2: Solve arr[3] and arr[4] first
         * Step 3: Solve arr[5] and arr[6]
         * Step 4: Solve arr[6] and arr[7]
         * Step 5: Solve arr[9] and arr[13]
         * Step 6: Solve arr[10 and arr[14]
         * use multi-dimension array instead.
        */

        class GridPuzzle
        {
            private int[] OutputArr;
            private int[] InputArr;
            private bool Test = true;

            public GridPuzzle()
            {
                // do not use Answer[0]
                OutputArr = new int[17];
                InputArr = new int[17];
            }

            public bool PossibleSolution()
            {
                int[] sampleSolution = new int[17];
                for (int i = 0; i < sampleSolution.Length; i++)
                    sampleSolution[i] = i;

                // the key to whether a solution is feasible depends on where
                // 16 is placed vs the parity of the number of steps it will take
                int[] testArr = OutputArr;



                return false;
            }

            /// <summary>
            /// Takes and input and compares with the output.
            /// </summary>
            /// <returns></returns>
            public bool IsSolvable()
            {
                // Fact 1: You can convert any permutation into any other
                // permutation using only transpositions.

                // Fact 2: The number of steps required is not fixed
                // but the parity (even / odd) of that number is fixed.

                // Create temp array
                int[] tempArr = InputArr;

                //*** set output to default
                for (int i = 0; i < OutputArr.Length; i++) OutputArr[i] = i;

                // Create two pointers, one for each input and output array.
                int x = 1;
                int y = 1;

                // count number of steps required.
                int count = 0;

                while (x < OutputArr.Length)
                {
                    if (OutputArr[x] != InputArr[y])
                    {
                        // Do not change the output
                        int correctNum = OutputArr[x];

                        for (int i = InputArr.Length - 1; i > x; i--)
                        {
                            if (tempArr[i] == correctNum)
                            {
                                // swap with the number before.
                                int temp = tempArr[i];
                                tempArr[i] = tempArr[i - 1];
                                tempArr[i - 1] = temp;
                                count++;
                            }
                        }
                    }

                    // Increase the pointer by 1.
                    x++;
                    y++;
                }

                for (int i = 0; i < tempArr.Length; i++)
                {
                    Console.Write($"[{tempArr[i]}] ");
                }
                Console.WriteLine("The number of steps required: " + count);

                return true;
            }
            public void GetUserOutput(params int[] userArr)
            {
                // user list to preserve the correct order and values of user inputs.
                List<int> userList = new List<int>();

                // store missing values to be added back into InputArr.
                List<int> listOfMissingNumbers = new List<int>();

                // Check for duplicates from user inputs
                if (userArr.Length != userArr.Distinct().Count())
                {
                    // remove duplicates.
                    userArr = userArr.Distinct().ToArray();
                }

                // Check for missing numbers inside user's input.
                for (int i = 1; i <= 16; i++)
                {
                    if (!userArr.Contains(i)) listOfMissingNumbers.Add(i);
                }

                // Check for numbers larger than or equals to 1 and 15.
                foreach (int num in userArr)
                {
                    if (num >= 1 && num <= 16) userList.Add(num);
                    else Console.WriteLine($"{num} is removed, 1 <= num <= 16");
                }

                // Fill in the rest of the numbers into user list.
                if (listOfMissingNumbers.Count != 0)
                    foreach (int num in listOfMissingNumbers) userList.Add(num);


                // Set the first num and last num to default
                OutputArr[0] = 0;

                // Iterate and update InputArr.
                for (int i = 0; i < userList.Count; i++) OutputArr[i + 1] = userList[i];

                // print inputArr
                if (Test)
                {
                    foreach (var num in OutputArr) Console.Write($"[{num}] ");
                    Console.WriteLine();
                }
            }
            public void GetUserInput(params int[] userArr)
            {
                // user list to preserve the correct order and values of user inputs.
                List<int> userList = new List<int>();

                // store missing values to be added back into InputArr.
                List<int> listOfMissingNumbers = new List<int>();

                // Check for duplicates from user inputs
                if (userArr.Length != userArr.Distinct().Count())
                {
                    // remove duplicates.
                    userArr = userArr.Distinct().ToArray();
                }

                // Check for missing numbers inside user's input.
                for (int i = 1; i <= 16; i++)
                {
                    if (!userArr.Contains(i)) listOfMissingNumbers.Add(i);
                }

                // Check for numbers larger than or equals to 1 and 15.
                foreach (int num in userArr)
                {
                    if (num >= 1 && num <= 16) userList.Add(num);
                    else Console.WriteLine($"{num} is removed, 1 <= num <= 16");
                }

                // Fill in the rest of the numbers into user list.
                if (listOfMissingNumbers.Count != 0)
                    foreach (int num in listOfMissingNumbers) userList.Add(num);


                // Set the first num and last num to default
                InputArr[0] = 0;

                // Iterate and update InputArr.
                for (int i = 0; i < userList.Count; i++) InputArr[i + 1] = userList[i];

                // print inputArr
                if (Test)
                {
                    foreach (var num in InputArr) Console.Write($"[{num}] ");
                    Console.WriteLine();
                }
            }
            public void GenerateRandomInput()
            {
                Random random = new Random();

                // Set the array in order.
                for (int i = 0; i < InputArr.Length; i++) InputArr[i] = i;

                // Always leave 16 at the last.
                for (int i = InputArr.Length - 2; i >= 1; i--)
                {
                    // randomize input array.
                    // min >= 1 , max < i + 1
                    int x = random.Next(1, i + 1);

                    int temp = InputArr[i];
                    InputArr[i] = InputArr[x];
                    InputArr[x] = temp;
                }

                // dont print 0.
                for (int i = 1; i < InputArr.Length; i++)
                {
                    Console.Write($"[{InputArr[i]}]  ");
                    if (i % 4 == 0) Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
        #endregion

        #region Josephus Problem
        private static void JosephusProblemTest()
        {
            // The Josephus Problem - Numberphile
            // https://www.youtube.com/watch?v=uCsD3ZGzMgE
            
            JosephusProblem problem = new JosephusProblem();
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            problem.WinningPosition(410);
            stopwatch1.Stop();
            Console.WriteLine("Run time for algo 1: " + stopwatch1.Elapsed.TotalSeconds);

            // This algo is 3 times faster.
            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            problem.WinningPositionOptimised(410);
            stopwatch2.Stop();
            Console.WriteLine("Run time for algo 2: " + stopwatch2.Elapsed.TotalSeconds);
        }
        class JosephusProblem
        {
            private Node First;
            private Node Last;
            // Determine winning position optimised
            public int WinningPositionOptimised(int n)
            {
                // Check for values less than 1
                if (n <= 1)
                {
                    Console.WriteLine("Please enter a value larger than 1");
                    return 1;
                }

                // Setup Circular LinkedList
                for (int i = 1; i <= n; i++)
                    AddDataSet(i);

                int x = 0;

                while (true)
                {
                    if (n - Math.Pow(2,x) < 0)
                    {
                        x = x - 1;
                        break;
                    }

                    x++;
                }

                int remainder = n - (int)Math.Pow(2, x);

                // If remainder == 0, means it is a binary number.
                if (remainder == 0)
                {
                    // if remainder is zero, 1 position always wins.
                    Console.WriteLine($"The winning position is [1]");
                    return 1;
                }
                else
                {
                    Node current = First;

                    // Make i moves which is equavalent to remainder.
                    for (int i = 1; i <= remainder; i++) current = current.Next.Next;

                    Console.WriteLine($"The winning position is [{current.Data}]");
                    return current.Data;
                }
            }

            // Determine the winning position
            public int WinningPosition(int n)
            {
                // Check for values less than 1
                if (n <= 1)
                {
                    Console.WriteLine("Please enter a value larger than 1");
                    return 1;
                }

                // Setup Circular LinkedList
                for (int i = 1; i <= n; i++)
                    AddDataSet(i);

                // two pointers
                Node current = First.Next;
                Node winner = First;
                int visited = 0;

                while (true)
                {
                    // if current not visited.
                    if (current.Visited == false)
                    {
                        // set current to visited.
                        current.Visited = true;

                        // keep track of how many visited.
                        visited++;

                        // winner is found if left one to visit.
                        if (visited == n - 1) break;

                        // winner must always be on a unvisited node.
                        while (true)
                        {
                            // loop until you find an unvisited node.
                            winner = winner.Next;
                            if (winner.Visited == false) break;
                        }

                        // current must always be one node in front of winner.
                        current = winner.Next;
                    }
                    else
                    {
                        // loop until current finds another unvisited node.
                        current = current.Next;
                    }
                }


                Console.WriteLine($"The winining position is at: [{winner.Data}]");

                return winner.Data;
            }

            // Create a circular linked list.
            public void AddDataSet(params int[] dataSet)
            {
                for (int i = 0; i < dataSet.Length; i++)
                    AddNodeFromEnd(dataSet[i]);
            }
            private void AddNodeFromEnd(int data)
            {
                if (First == null)
                {
                    First = new Node(data);
                    Last = First;
                }
                else
                {
                    Node temp = new Node(data);
                    Last.Next = temp;
                    temp.Next = First;
                    Last = temp;
                }
            }
            public void Print()
            {
                Print(First);
                Console.WriteLine($"[{First.Data}] -> Loop");
            }
            private void Print(Node current)
            {
                Console.Write($"[{current.Data}] -> ");

                // Base case
                if (current == Last) return;

                Print(current.Next);
            }
        }

        class Node
        {
            public int Data { get; set; }
            public Node Next { get; set; }
            public bool Visited { get; set; }

            public Node(int data, bool visited = false)
            {
                Data = data;
                Visited = visited;
            }
        }
        #endregion
    }
}
