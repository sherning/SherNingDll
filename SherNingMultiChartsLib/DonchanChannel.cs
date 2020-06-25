using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SherNingMultiChartsLib
{
    public class DonchanChannel
    {
        // class properties
        public double GetPivotHighValue { get { return GetPivotHigh(); } }
        public double GetPivotLowValue { get { return GetPivotLow(); } }

        // class fields
        private double[] DataSet;
        private int Count, Length;
        private double PivotHighValue, PivotLowValue;

        // class constructor
        public DonchanChannel(int length)
        {
            SetLength(length);
        }
        public DonchanChannel()
        {
            // minimum length required for pivot
            SetLength(3);
        }

        #region Property Functions
        public void ResetClassFields()
        {
            Count = 0;
            PivotHighValue = 0;
            PivotLowValue = 0;
        }
        public void SetLength(int length)
        {
            // First initialization of array
            if (Length == 0)
            {
                if (length > 2)
                    Length = length;
                else
                    // minimum length to calculate pivot
                    Length = 3;

                DataSet = new double[Length];
                ResetClassFields();
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
                    PivotLowValue = 0;
                    PivotHighValue = 0;
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
                    PivotLowValue = 0;
                    PivotHighValue = 0;
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
        #endregion

        #region Pivot Function
        private double GetPivotLow()
        {
            // check for sufficient data
            if (Count < Length) return 0;

            // check data[0] > data[1] < data[2], update Pivot Low Value
            if (DataSet[0] > DataSet[1] && DataSet[1] < DataSet[2])
            {
                double[] tempArr = DataSet;
                PivotLowValue = LowestLow(tempArr);
            }

            return PivotLowValue;
        }
        private double GetPivotHigh()
        {
            // check for sufficient data
            if (Count < Length) return 0;

            // check data[0] < data[1] > data[2]
            if (DataSet[0] < DataSet[1] && DataSet[1] > DataSet[2])
            {
                double[] tempArr = DataSet;
                PivotHighValue = HighestHigh(tempArr);
            }

            return PivotHighValue;
        }

        #endregion

        #region Highest High / Lowest Low
        private double HighestHigh(double[] arr)
        {
            double highestHigh = arr[0];

            for (int i = 1; i < Length; i++)
            {
                if (arr[i] > highestHigh)
                    highestHigh = Math.Max(arr[i], highestHigh);
            }

            return highestHigh;
        }

        private double LowestLow(double[] arr)
        {
            double lowestLow = arr[0];

            for (int i = 1; i < Length; i++)
            {
                if (arr[i] < lowestLow)
                    lowestLow = Math.Min(arr[i], lowestLow);
            }

            return lowestLow;
        }
        #endregion

        #region Sorting Algorithm (Smallest to Largest)
        private void Sort(double[] arr)
        {
            Sort(arr, 0, arr.Length - 1);
        }
        private void Sort(double[] arr, int l, int r)
        {
            // Start index < end index.
            // for most cases will be start = 0, end = arr.Length
            if (l < r)
            {
                // Find the middle point 
                // Remainder for division will also be floored.
                // The combination of finding m and l < r is the key to recursion.
                int m = (l + r) / 2;

                // Sort first and second halves 
                // Sort will continue until m = 0 before sort(arr, m+ 1, r) is called.
                Sort(arr, l, m);
                Sort(arr, m + 1, r);

                // Merge the sorted halves 
                Merge(arr, l, m, r);
            }
        }
        private void Merge(double[] arr, int l, int m, int r)
        {
            // Find sizes of two subarrays to be merged 
            int n1 = m - l + 1;
            int n2 = r - m;

            /* Create temp arrays */
            double[] L = new double[n1];
            double[] R = new double[n2];

            /*Copy data to temp arrays*/
            // Did not touch the original array.
            // Only reference it for COPYING.
            for (int i = 0; i < n1; ++i)
                L[i] = arr[l + i];

            for (int j = 0; j < n2; ++j)
                R[j] = arr[m + 1 + j];


            /* Merge the temp arrays */

            // Initial indexes of first and second subarrays 
            int x = 0, y = 0;

            // Initial index of merged sub array 
            int k = l;
            while (x < n1 && y < n2)
            {
                if (L[x] <= R[y])
                {
                    arr[k] = L[x];
                    x++;
                }
                else
                {
                    // Change the original array.
                    arr[k] = R[y];
                    y++;
                }
                k++;
            }

            /* Copy remaining elements of L[] if any */
            while (x < n1)
            {
                arr[k] = L[x];
                x++;
                k++;
            }

            /* Copy remaining elements of R[] if any */
            while (y < n2)
            {
                arr[k] = R[y];
                y++;
                k++;
            }
        }
        #endregion
    }

}
