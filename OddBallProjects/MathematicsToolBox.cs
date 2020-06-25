using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OddBallProjects
{
    public class MathematicsToolBox
    {
        private static List<double> PriceList;
        public static void Main()
        {
            //GetPrimesSieveMethod(10000);
            //TrialDivisionMethod(98877889);
            //ImprovedTrialDivisionMethod(98877889);
            //TestCalculateAvg();
        }

        #region Standar Deviation Calculator
        class StandardDeviationCalculator
        {
            // class properties
            public int DataType { get; set; }
            public int Count { get; private set; }
            public double Value { get { return CalcStdDev(); } }

            // class fields
            private double[] DataSet;
            private int Length;

            // Constructor to set initial length
            public StandardDeviationCalculator(int length)
            {
                DataType = 0;
                // ------------ Data Type ------------ //
                // 0: Population type                  //
                // 1: Sample type                      //
                // ----------------------------------- //

                // if dataset is small, use population
                Length = 0;
                SetLength(length);
            }
            public void SetLength(int length)
            {
                // First initialization of array
                if (Length == 0)
                {
                    if (length > 0)
                        Length = length;
                    else
                        Length = 1;

                    DataSet = new double[Length];
                    Count = 0;
                    return;
                }

                // Changing property
                if (Length > 0)
                {
                    if (length > Length)
                    {
                        // resize
                        double[] tempArr = new double[length];

                        // store existing data to temp
                        for (int i = 0; i < Length; i++)
                            tempArr[i] = DataSet[i];

                        // change Length property
                        Count = Length;
                        Length = length;
                        DataSet = new double[Length];
                        DataSet = tempArr;
                    }
                    else if (length < Length)
                    {
                        // resize
                        double[] tempArr = new double[length];
                        for (int i = 0; i < length; i++)
                            tempArr[i] = DataSet[i];

                        Count = Length = length;
                        DataSet = new double[Length];
                        DataSet = tempArr;
                    }
                    else
                    {
                        // remain status quo if no change in length
                    }
                }
            }
            public void Add(double data)
            {
                // shift all the data back by 1
                for (int i = Length - 1; i > 0; i--)
                    DataSet[i] = DataSet[i - 1];

                // Add data
                DataSet[0] = data;

                // Count number of data added.
                Count++;

                if (Count > Length) Count--;
            }
            private double CalcStdDev()
            {
                // if insufficient data
                if (Count < Length) return 0;

                // Calculate mean
                double mean = 0;
                for (int i = 0; i < Length; i++)
                    mean += DataSet[i];

                mean /= Length;

                // calculate variance sigma^2
                double variance = 0;
                for (int i = 0; i < Length; i++)
                    variance += Math.Pow(DataSet[i] - mean, 2);

                // Calculate sample variance
                if (DataType > 0) variance /= (Length - 1);
                
                // Calculate population variance
                else variance /= Length;

                // calculate std dev = Sqrt(variance)
                return Math.Sqrt(variance);
            }
        }

        private static double CalcStdDev(double[] dataSet, int length)
        {
            // Calculating Standard Deviation with Population Data
            if (dataSet.Length < length) return 0;

            // Calculate mean
            double mean = 0;
            for (int i = 0; i < length; i++)
                mean += dataSet[i];

            mean /= length;

            // calculate variance sigma^2
            double variance = 0;
            for (int i = 0; i < length; i++)
                variance += Math.Pow(dataSet[i] - mean, 2);

            variance /= length;

            // calculate std dev = Sqrt(variance)
            return Math.Sqrt(variance);
        }
        #endregion

        private static void TestCalculateAvg()
        {
            int length = 5;
            PriceList = new List<double>(length);

            for (int i = 0; i <= 10; i++)
            {
                // Add first.
                PriceList.Add(i);

                foreach (var item in PriceList)
                    Console.WriteLine("Number in list: " + item);

                if (PriceList.Count > length)
                {
                    Console.WriteLine("Remove at: " + PriceList[0]);
                    PriceList.RemoveAt(0);
                }


                if (PriceList.Count == length)
                    Console.WriteLine("Average: " + CalculateAverage(length));

                Console.WriteLine();
            }

            
        }

        private static double CalculateAverage(int length)
        {
            double sum = 0;
            for (int i = 0; i < length; i++) 
                sum += PriceList[i];

            return sum / length;
        }

        private static double CalcAvg(int length)
        {
            if (length == PriceList.Count - 1) 
                return PriceList[length];

            if (length == 0)
                return (PriceList[length] + CalcAvg(length + 1)) / PriceList.Count;

            return (PriceList[length] + CalcAvg(length + 1));
        }

        private static bool TrialDivisionMethod(int num)
        {
            // Number of steps
            int steps = 0;

            // minimum steps required is 2
            steps = steps + 2;

            // assume prime is true
            bool primeCheck = true;

            // Loop until test is <= square root of num
            int upperBound = (int)Math.Floor(Math.Sqrt(num));

            for (int i = 2; i < upperBound; i++)
            {
                steps++;

                // check if test divides by num
                if (num % i == 0)
                {
                    // Found a factor, therefore num is not prime.
                    primeCheck = false;
                }
            }
            
            // print statement before returning boolean
            if (primeCheck == true)
            {
                Console.WriteLine($"{num} is a prime number, and it took {steps} steps");
            }
            else
            {
                Console.WriteLine($"{num} is not a prime number, and it took {steps} steps");
            }

            // return statement
            return primeCheck;
        }

        private static bool ImprovedTrialDivisionMethod(int num)
        {
            int steps = 0;
            steps = steps + 3;

            // Assume number is prime, unless proven otherwise
            bool primeCheck = true;

            // Check if num is divisible by 2
            if (num % 2 == 0)
            {
                // unless the value is 2 which is a prime, otherwise all other numbers are composite
                if (num == 2)
                {
                    Console.WriteLine($"{num} is a prime number, and it took 1 steps");
                    return true;
                }
                else
                {
                    Console.WriteLine($"{num} is not a prime number, and it took 1 steps");
                    return false;
                }
            }

            // Loop until test is <= square root of num
            int upperBound = (int)Math.Floor(Math.Sqrt(num));

            for (int i = 3; i < upperBound; i++)
            {
                steps++;
                if (num % i == 0)
                {
                    primeCheck = false;
                }
            }

            // print statement before returning boolean
            if (primeCheck == true)
            {
                Console.WriteLine($"{num} is a prime number, and it took {steps} steps");
            }
            else
            {
                Console.WriteLine($"{num} is not a prime number, and it took {steps} steps");
            }

            return primeCheck;
        }

        /// <summary>
        /// Returns print out of all the prime numbers which is less than max prime.
        /// </summary>
        /// <param name="maxPrime"></param>
        private static void GetPrimesSieveMethod(int maxPrime)
        {
            if (maxPrime < 2)
            {
                return;
            }

            // Build array to mark numbers with
            int[] isComposite = new int[maxPrime];

            // build array to hold primes
            List<int> primes = new List<int>();

            // mark 0,1 as not prime numbers
            isComposite[0] = 1;
            isComposite[1] = 1;

            // loop from 2 to sqrt(maxPrime)
            for (int i = 2; i <= Math.Sqrt(maxPrime); i++)
            {
                // if composite is marked 1, means it is not a prime
                // the statement belows states that if the composite is unmarked then...
                if (isComposite[i] != 1)
                {
                    // mark off all multiples start with i * i
                    // j += i finds the multiples. e.g. start is 4 + 2 + 2 + 2 + ...
                    for (int j = i*i; j < maxPrime; j += i)
                    {
                        // mark the current cell as a composite or not a prime.
                        isComposite[j] = 1;
                    }
                }
            }

            for (int i = 0; i < maxPrime; i++)
            {
                if (isComposite[i] != 1)
                {
                    primes.Add(i);
                }
            }

            // Display 
            Console.WriteLine("\\\\--- Displaying all the Prime Numbers less than " + maxPrime + " ---//" + "\n");

            // Display the number of prime numbers in a table
            for (int i = 0; i < primes.Count(); i++)
            {
                Console.Write(" " + primes[i] + " ");

                // if i is not zero, as zero mod 10 is 0!
                if (i != 0 && i % 10 == 0)
                {
                    Console.WriteLine();
                }
            }
             
            Console.WriteLine($"\n\n\\\\--- There are a total of {primes.Count()} prime numbers ---//");
            Console.WriteLine();
        }
    }
}
