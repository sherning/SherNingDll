using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SherNingMultiChartsLib.MathFunctions
{
    public class StandardDeviationCalculator
    {
        // class properties
        public bool PopulationStdDev { get; set; }
        public int Count { get; private set; }
        public double Value { get { return CalcStdDev(); } }

        // class fields
        private double[] DataSet;
        private int Length;

        // Constructor to set initial length
        public StandardDeviationCalculator(int length, bool isPopulation)
        {
            PopulationStdDev = isPopulation;
            // ------------ Data Type ------------ //
            // true: Population type calculations  //
            // false: Sample type calculations     //
            // ----------------------------------- //

            // if dataset is small, use population
            SetLength(length);
        }
        public StandardDeviationCalculator()
        {
            PopulationStdDev = true;
            SetLength(1);
        }

        // class methods
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

            // If Length property is not 0
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

            // Add newest data to the front
            DataSet[0] = data;

            // Count number of data added.
            Count++;

            if (Count > Length) Count--;
        }
        private double CalcStdDev()
        {
            // if insufficient data
            if (Count < Length) return 0;

            // Calculate mean sum
            double mean = 0;
            for (int i = 0; i < Length; i++)
                mean += DataSet[i];

            // mean = summation(i=0 to i=Length-1) / Length
            mean /= Length;

            // calculate variance sigma^2
            double variance = 0;
            for (int i = 0; i < Length; i++)
                variance += Math.Pow(DataSet[i] - mean, 2);

            // Calculate sample variance
            if (PopulationStdDev == false) variance /= (Length - 1);

            // Calculate population variance
            else variance /= Length;

            // calculate std dev = Sqrt(variance)
            return Math.Sqrt(variance);
        }
    }
}
