using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    class DynamicProgrammingApplication
    {
        public static void Main()
        {
            //LongestPalindromicTest();
            //PainterPartitionTest();
            //SolvingDynamicProgrammingProblems();
            //MaxGold();
            //CoinChangeTest();
            //FriendsPairingTest();
            //StairCaseTest();
            //CountDecoding();
            //JobSchedulingTest();
            //KeyPadProblem();

        }

        #region How to solve dynamic programming problems?
        // Essence to Dynamic Programming
        // Bases cases are the minimum parameter values.
        // Recursion is 2^n therefore there can only be 2 decision at each juncture.
        // Use previous calculated values to evaluate final results. (Optimal substructure)
        // Reduce paramenter to form smaller sub probalem (Overlapping subproblems)
        // Store calculated results to prevent recalculation.

        /*
         * 1) Identify if it is a DP problem
         * Maximizing or minimizing problems usually can be solved with DP
         * Generally, something that can be done recursively, which uses previous values
         * to build up to the final solution can be solved with dynamic programming.
         * 
         * 2) Deceide a state expression with least parameters
         * State: a set of parameters that can uniquely identify a certain position or standing
         * in a give problem.
         * 
         * 3) Formulate state relationship
         * Find the relation between previous states to reach the current state.
         * This is the most difficult part of DP.
         * 
         * 4) Do tabulation or memoization
         * 
         * 
         */

        private static void SolvingDynamicProgrammingProblems()
        {
            int[] numArr = { 1, 3, 5 };
            int sum = 6;
            int results2 = MaxWaysToComputeThreeNumbers(numArr, sum);
            int results = Solve(sum);
            Console.WriteLine($"The max ways to compute {numArr[0]}, {numArr[1]}, {numArr[2]} " +
                $"with the sum of {sum} is {results}");
        }

        /// <summary>
        /// Returns the maximum ways to compute 3 numbers
        /// </summary>
        /// <param name="numArr"> takes an array of 3 numbers </param>
        /// <returns> the max number of ways to compute 3 numbers with repetition </returns>
        private static int MaxWaysToComputeThreeNumbers(int[] numArr, int sum)
        {
            if (sum < 1) return 0;
            if (sum == 1) return 1;

            return MaxWaysToComputeThreeNumbers(numArr, sum - numArr[0])
                + MaxWaysToComputeThreeNumbers(numArr, sum - numArr[1])
                + MaxWaysToComputeThreeNumbers(numArr, sum - numArr[2]);
        }

        private static int Solve(int n)
        {
            // base case 
            if (n <= 0)
                return 0;
            if (n <= 1)
                return n;

            return Solve(n - 1) + Solve(n - 3) + Solve(n - 5);
        }
        #endregion

        #region Dynamic Programming Concepts

        // Simple procedure to solve DP problems.
        // Determine parameters
        // Determine base cases
        // Determine decision tree based on constraints.
        // Create optimal solution based on optimal substructure
        // Create overlapping subproblems by reducing arguments passed.
        // Add memoization or tabulation.

        // Practicing DP
        // Each DP problem use max 30-45 min to solve.
        // Following which take 20 min to understand the problem if you are unable to solve.
        // And, another 40 min to convert DP to tabulation and memoization.

        // Follow this procedure to solve DP
        // Bases cases are the minimum parameter values.
        // Recursion is 2^n therefore there can only be 2 decision at each juncture.
        // Use previous calculated values to evaluate final results. (Optimal substructure)
        // Reduce paramenter to form smaller sub probalem (Overlapping subproblems)
        // Store calculated results to prevent recalculation.

        // Essense of Dynamic Programming
        // Ask critical questions to form bases cases, then build if 1 then if 2 then if 3 .. so on ...
        // Most problems are SIMILAR just the decisions and constraints are DIFFERENT.
        // Therefore, to master DP. I only need to work on the decisions and contraints of each problem.
        // And, most of the time if you can solve 2 levels of DP + base condition, you solve the whole problem.
        // And, if the 3rd level is successful, the algorithm is completed.
        // To help with visualization, draw recursive tree or tabulation table.

        // Problem solving technique is general
        // What information do i have ?
        // What are my constraints ?
        // What am i optimizing ?
        // What are the decisions i have to make when optimizing e.g to include or to exlude?

        // ----------------------------------- Practice makes Perfect ----------------------------------- //

        // Essence to Dynamic Programming
        // Bases cases are the minimum parameter values.
        // Recursion is 2^n therefore there can only be 2 decision at each juncture.
        // Use previous calculated values to evaluate final results. (Optimal substructure)
        // Reduce paramenter to form smaller sub probalem (Overlapping subproblems)
        // Store calculated results to prevent recalculation.

        // The minimum values of the parameters make up the base cases.
        // understand time complexity. why 2^n because recursion is binary for 2 decisions.

        // Understand that for each subproblem in dynamic programming, we need to make choices.
        // And we either get the maximum or minimum for that choice.

        // The secret to learning anything is to give your best effort first.
        // Before looking up for the answer. This way the answer will stick better.

        // Overlapping sub problems - if something can be done recursively this criterion is met.
        // Optimal Substructure - if you need the previous values to calculate your final answer, this criterion is met.

        // Dont be afraid, to start, solve it normally without iteration or recursion.
        // just solve it normally.

        // Top down, memoization recursive approach vs
        // Bottom up, tabulation iterative approach

        // The key to dynamic programming problems is to identify
        // recursive SUB-Problems.

        // After solving recursive function, then apply memoization or tabulation
        // to improve the function's performance.

        // Another way of thinking of sub problems is a scaled down version of the big problem.
        // we can think of building it up from say (1,1) to (10,10), bottom up approach.
        // or another way you can think of DP is filling up a matrix or array by hand.

        // Overlapping sub problems are basically. functions that are called multiple time with the same input.
        // Optimal substructure is basically, to calculate current answer, you need the previous answer from the 
        // function.

        // so the two parts to Dynamic Programming will be:
        // 1. Identify the damn sub problem and solve it recursively first.
        // 2. Apply either Memoization or Tabulation technique to IMPROVE performance.

        // One good way of identifying subproblems.
        // Those that require you to ask questions like, to break or not to break. To include or not to include.
        // yes or not .. these are the kind of questions that lead to identifying sub-problems.

        // So the key to solving recursive function is to solve a single calculation FIRST. then make it recursive
        // or iterative. Basically, solve current function then ADD loops to the receipe.

        // To start practicing, start with identifying overlapping sub problems first.
        // Practice from psuedo code to main code recursively
        // then add memoization, and tabulation.
        // Understanding algorithm comes after that.

        // Very often DP requires build up a table of data. that relies on overlapping subproblems 
        // (calculating over the same area of the table) and 
        // optimal substructure (you need some previous value on the table) to fill the table
        // that is why storing repeating calling to memory will speed up process.
        // there is where memoization and tabulation will come in.
        // BUT, the jey initially is to find the recursive function for DP to work.

        // dynamic programming is usually two dimension.
        // always a decision to include or exclude.
        // when build DP function. dont think of recursion. 
        // Think of building a normal function to solve normal problems.
        // 3 steps approach to solving DP.
        // DP is about learning to think logically and how to solve problems
        // chronogically.
        // Then convert it to normal -> recursion -> memoization

        // how to solve normally ? 
        // just ask what if i have a string with 2 characters.
        // what if i have a string with 3 characters all different.
        // things like this, exactly how you normally solve problems.

        // in order to solve sub-problems, you need to identify all possible base cases scenario.
        // as all these scenario will build up the optimal sub structure.
        // The key to success is practice, practice and more practice.

        private static void DynamicProgrammingConcepts()
        {
            Console.WriteLine(FibMemoStepOne(10));
            Console.WriteLine(FibMemoStepTwo(3, new int[3 + 1]));
            Console.WriteLine(FibTab(10));
        }

        // first step to do Memoization.
        private static int FibMemoStepOne(int n)
        {
            // Basic of recursion, the base case.
            if (n <= 1) return n;

            // Create local variables, instead of lumping all together.
            int first = FibMemoStepOne(n - 2);
            int second = FibMemoStepOne(n - 1);

            // for memoization, store the result you want to find.
            // because you solve the BIG picture, that why recursion is Top-Down.
            int results = first + second;

            return results;
        }

        // Step 2 save the local variable into a reference type.
        private static int FibMemoStepTwo(int n, int[] memo)
        {
            // Check if data has NOT been calculated.
            if (memo[n] == default)
            {
                // special cases.
                if (n <= 1)
                    memo[n] = n;

                // if not the first two special cases then.. Save actual result to memory.
                else
                    // Top down approach because you start calculating with n then to 0.
                    // When you start memo[n] this is the final solution you want to find.
                    // so technically you are starting at the very top. that's why top-down.

                    // Optimal Substructure. in order to find Fib(3) = Fib(2) + Fib(1)
                    // Overlapping subproblems Fib(2) and Fib(1) called multiple times.
                    // because of this you can save the results into memory.
                    // it can used to find Optimal Solution (Max or Min)
                    memo[n] = FibMemoStepTwo(n - 1, memo) + FibMemoStepTwo(n - 2, memo);
            }

            // if data was calculated before, return result.
            return memo[n];
        }

        // tabulation structure and memoization structure is very similar.
        private static int FibTab(int n)
        {
            // slowly build up the results. table instead of memo.
            int[] table = new int[n + 1];

            // instead of recursion, we use a for loop to iterate.
            for (int i = 0; i < table.Length; i++)
            {
                // check if has NOT been calculated.
                if (table[i] == default)
                {
                    // save the special case
                    if (i <= 1)
                        table[i] = i;

                    // if not the first two special cases then.. Save actual result to memory.
                    else
                        // Bottom up approach because you start calculating from 0. and build up.
                        table[i] = table[i - 1] + table[i - 2];
                }
            }

            return table[n];
        }

        #endregion

        #region Mobile Numeric Keypad Problems
        // note : this is not a DP problem. it is a back-tracking problem
        private static void KeyPadProblem()
        {
            int numKeyPresses = 2;
            int results = KeyPadProblem(numKeyPresses, 0);
            Console.WriteLine($"The number of ways {numKeyPresses} keypresses can obtain is {results}");
        }

        // Return the number of possible key presses for N = x
        private static int KeyPadProblem(int key, int current)
        {
            // Determine parameters

            // Determine base cases
            if (key <= 0) return 0;

            // Determine decision tree based on constraints.
            int sum = 0;

            for (int i = current; i <= 9; i++)
            {
                if (i == 0) sum += 1 + KeyPadProblem(key - 1, 8);

                if (i == 1)
                    sum += 1 + KeyPadProblem(key - 1, 2) + KeyPadProblem(key - 1, 4);

                if (i == 2)
                    sum += 1 + KeyPadProblem(key - 1, 1) + KeyPadProblem(key - 1, 3) + KeyPadProblem(key - 1, 5);

                if (i == 3)
                    sum += 1 + KeyPadProblem(key - 1, 2) + KeyPadProblem(key - 1, 6);

                if (i == 4)
                    sum += 1 + KeyPadProblem(key - 1, 1) + KeyPadProblem(key - 1, 5) + KeyPadProblem(key - 1, 7);

                if (i == 5)
                    sum += 1 + KeyPadProblem(key - 1, 2) + KeyPadProblem(key - 1, 4)
                        + KeyPadProblem(key - 1, 6) + KeyPadProblem(key - 1, 8);

                if (i == 6)
                    sum += 1 + KeyPadProblem(key - 1, 3) + KeyPadProblem(key - 1, 5) + KeyPadProblem(key - 1, 9);

                if (i == 7)
                    sum += 1 + KeyPadProblem(key - 1, 4) + KeyPadProblem(key - 1, 8);

                if (i == 8)
                    sum += 1 + KeyPadProblem(key - 1, 5) + KeyPadProblem(key - 1, 7)
                        + KeyPadProblem(key - 1, 9) + KeyPadProblem(key - 1, 0);

                if (i == 9)
                    sum += 1 + KeyPadProblem(key - 1, 6) + KeyPadProblem(key - 1, 8);
            }

            // Create optimal solution based on optimal substructure
            // Create overlapping subproblems by reducing arguments passed.
            // Add memoization or tabulation.

            return sum;
        }
        #endregion

        #region Weighted Job Scheduling

        private static void JobSchedulingTest()
        {
            Jobs[] jobs =
            {
                new Jobs(1,2,50),
                new Jobs(3,5,20),
                new Jobs(2,100,200),
                new Jobs(6,19,100)
            };

            Array.Sort(jobs);

            int maxProfits = JobScheduling(jobs, jobs.Length);
            Console.WriteLine($"The maximum profits for {jobs.Length} jobs is {maxProfits}");
        }

        private static int NonConflict(Jobs[] jobs, int i)
        {
            for (int j = i - 1; j >= 0; j--)
            {
                if (jobs[j].EndTime <= jobs[i - 1].StartTime) return j;
            }
            return -1;
        }

        private static int JobScheduling(Jobs[] jobs, int n)
        {
            if (n == 1) return jobs[n - 1].Profits;

            int acceptJob = jobs[n - 1].Profits;
            int i = NonConflict(jobs, n);

            if (i != -1) acceptJob = JobScheduling(jobs, i + 1);

            int rejectJob = JobScheduling(jobs, n - 1);

            return Math.Max(acceptJob, rejectJob);
        }

        struct Jobs : IComparable<Jobs>
        {
            public int StartTime { get; set; }
            public int EndTime { get; set; }
            public int Profits { get; set; }
            public Jobs(int start, int end, int profits)
            {
                StartTime = start;
                EndTime = end;
                Profits = profits;
            }

            public int CompareTo(Jobs job)
            {
                if (this.StartTime > job.StartTime) return 1;
                if (this.StartTime < job.StartTime) return -1;
                return 0;
            }

            public override string ToString()
            {
                return $"The current job start time is {StartTime}, " +
                    $"end time is {EndTime}, with profits of {Profits}";
            }
        }
        #endregion

        #region Painter Partition Problems
        private static void PainterPartitionTest()
        {
            int[] board = { 40, 10, 20, 30 };
            int results = PainterPartition(board, 3, 0, board.Length - 1);
            int results2 = PainterPartitionBetter(board, board.Length, 2);
            Console.WriteLine($"The minimum time required to paint the board is {results} hrs");
            Console.WriteLine($"The minimum time required to paint the board is {results2} hrs");
            PainterPartitionMemo(board, 2);
        }
        private static void PainterPartitionMemo(int[] board, int painter)
        {
            int[,] memo = new int[painter + 1, board.Length + 1];
            for (int i = 0; i <= painter; i++)
            {
                for (int j = 0; j <= board.Length; j++) memo[i, j] = -1;
            }

            int results = PainterPartitionMemo(board, board.Length, painter, memo);
            Console.WriteLine($"The minimum time required to paint the board is {results} hrs");
        }
        private static int PainterPartitionMemo(int[] board, int length, int painter, int[,] memo)
        {
            if (memo[painter, length] == -1)
            {
                if (painter == 1) memo[painter, length] = Sum(board, 0, length - 1);
                else

                if (length == 1) memo[painter, length] = board[0];
                else
                {
                    memo[painter, length] = int.MaxValue;

                    for (int i = 1; i <= length; i++)
                    {
                        int nextPainter = PainterPartitionMemo(board, i, painter - 1, memo);
                        int currentPainter = Sum(board, i, length - 1);
                        memo[painter, length] =
                            Math.Min(memo[painter, length], Math.Max(nextPainter, currentPainter));
                    }
                }
            }

            return memo[painter, length];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"> dimensions of the board </param>
        /// <param name="length"> length of the board </param>
        /// <param name="painters"> number of painters </param>
        /// <returns></returns>
        private static int PainterPartitionBetter(int[] board, int length, int painters)
        {
            // base cases  
            if (painters == 1) return Sum(board, 0, length - 1);

            // board has only 1 length
            if (length == 1) return board[0];

            // you will need to set to max value if you were to use Math.Min
            int best = int.MaxValue;

            // find minimum of all possible maximum 
            // k-1 partitions to the left of arr[i], 
            // with i elements, put k-1 th divider  
            // between arr[i-1] & arr[i] to get k-th  
            // partition 
            for (int i = 1; i <= length; i++)
            {
                // getting the yes / no statement problem is the key to solving DP. 
                int nextPainter = PainterPartitionBetter(board, i, painters - 1);
                int currentPainter = Sum(board, i, length - 1);

                best = Math.Min(best, Math.Max(nextPainter, currentPainter));
            }

            return best;
        }

        /// <summary>
        /// Minimum amount of time required to paint the entire board.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="painters"></param>
        /// <returns></returns>
        private static int PainterPartition(int[] board, int painters, int start, int end)
        {
            // Find where to put in the recursive function LAST.
            // Work out the algorithm first - starting with base cases.

            // there is no more elements left to iterate
            if (start >= end) return 0;

            // what is the total time to paint the board ?
            int timeNeeded = Sum(board, start, end);

            // if time = 0
            if (timeNeeded == 0) return 0;

            // if there are no painters
            if (painters == 0) return 0;

            // if there is only one painter
            if (painters == 1) return timeNeeded;

            // After considering all cases.
            int timePerPainter = timeNeeded / painters;

            // return the minimum time required to paint the board.
            int ret = 0;

            // return value
            int minTimeRequired = 0;

            // otherwise
            for (int i = start; i <= end; i++)
            {
                minTimeRequired += board[i];

                if (minTimeRequired >= timePerPainter && i <= end)
                {
                    ret = Math.Max(minTimeRequired, PainterPartition(board, painters - 1, i + 1, end));
                    break;
                }
            }

            return ret;
        }

        private static int Sum(int[] numArr, int start, int end)
        {
            int sum = 0;

            for (int i = start; i <= end; i++) sum += numArr[i];
            return sum;
        }
        #endregion

        #region Longest Palindromic Subsequence

        private static void LongestPalindromicTest()
        {
            string test = "GeeksForGeeks";
            char[] charArr = test.ToLower().ToCharArray();

            int results = LongestPalindromic(charArr, 0, charArr.Length - 1);
            int results2 = LongestPalinMemoization(charArr);
            int results3 = LongestPalinTabulation(charArr);

            Console.WriteLine($"The longest palindromic for {test} is {results}");
            Console.WriteLine($"The longest palindromic for {test} is {results2}");
            Console.WriteLine($"The longest palindromic for {test} is {results3}");
        }

        /// <summary>
        /// Practice convert Pseudo Code to Code first.
        /// </summary>
        /// <param name="charArr"> the sub-problem is between i and j </param>
        /// <param name="i"> starting index </param>
        /// <param name="j"> ending index </param>
        /// <returns></returns>
        private static int LongestPalindromic(char[] charArr, int i, int j)
        {
            // Every single character is a palindrome of length 1, needs to be odd.
            if (i == j) return 1;

            // if there are only 2 characters and both are the same
            if (j == i + 1 && charArr[i] == charArr[j]) return 2;

            // if there are more than 2 characters, and first and last characters are the same
            if (charArr[i] == charArr[j]) return LongestPalindromic(charArr, i + 1, j - 1) + 2;

            // If first and last characters are not the same
            int next = LongestPalindromic(charArr, i + 1, j);
            int prev = LongestPalindromic(charArr, i, j - 1);
            int results = Math.Max(next, prev);
            return results;
        }
        private static int LongestPalinMemoization(char[] charArr)
        {
            int[,] memo = new int[charArr.Length + 1, charArr.Length + 1];
            for (int i = 0; i <= charArr.Length; i++)
            {
                for (int j = 0; j <= charArr.Length; j++)
                {
                    memo[i, j] = -1;
                }
            }

            int length = LongestPalinMemoization(memo, charArr, 0, charArr.Length - 1);

            return length;
        }
        private static int LongestPalinMemoization(int[,] memo, char[] charArr, int i, int j)
        {
            if (memo[i, j] == -1)
            {
                if (i == j)
                {
                    memo[i, j] = 1;
                }
                else if (j == i + 1 && charArr[i] == charArr[j])
                {
                    memo[i, j] = 2;
                }
                else if (charArr[i] == charArr[j])
                {
                    memo[i, j] = LongestPalinMemoization(memo, charArr, i + 1, j - 1) + 2;
                }
                else
                {
                    int next = LongestPalinMemoization(memo, charArr, i + 1, j);
                    int prev = LongestPalinMemoization(memo, charArr, i, j - 1);
                    memo[i, j] = Math.Max(next, prev);
                }
            }

            return memo[i, j];
        }

        private static int LongestPalinTabulation(char[] charArr)
        {
            int[,] table = new int[charArr.Length + 1, charArr.Length];

            // Fill all base cases first.
            for (int i = 0; i <= charArr.Length; i++) table[i, i] = 1;

            for (int i = 2; i <= charArr.Length; i++)
            {
                for (int j = 0; j <= charArr.Length - i + 1; j++)
                {
                    if (j == i + 1 && charArr[j] == charArr[i])
                    {
                        table[i, j] = 2;
                    }
                    else if (charArr[i] == charArr[j])
                    {
                        table[i, j] = table[i + 1, j - 1] + 2;
                    }
                    else
                    {
                        int next = table[i + 1, j];
                        int prev = table[i, j - 1];
                        table[i, j] = Math.Max(next, prev);
                    }
                }
            }

            return table[0, charArr.Length - 1];
        }

        #endregion

        #region Matrix Chain Multiplication

        #endregion

        #region Egg Dropping Puzzle
        // Egg Dropping Puzzle | DP-11
        // https://www.geeksforgeeks.org/egg-dropping-puzzle-dp-11/?ref=lbp
        // Optimal Solution: Solve for maximum number of steps. //
        // Understanding the problem is the problem.
        // Changing to dynamic from recursion is simple.
        // difficulty is getting recursive function.
        // another clue is consider all exhausive options.

        private static void EggDropTest()
        {
            // Overlapping sub problems (fib series ) vs non-overlapping sub problems (mergesort)
            // Calling with the same arguments over and over again.

            // Optimal solution can be constructed from optimal solutions of its A->B->C & A->B & A->.
            // Using memory to save data in order to save time later.
            // KnapSackTest();
            // DynamicProgrammingConcepts();
            int eggs = 3;
            int floors = 6;
            EggDropOptimized(eggs, floors);
            DropEggTabulation(eggs, floors);
            EggDrop(eggs, floors, "");
        }
        private static void EggDrop(int eggs, int floors, string message = "")
        {
            // use message as a local variable.
            message = $"The minimum number of attempts " +
                $"for {eggs} eggs and {floors} floors is : ";

            // return the minimum number of attemps.
            int attempts = EggDrop(eggs, floors);

            // Print answer.
            Console.WriteLine(message + " " + attempts);
        }

        /// <summary>
        /// Returns the minimum number of attempts needed,
        /// to find the highest floor before the egg breaks.
        /// </summary>
        /// <param name="totalEggs"></param>
        /// <param name="totalFloors"></param>
        /// <returns></returns>
        private static int EggDrop(int totalEggs, int totalFloors)
        {
            // If there is only 1 floor, you only need one attempt
            if (totalFloors == 1 || totalFloors == 0) return totalFloors;

            // if you have only one egg, you have to take k floors attempt.
            if (totalEggs == 1) return totalFloors;

            int min = int.MaxValue;

            // Start from first floor. base floor is floor 0 or ground floor.
            for (int x = 1; x <= totalFloors; x++)
            {
                // total floors if eggs break x -1 
                int eggBreaks = EggDrop(totalEggs - 1, x - 1);

                // total floors left if eggsurvives k - x
                int eggSurvives = EggDrop(totalEggs, totalFloors - x);

                int results = Math.Max(eggBreaks, eggSurvives);

                if (results < min) min = results;
            }

            // Note: We are not finding the pivotal floor, we are finding the min attempts.
            // to 100% gurantee that X number of attemps, we can find the pivotal floor.
            // dont forget to add one. WHY ? because the floor before the split has not be accounted for.
            return min + 1;
        }
        private static void EggDropOptimized(int eggs, int floors)
        {
            // need to account for 0, and zero-indexed
            // int[rows,columns]
            int[,] memo = new int[eggs + 1, floors + 1];

            // Rows
            for (int i = 0; i < eggs + 1; i++)
            {
                // Columns
                for (int j = 0; j < floors + 1; j++)
                    memo[i, j] = -1;
            }

            int attempts = EggDropOptimized(eggs, floors, memo);
            Console.WriteLine($"The minimum number of attempts would be: " + attempts);
        }

        private static int EggDropOptimized(int eggs, int floors, int[,] memo)
        {
            if (memo[eggs, floors] == -1)
            {
                // first base case
                if (floors == 1 || floors == 0) memo[eggs, floors] = floors;

                // second base case
                else if (eggs == 1) memo[eggs, floors] = floors;

                // third base case
                else if (eggs == 0) memo[eggs, floors] = eggs;

                else
                {
                    memo[eggs, floors] = int.MaxValue;

                    // very similar to recursive function. just slight chances.
                    for (int i = 1; i <= floors; i++)
                    {
                        // egg breaks
                        int eggBreaks = EggDropOptimized(eggs - 1, i - 1, memo);

                        // egg survives
                        int eggSurvives = EggDropOptimized(eggs, floors - i, memo);

                        // get the results, so long as there is value,
                        // you wont get the exception "use of unassigned local variable." 
                        // you need to choose one of two so need to add back + 1
                        int results = Math.Max(eggBreaks, eggSurvives) + 1;

                        // smallest number of attempts needed to gurantee results.
                        if (results < memo[eggs, floors]) memo[eggs, floors] = results;
                    }
                }
            }

            // return data from table.
            return memo[eggs, floors];
        }

        private static int DropEggTabulation(int eggs, int floors)
        {
            // Bottom up approach is filling up the table.
            // make the assumption that the egg does not necessaily needs to break at all.
            int[,] table = new int[eggs + 1, floors + 1];

            // set all table values to -1
            for (int i = 0; i <= eggs; i++)
            {
                // same as <= floor
                for (int j = 0; j < floors + 1; j++) table[i, j] = -1;
            }

            // set the base cases
            // i and j are local variables inside the for loop
            for (int i = 0; i <= eggs; i++)
            {
                for (int j = 0; j <= floors; j++)
                {
                    // no eggs or no floors.
                    if (i == 0 || j == 0) table[i, j] = 0;
                    else
                    // only one egg
                    if (i == 1) table[i, j] = j;
                    else
                    // only one floor
                    if (j == 1) table[i, j] = 1;
                    else
                        table[i, j] = -1;
                }
            }

            // rows: eggs
            for (int i = 2; i <= eggs; i++)
            {
                // columns: floors
                for (int j = 2; j <= floors; j++)
                {
                    table[i, j] = int.MaxValue;

                    // floors ?
                    for (int k = 1; k <= j; k++)
                    {
                        // j - k is solve the remaining number of floors.
                        // for each floor you must consider the following options.
                        int results = Math.Max(table[i - 1, k - 1], table[i, j - k]) + 1;
                        table[i, j] = Math.Min(table[i, j], results);
                    }
                }
            }

            Console.WriteLine($"The minimum number of attempts " +
                $"for {eggs} eggs and {floors} floors is : {table[eggs, floors]}");

            // return the final value of the table.
            return table[eggs, floors];
        }

        #endregion

        #region Recursion Test
        // no need for ref keyword, as table is a reference type.
        private static void Table(int n, int[] table)
        {
            if (n == 0) return;

            table[n - 1] = n;
            Table(n - 1, table);

            // output 1,2,3,4
        }

        private static void Print(int n)
        {
            if (n == 0) return;

            Console.Write($"[{n}] -> ");
            Print(n - 1);
        }

        private static void PrintReverse(int n)
        {
            if (n == 0) return;
            //PrintReverse(n - 1);

            // note that n = n - 1, the value of n will be changed.
            PrintReverse(--n);
            Console.Write($"[{n}] -> ");
        }

        private static void TestDecrement()
        {
            int a = 10;

            Console.WriteLine("Using a - 1");
            for (int x = 0; x < 5; x++)
            {
                Console.WriteLine(a - 1);
            }

            Console.WriteLine();

            int b = 10;

            Console.WriteLine("Using --b");
            for (int x = 0; x < 5; x++)
            {
                Console.WriteLine(--b);
            }
        }
        #endregion

        #region Stair Case Problem
        // can only take either 1 or 2 steps.
        // how many ways we can take the above to climb a stairs of x steps.

        private static void StairCaseTest()
        {
            int stairs = 6;
            int results = StairCaseProblem(stairs);
            int results2 = StairCaseProblemTab(stairs);

            Console.WriteLine($"The total number of ways to climb {stairs} stairs is {results}");
            Console.WriteLine($"The total number of ways to climb {stairs} stairs is {results2}");
            StairCaseProblemMemo();
        }

        private static int StairCaseProblem(int stairs)
        {
            // reach the top of the stairs
            if (stairs == 0) return 1;

            // if negative, mean cannot take that step.
            if (stairs < 0) return 0;

            // choices either take one step, or take 2 steps
            int oneStep = StairCaseProblem(stairs - 1);
            int twoStep = StairCaseProblem(stairs - 2);

            return oneStep + twoStep;
        }

        private static void StairCaseProblemMemo()
        {
            int stairs = 6;

            // can use array instead of multiD array, as only 1 variable stairs.
            int[] memo = new int[stairs + 1];

            for (int i = 0; i < memo.Length; i++) memo[i] = -1;

            int results = StairCaseProblemMemo(memo, stairs);
            Console.WriteLine($"The total number of ways to climb {stairs} stairs is {results}");

        }

        private static int StairCaseProblemMemo(int[] memo, int stairs)
        {
            // if results not cache, then do calculations.
            if (memo[stairs] == -1)
            {
                if (stairs == 0) return 1;

                int oneStep = (stairs - 1 < 0) ? 0 : StairCaseProblemMemo(memo, stairs - 1);
                int twoStep = (stairs - 2 < 0) ? 0 : StairCaseProblemMemo(memo, stairs - 2);

                memo[stairs] = oneStep + twoStep;
            }

            return memo[stairs];
        }

        private static int StairCaseProblemTab(int stairs)
        {
            // 2 steps - 0 , 1 , 2
            int[] table = new int[stairs + 1];

            // base cases, not so intuitive.
            table[0] = table[1] = 1;

            for (int i = 2; i < table.Length; i++)
                table[i] = table[i - 1] + table[i - 2];

            return table[stairs];
        }
        #endregion

        #region Decode Ways
        // this is the same as the change coin problem.
        // Decision is made to either look for 1 or 2 letters why ?
        // maxium is 26 -> z 1 -> a outside scope return 0.
        static void CountDecoding(string numbers = "123456")
        {
            char[] digits = numbers.ToCharArray();
            int results = CountDecoding(digits, digits.Length);

            Console.WriteLine($"The number of ways {numbers} can be decoded is {results}");
        }
        static int CountDecoding(char[] digits, int n)
        {

            // base cases 
            if (n == 0 || n == 1)
                return 1;

            // Initialize count 
            int count = 0;

            // If the last digit is not 0, then  
            // last digit must add to 
            // the number of words 
            if (digits[n - 1] > '0')
                count = CountDecoding(digits, n - 1);

            // If the last two digits form a number 
            // smaller than or equal to 26, then  
            // consider last two digits and recur 
            if (digits[n - 2] == '1' ||
               (digits[n - 2] == '2' && digits[n - 1] < '7'))
                count += CountDecoding(digits, n - 2);

            return count;
        }
        #endregion

        #region Knapsack Problem 
        class Product
        {
            public int Weight { get; set; }
            public double Price { get; set; }
            public string Name { get; set; }
            public Product(int weight, double price, string name)
            {
                Name = name;
                Weight = weight;
                Price = price;
            }
        }

        // What is dynamic programming ?
        // is an algorithmic paradigm that solves a given complex problem by 
        // breaking it into sub problems and STORING the results of subproblems
        // to avoid computing the same results again.

        // What is optimal Sub-Structure ?
        // That means A->B->C->D, in order to solve D. You need to solve A->B->C.
        // and in order to solve C. you need to solve A->B, etc..
        // which is a build up in order to solve D.
        // Floyd-Warshall / Bellman-Ford

        // What is Overlapping SubProblems ?
        // Calling fib(0) and fib(1) repeatedly. same function call with same arguments.

        private static void KnapSackTest()
        {
            Product[] products =
            {
                new Product(10, 60, "Rice Sack"),
                new Product(30, 120, "Fridge"),
                new Product(20, 100, "Printer"),
                new Product(40, 180, "Generator")
            };

            KnapSack(50, products);
            KnapSackTabulation(50, products);
            KnapSackMemoization(50, products);
        }
        private static double Max(double a, double b) => a > b ? a : b;

        // Using Memoization technique.
        private static double KnapSack(int maxWeight, Product[] products)
        {
            double maxValue = KnapSack(maxWeight, products, products.Length);
            Console.WriteLine($"The max value the knapsack can hold is: {maxValue:C2}");
            return maxValue;
        }
        private static double KnapSack(int maxWeight, Product[] products, int n)
        {
            if (n == 0 || maxWeight == 0)
                return 0;

            if (products[n - 1].Weight > maxWeight)
                return KnapSack(maxWeight, products, n - 1);

            else return Max
            (
                // if we change n - 1 to --n, see recursive test. we need to maintain state of n.
                products[n - 1].Price + KnapSack(maxWeight - products[n - 1].Weight, products, n - 1),
                KnapSack(maxWeight, products, n - 1)
            );
        }

        private static double KnapSackMemoization(int maxWeight, Product[] products)
        {
            int[,] table = new int[products.Length + 1, maxWeight + 1];

            for (int i = 0; i < products.Length + 1; i++)
            {
                for (int j = 0; j < maxWeight + 1; j++) table[i, j] = -1;
            }

            double maxProfit = KnapSackMemoization(maxWeight, products, products.Length - 1, table);

            // print out
            for (int i = 0; i < products.Length + 1; i++)
            {
                for (int j = 0; j < maxWeight + 1; j += 10) Console.Write(table[i, j] + " ");
                Console.WriteLine();
            }

            Console.WriteLine($"\nThe max value the knapsack can hold is: {maxProfit:C2}");
            return maxProfit;
        }

        // Bottom up approach.
        private static double KnapSackMemoization(int maxWeight, Product[] products, int i, int[,] table)
        {
            // start from last product to first.
            if (i < 0) return 0;

            // Set all values in table to -1 first, if table has value, return table value.
            if (table[i, maxWeight] != -1) return table[i, maxWeight];


            if (products[i].Weight > maxWeight)
            {
                // move the pointer down the table.
                table[i, maxWeight] = (int)KnapSackMemoization(maxWeight, products, i - 1, table);
                return table[i, maxWeight];
            }
            else
            {
                double price1 = products[i].Price
                    + KnapSackMemoization(maxWeight - products[i].Weight, products, i - 1, table);

                double price2 = KnapSackMemoization(maxWeight, products, i - 1, table);

                table[i, maxWeight] = (int)Max(price1, price2);

                return table[i, maxWeight];
            }
        }

        private static double KnapSackTabulation(int maxWeight, Product[] products)
        {
            // 0/1 Knapack Problem solved using dynamic programming tabulation method.
            // https://www.youtube.com/watch?v=nLmhmB6NzcM&t=1115s
            // Question: Must the product list be sorted? If yes, in what way must it be sorted ?
            // Order does not matter.

            Console.WriteLine("KnapSack Tabulation Method.");
            int n = products.Length;

            // you can use local variables for forloop
            int i, w;

            // Number of products vs Weight Scale
            // We are not using the first row of the able, so set it all to zero.
            // Table stores the object price.
            int[,] table = new int[n + 1, maxWeight + 1];

            // for rows will be each product.
            for (i = 0; i <= n; i++)
            {
                // Column: From 0 to max weight
                for (w = 0; w <= maxWeight; w++)
                {
                    // Not using first row of table && first column.
                    // Max weight = 0, no objects. Product weight 0, no object.
                    if (i == 0 || w == 0) table[i, w] = 0;

                    // if current product weight, is less than current weight capacity.
                    else if (products[i - 1].Weight <= w)
                        table[i, w] = (int)Math.Max(

                            // select product's price + previous product weight.
                            // Overlapping sub problems, solving the same sub problem with same arguments.
                            products[i - 1].Price + table[i - 1, w - products[i - 1].Weight],

                            // refers to the row before, with the same capacity that i am calculating.
                            table[i - 1, w]);

                    else table[i, w] = table[i - 1, w];
                }
            }

            // return product names.
            List<Product> selectedProducts = new List<Product>();

            // max profits
            double maxProfit = table[n, maxWeight];
            double balanceProfit = maxProfit;

            for (int x = n; x >= 1; x--)
            {
                // if the max weight for current combination is greater than previous combination.
                if (table[x, maxWeight] > table[x - 1, maxWeight] &&
                    balanceProfit - products[x - 1].Price >= 0)
                {
                    selectedProducts.Add(products[x - 1]);
                    balanceProfit -= products[x - 1].Price;
                }

                // if no profits left, break out of loop.
                if (balanceProfit <= 0) break;
            }

            Console.WriteLine($"\nList of products with the highest value for {maxWeight}kg sack.");
            foreach (Product product in selectedProducts) Console.WriteLine(product.Name);

            // table[n, maxWeight] is n - 1, max weight for 1 product ... n = 3, max weight for 3 products.
            Console.WriteLine($"\nThe max value the knapsack can hold is: {maxProfit:C2}");

            // in order to build up to this result is Optimal sub-structure.
            return maxProfit;
        }
        #endregion

        #region Fibonacci Series
        private static void Fibonacci_Test()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i <= 50; i++)
            {
                Console.Write(Fib_Tabulation(i) + ", ");
            }
            Console.WriteLine();
            stopwatch.Stop();
            Console.WriteLine("Time taken in sec: " + stopwatch.Elapsed.TotalSeconds);

        }
        private static int Fibonacci(int n)
        {
            if (n == 0) return 0;
            if (n == 1 || n == 2) return 1;
            return Fibonacci(n - 2) + Fibonacci(n - 1);
        }
        private static int Fib_Tabulation(int n)
        {
            // Bottom up approach.
            // https://www.quora.com/What-is-the-difference-between-bottom-up-and-top-down-dynamic-programming-method
            // why n + 1? the last n position will be stored in table[n].
            // and table is zero index.

            if (n == 0) return 0;

            int[] table = new int[n + 1];
            table[0] = 0;
            table[1] = 1;

            // Bottom up approach
            // build up the solution first, usually use with iteration
            for (int i = 2; i <= n; i++)
            {
                table[i] = table[i - 1] + table[i - 2];
            }

            return table[n];
        }
        private static int Fib_Memoization(int n)
        {
            return Fib_Memoization(n, new int[n + 1]);
        }
        private static int Fib_Memoization(int n, int[] memo)
        {
            // Top down approach / recursive approach
            // break a large problem into multiple sub-problems
            if (memo[n] == 0)
            {
                if (n < 2)
                {
                    memo[n] = n;
                }
                else
                {
                    // Memoization, usually used with recursion.
                    int left = Fib_Memoization(n - 1, memo);
                    int right = Fib_Memoization(n - 2, memo);
                    memo[n] = left + right;
                }
            }

            return memo[n];
        }
        #endregion

        #region Gold Mine Problem
        private static void MaxGold()
        {
            int[,] gold = new int[,]
            {
                {1, 3, 1, 5},
                {2, 2, 4, 1},
                {5, 0, 2, 3},
                {0, 6, 1, 2}
            };

            int[,] gold2 = new int[,]
            {
                {10, 33, 13, 15},
                {22, 21, 04, 01},
                {05, 00, 02, 03},
                {00, 06, 14, 02}
            };

            int[,] gold3 = new int[,]
            {
                {10, 33},
                {22, 21}
            };
            int m = 4, n = 4;
            Console.WriteLine($"The maximum gold the miner can get is {MaxGold(gold, m, n)}");
            Console.WriteLine($"The maximum gold the miner can get is {GetMaxGold(gold3, 0, 0)}");
        }

        private static int GetMaxGold(int[,] gold, int row, int col)
        {
            // base cases
            if (row == -1 || row >= gold.GetLength(0) || col >= gold.GetLength(1)) return 0;

            int results = 0;

            for (int i = row; i < gold.GetLength(0); i++)
            {
                int right = GetMaxGold(gold, i, col + 1);
                int rightUp = GetMaxGold(gold, i - 1, col + 1);
                int rightDown = GetMaxGold(gold, i + 1, col + 1);

                // unlike other problems, we have 3 choices, instead of 2.
                // for each sub problem, we need to make 3 choices here.
                results = Math.Max(results, gold[i, col] + Math.Max(right, Math.Max(rightUp, rightDown)));
            }

            return results;
        }

        private static int MaxGold(int[,] gold, int m, int n)
        {

            // Create a table for storing intermediate  
            // results and initialize all cells to 0.  
            // The first row of goldMineTable gives 
            // the maximum gold that the miner  
            // can collect when starts that row  
            int[,] goldTable = new int[m, n];

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    goldTable[i, j] = 0;

            for (int col = n - 1; col >= 0; col--)
            {
                for (int row = 0; row < m; row++)
                {
                    // Gold collected on going to  
                    // the cell on the right(->)  
                    int right = (col == n - 1) ? 0 :
                                goldTable[row, col + 1];

                    // Gold collected on going to  
                    // the cell to right up (/)  
                    int right_up = (row == 0 || col == n - 1)
                                ? 0 : goldTable[row - 1, col + 1];

                    // Gold collected on going  
                    // to the cell to right down (\)  
                    int right_down = (row == m - 1 || col == n - 1)
                                    ? 0 : goldTable[row + 1, col + 1];

                    // Max gold collected from taking  
                    // either of the above 3 paths  
                    goldTable[row, col] = gold[row, col] +
                                    Math.Max(right, Math.Max(right_up,
                                                        right_down));
                }
            }

            // The max amount of gold collected will be the max  
            // value in first column of all rows  
            int res = goldTable[0, 0];
            for (int i = 1; i < m; i++)
                res = Math.Max(res, goldTable[i, 0]);
            return res;
        }
        #endregion

        #region Friends Pairing Problem
        // Given n friends, each can either be paired or remain single.
        // Each friend can only be paired once.
        // Total number of ways friends can be paired
        // This approach is very similar to Coin Change.
        // We consider the first subproblem of only 1 friend, then 2 friend ... 
        // Return the number of ways, we can pair n friends.

        private static void FriendsPairingTest()
        {
            int friends = 4;
            int results = FriendsPairing(friends);
            int results2 = FriendsPairingTable(friends);

            Console.WriteLine($"The number of ways {friends} friends can be arranged is {results}");
            Console.WriteLine($"The number of ways {friends} friends can be arranged is {results2}");
        }

        private static int FriendsPairing(int friends)
        {
            // if friends = 0, all friends are already paired or remain single.
            if (friends == 0) return 1;

            // you can't pair up when friends = 1
            if (friends < 0) return 0;

            // ---------- Decisions to the sub-problems made here ---------- //

            // remain single
            int single = FriendsPairing(friends - 1);

            // be paired up, friends - 1 is because he cannot pair himself with himself.
            // or C can either pair with A or B, if there are only three friends A B C
            // cases are mutually exclusive
            int paired = FriendsPairing(friends - 2) * (friends - 1);

            return single + paired;
        }

        private static int FriendsPairingTable(int friends)
        {
            int[] tab = new int[friends + 1];

            // Bottom up approach.
            for (int i = 0; i < tab.Length; i++)
            {
                if (i <= 2)
                {
                    tab[i] = i;
                }
                else
                {
                    tab[i] = tab[i - 1] + (i - 1) * tab[i - 2];
                }
            }

            return tab[friends];
        }
        #endregion

        #region Coin Change
        private static void CoinChangeTest()
        {
            int[] coinSet = { 1, 2 };
            int sum = 4;
            int results = CoinChange(coinSet, coinSet.Length, sum);
            int results2 = CoinChangeTab(coinSet, sum);

            Console.WriteLine($"The number of sets which sum = {sum} is {results}");
            Console.WriteLine($"The number of sets which sum = {sum} is {results2}");
            CoinChangeMemo(coinSet, sum);
        }

        // ----------------------------- Top-Down Approach ----------------------------- //
        // why called top down approach, because we are calling the final answer first   //
        // and we do this with a recursive tree, which is depth first seach              //
        private static int CoinChange(int[] coinSet, int selection, int sum)
        {
            // Found one set.
            if (sum == 0) return 1;

            // Exceeds sum value, return 0 try again.
            if (sum < 0) return 0;

            // no coins left to select, and the sum has value.
            if (sum >= 1 && selection <= 0) return 0;

            // dont select.
            int exclude = CoinChange(coinSet, selection - 1, sum);

            // select and subtract from sum.
            int include = CoinChange(coinSet, selection, sum - coinSet[selection - 1]);

            return exclude + include;
        }

        private static void CoinChangeMemo(int[] coinSet, int sum)
        {
            int[] memo = new int[sum + 1];
            for (int i = 0; i < memo.Length; i++) memo[i] = -1;

            int results = CoinChangeMemo(coinSet, coinSet.Length, sum, memo);
            Console.WriteLine($"The number of sets which sum = {sum} is {results}");
        }

        private static int CoinChangeMemo(int[] coinSet, int selection, int sum, int[] memo)
        {
            if (memo[sum] == -1)
            {
                // Found one set.
                if (sum == 0) return 1;

                // Exceeds sum value, return 0 try again.
                if (sum < 0) return 0;

                // no coins left to select, and the sum has value.
                if (sum >= 1 && selection <= 0) return 0;

                // dont select.
                int exclude = CoinChangeMemo(coinSet, selection - 1, sum, memo);

                // select and subtract from sum.
                int include = CoinChangeMemo(coinSet, selection, sum - coinSet[selection - 1], memo);

                memo[sum] = exclude + include;
            }

            return memo[sum];
        }

        // ----------------------------- Bottom-Up Approach ----------------------------- //
        // why called bottom up approach is we solve sum = 0 first before solving sum = n //
        // we do this with a table or matrix table to get to the final value.             //
        private static int CoinChangeTab(int[] coinSet, int sum)
        {
            // index used is the sum.
            int[] table = new int[sum + 1];

            // why if sum is zero return 1 ?
            table[0] = 1;

            // first select the coin.
            for (int i = 0; i < coinSet.Length; i++)
            {
                // check if coin is less than the sum
                for (int j = coinSet[i]; j <= sum; j++)
                    table[j] = table[j] + table[j - coinSet[i]];
            }

            return table[sum];
        }
        #endregion
    }
}
