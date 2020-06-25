using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    class BackTrackingApplication
    {
        public static void Main()
        {
            // static vs non-static.
            // non-static class stores its own reference data which is NOT shared.
            SudokuProblem sudokuProblem = new SudokuProblem();
            //sudokuProblem.SolveSudokuProblem();

            NQueenProblem queenProblem = new NQueenProblem(4);
            //queenProblem.Solve();
        }

        #region Learning Back-Tracking with Sudoku Solver
        // Choice , Constrain, Goal

        class SudokuProblem
        {
            public void SolveSudokuProblem()
            {
                // 9 by 9 soduku board
                int[,] sudokuBoard =
                {
                    { 7,8,0,4,0,0,1,2,0 },
                    { 6,0,0,0,7,5,0,0,9 },
                    { 0,0,0,6,0,1,0,7,8 },
                    { 0,0,7,0,4,0,2,6,0 },
                    { 0,0,1,0,5,0,9,3,0 },
                    { 9,0,4,0,6,0,0,0,5 },
                    { 0,7,0,3,0,0,0,1,2 },
                    { 1,2,0,0,0,7,4,0,0 },
                    { 0,4,9,2,0,6,0,0,7 }
                };

                // Difficulty level high.
                int[,] sudokuBoard2 =
                {
                    { 0,0,0,0,0,1,0,0,0 },
                    { 0,0,0,0,0,0,0,0,0 },
                    { 0,0,0,0,0,6,0,0,0 },
                    { 0,0,0,4,0,0,0,0,0 },
                    { 0,0,0,0,8,0,0,0,0 },
                    { 2,0,9,0,0,0,0,0,7 },
                    { 0,0,0,0,0,0,0,0,0 },
                    { 0,0,0,0,0,3,0,0,0 },
                    { 0,0,0,0,0,0,0,0,0 },
                };


                PrintBoard(sudokuBoard2);
                Count = 0;
                Solve(sudokuBoard2);
                Console.WriteLine($"Sudoku solved in {Count} steps");
                PrintBoard(sudokuBoard2);
            }
            private int Count;

            private bool Solve(int[,] sudokuBoard)
            {
                //this.PrintBoard(sudokuBoard);

                // base cases
                (int, int) emptyPosition = FindEmpty(sudokuBoard);

                // could not find an empty position on the entire board.
                if (emptyPosition == (-1, -1)) return true;

                // Create tuple
                (int row, int col) = emptyPosition;

                // choose number from 1 to 9
                for (int i = 1; i <= 9; i++)
                {
                    if (IsValid(sudokuBoard, emptyPosition, i) == true)
                    {
                        sudokuBoard[row, col] = i;
                        Count++;

                        // backtrack. i state is cache on stack.
                        if (Solve(sudokuBoard) == true) return true;
                        else
                        {
                            sudokuBoard[row, col] = 0;
                        }
                    }
                }

                return false;
            }

            private bool IsValid(int[,] sudokuBoard, (int, int) currentPosition, int number)
            {
                // Position
                (int row, int col) = currentPosition;

                // Check row if row contains number
                for (int i = 0; i < sudokuBoard.GetLength(1); i++)
                {
                    if (sudokuBoard[row, i] == number) return false;
                }

                // Check column if column contains number
                for (int i = 0; i < sudokuBoard.GetLength(0); i++)
                {
                    if (sudokuBoard[i, col] == number) return false;
                }

                // check box if box contains number.
                (int, int) box;
                box.Item1 = row / 3;
                box.Item2 = col / 3;

                // There are 9 boxes in sudoku.
                for (int i = box.Item1 * 3; i < box.Item1 * 3 + 3; i++)
                {
                    for (int j = box.Item2 * 3; j < box.Item2 * 3 + 3; j++)
                    {
                        if (sudokuBoard[i, j] == number && (i, j) != currentPosition)
                            return false;
                    }
                }

                return true;
            }

            // return a tuple position of the empty slot.
            private (int, int) FindEmpty(int[,] sudokuBoard)
            {
                for (int i = 0; i < sudokuBoard.GetLength(0); i++)
                {
                    for (int j = 0; j < sudokuBoard.GetLength(1); j++)
                    {
                        // if there are no 0 on the board, means it is completed.
                        if (sudokuBoard[i, j] == 0) return (i, j);
                    }
                }

                // finished.
                return (-1, -1);
            }

            // only works for 9 X 9
            private void PrintBoard(int[,] sudokuBoard)
            {
                Console.WriteLine("*-----------------------*");
                for (int i = 0; i < sudokuBoard.GetLength(0); i++)
                {
                    Console.Write("| ");
                    for (int j = 0; j < sudokuBoard.GetLength(1); j++)
                    {
                        if (j == 3 || j == 6) Console.Write("| ");

                        Console.Write(sudokuBoard[i, j] + " ");
                    }
                    Console.WriteLine("|");

                    if (i == 2 || i == 5) Console.WriteLine("|-----------------------|");
                }
                Console.WriteLine("*-----------------------*");
            }
        }
        #endregion

        #region Backtracking and Branch & Bound
        // Backtracking is Depth First Search
        // Branch and Bound is Breath First Search


        #endregion 

        #region NQueen Problem
        private static void ForLoopTest()
        {
            // you can declare and initialize value types like this.
            int x = 0, y = 0;
            Console.WriteLine($"{x} {y}");

            // i and j is incremented together.
            for(int i = 0, j = 0; i <= 4 && j <= 4; i++, j++)
            {
                Console.WriteLine($"[{i},{j}]");
            }
        }
        private static int NumberOfCombination(int n)
        {
            // assuming you do not consider diagonal check.

            // sum = 1, add the first node.
            int sum = 1;

            for (int i = 0; i < n; i++)
            {
                int total = 1;
                for (int j = 0; j <= i; j++)
                    total *= n - j;

                sum += total;
            }

            return sum;
        }
        // do similar example to Number of Combinations recursively
        // Call stack remembers state
        private static int SolveC(int n)
        {
            //solve 5 + 4 + 3 + 2 + 1
            int sum = n;
            for (int i = 1; i < n; i++)
            {
                sum += i;
            }

            return sum;
            //if (n == 1) return 1;
            //return n + Solve(n - 1);
        }
        private static int SolveB(int n)
        {
            int sum = 0;
            for (int j = 1; j < n; j++)
            {
                sum += SolveA(n - 1, j) * n;
            }

            return sum + n + 1;
        }
        private static int SolveA(int n, int i)
        {
            if (n == i) return i;
            return SolveA(n - 1, i) * n;
        }


        // Solving backtracking is about Goal, Choice, Constraint.
        class NQueenProblem
        {
            private int Queens;
            public NQueenProblem(int queens = 8)
            {
                Queens = queens;
            }
            public void Solve(int queens)
            {
                Queens = queens;
                int[,] board = new int[Queens, Queens];
                PrintBoard(board);
                Console.WriteLine();

                if (Solve(board, 0) == false) Console.WriteLine("Solution not found");

                PrintBoard(board);

            }

            public void Solve()
            {
                int[,] board = new int[Queens, Queens];
                List<int[,]> answers = new List<int[,]>();
                Solve(board, 0, answers);
                Console.WriteLine("Total number of solutions: " + answers.Count);
               
            }

            private void ClearBoard(int[,] board)
            {
                // does this work ?
                //board = new int[Queens, Queens];

                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                        board[i, j] = 0;
                }
            }

            private bool Solve(int[,] board, int col, List<int[,]> answers)
            {
                // Define Goal, found our first answer
                if (col == Queens)
                {
                    answers.Add(board);
                    Console.WriteLine("\nFound 1 Answer: ");
                    PrintBoard(board);
                    Console.WriteLine("\n\nFind next solution: ");
                    return true;
                }

                bool result = false;

                // try this col and place queen in all rows, one by one 
                for (int i = 0; i < Queens; i++)
                {
                    // Define constraint
                    if (IsValid(board, (i, col)))
                    {
                        // Define Choice
                        board[i, col] = 1;

                        PrintBoard(board);

                        // Make sure trye if any placement is possible
                        result = Solve(board, col + 1, answers) || result;

                        // if placing queen on the board [i, col] doesnt lead to a solution
                        // then remove queen from the board.
                        board[i, col] = 0;
                    }
                }

                // if queen cannot be place in any row or in this col return false
                return result;
            }

            private bool Solve(int[,] board, int col)
            {
                // Define Goal, found our first answer
                if (col == Queens) return true;

                // Square -> n Queens n Rows, rows == Queens
                for (int i = 0; i < Queens; i++)
                {
                    // Define constraint
                    if(IsValid(board, (i,col)))
                    {
                        // Define Choice
                        board[i, col] = 1;

                        // This is the backtracking algorithm, state space tree
                        if (Solve(board, col + 1)) return true;
                        board[i, col] = 0;
                    }
                }
                return false;
            }

            // Define Constraint
            // As "col" queens are places from 0 to col - 1
            // we only need to check left side for attacking queens
            private bool IsValid(int[,] board, (int,int) position)
            {
                (int row, int col) = position;

                // no need row check, as you can only place on queen on each row.

                // column check
                for (int i = 0; i < col; i++)
                {
                    if (board[row, i] == 1) return false;
                }

                // upper diagonal check on left side
                for(int i = row, j = col; i >= 0 && j >= 0; i--, j--)
                {
                    if (board[i, j] == 1) return false;
                }

                // lower diagonal check on left side
                for(int i = row, j = col; j >= 0 && i < Queens; i++, j--)
                {
                    if (board[i, j] == 1) return false;
                }

                return true;
            }
            private void PrintBoard(int[,] board)
            {
                // Print the borders for the top row.
                Console.Write("-");
                for (int i = 0; i < board.GetLength(0); i++) Console.Write("----");
                Console.WriteLine();

                for (int i = 0; i < board.GetLength(0); i++)
                {
                    Console.Write("|");
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        if (board[i, j] == 1) Console.Write($" Q |");
                        else Console.Write($" - |");
                    }

                    // Print the bottom border
                    Console.Write("\n-");
                    for (int k = 0; k < board.GetLength(0); k++) Console.Write("----");
                    Console.WriteLine();
                }
            }
        }

        #endregion
    }
}
