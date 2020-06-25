//==============================================================================
// Name           : HullMovingAverage
// Description    : Returns the HMA for a given length
// Version        : v.1.1
// Date Created   : 02 - June - 2020
// Time Taken     : 
// Remarks        :
//==============================================================================
// Copyright      : 2020, Sher Ning Technologies           
// License        :      
//==============================================================================

/* ------------------------------- Version History -------------------------------
 * v1.0 Uses a list to compute the data for Hull Moving Average
 * 
 * v1.1 Replaces the list with a Jagged Array for computing HMA
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SherNingMultiChartsLib
{

    #region Hull Moving Average
    class HullMovingAverage
    {
        // class properties
        public double Value { get { return CalcHma(); } }

        // class fields
        private int Length;
        private int[] Lengths;
        private double[][] PriceList;
        private int Count, Count3;

        // constructor
        public HullMovingAverage(int length)
        {
            // Use constructor to StartCalc()
            SetLength(length);
        }

        #region Public Methods
        public void AddData(double data)
        {
            // Shift all data to the front of the list
            for (int i = 0; i < Lengths[2] - 1; i++)
                PriceList[2][i] = PriceList[2][i + 1];

            // Add latest data to end of list
            PriceList[2][Lengths[2] - 1] = data;
            Count++;

            // Remove overflow
            if (Count > Length) Count--;
        }
        public void SetLength(int length)
        {
            // parameter check
            if (length < 1) Length = 1;
            else Length = length;

            if (Length < 1) Length = 1;

            // 3 lengths, avoid [0]
            Lengths = new int[4];

            // Calculate only when length value has changed
            int halvedLength;
            if ((Math.Ceiling((double)(Length / 2)) - (Length / 2)) <= 0.5)
            {
                halvedLength = (int)Math.Ceiling((double)(Length / 2));
            }
            else
            {
                halvedLength = (int)Math.Floor((double)(Length / 2));
            }

            int sqrRootLength;
            double sqLength = Math.Sqrt(Length);

            if ((Math.Ceiling(sqLength) - sqLength) <= 0.5)
            {
                sqrRootLength = (int)Math.Ceiling(sqLength);
            }
            else
            {
                sqrRootLength = (int)Math.Floor(sqLength);
            }

            //Lengths[0] = 0 by default
            Lengths[1] = halvedLength;
            Lengths[2] = Length;
            Lengths[3] = sqrRootLength;

            // Reset Count
            Count = Count3 = 0;

            // Avoid [0]
            PriceList = new double[4][];

            //PriceList[0] = null by default
            PriceList[1] = new double[Lengths[1]];
            PriceList[2] = new double[Lengths[2]];
            PriceList[3] = new double[Lengths[3]];
        }
        #endregion

        #region Private Methods
        private double CalcHma()
        {
            // insufficient data
            if (Count != Length) return 0;

            //PriceList1[lastvalue--] = PriceList[lastvalue--]
            for (int i = Lengths[1] - 1; i >= 0; i--)
                PriceList[1][i] = PriceList[2][i + Lengths[2] - Lengths[1]];

            // calculate 
            double wma1 = 2.0 * WeightedAverage(PriceList[1], Lengths[1]);
            double wma2 = WeightedAverage(PriceList[2], Lengths[2]);

            // shuffle to the front
            for (int i = 0; i < Lengths[3] - 1; i++)
                PriceList[3][i] = PriceList[3][i + 1];

            // Add to the back of the list
            PriceList[3][Lengths[3] - 1] = wma1 - wma2;
            Count3++;

            // remove overflow
            if (Count3 > Lengths[3]) Count3--;

            if (Count3 == Lengths[3])
                return WeightedAverage(PriceList[3], Lengths[3]);
            else
                return -1;
        }

        private double WeightedAverage(double[] dataSet, int length)
        {
            if (length < 1) return 0;

            double denominator = 1 / ((length + 1) * length * 0.5);
            double weightedSum = 0;

            for (int i = 0; i < length; i++)
                weightedSum += (length - i) * dataSet[length - 1 - i];

            return weightedSum * denominator;
        }
        #endregion
    }

    #endregion

    #region HullMovingAverage v1.1
    /// <summary>
    /// Update to replace List instead of Jagged Array
    /// </summary>
    class HullMovingAveragev11
    {
        // class properties
        public double Value { get { return CalcHma(); } }

        // class fields
        private int Length;
        private int[] Lengths;
        private double[][] PriceList;
        private int Count, Count3;

        // constructor
        public HullMovingAveragev11(int length)
        {
            // Use constructor to StartCalc()
            SetLength(length);
        }

        #region Public Methods
        public void AddData(double data)
        {
            // Shift all data to the front of the list
            for (int i = 0; i < Lengths[2] - 1; i++)
                PriceList[2][i] = PriceList[2][i + 1];

            // Add latest data to end of list
            PriceList[2][Lengths[2] - 1] = data;
            Count++;

            // Remove overflow
            if (Count > Length) Count--;
        }
        public void SetLength(int length)
        {
            // parameter check
            if (length < 1) Length = 1;
            else Length = length;

            if (Length < 1) Length = 1;

            // 3 lengths, avoid [0]
            Lengths = new int[4];

            // Calculate only when length value has changed
            int halvedLength;
            if ((Math.Ceiling((double)(Length / 2)) - (Length / 2)) <= 0.5)
            {
                halvedLength = (int)Math.Ceiling((double)(Length / 2));
            }
            else
            {
                halvedLength = (int)Math.Floor((double)(Length / 2));
            }

            int sqrRootLength;
            double sqLength = Math.Sqrt(Length);

            if ((Math.Ceiling(sqLength) - sqLength) <= 0.5)
            {
                sqrRootLength = (int)Math.Ceiling(sqLength);
            }
            else
            {
                sqrRootLength = (int)Math.Floor(sqLength);
            }

            //Lengths[0] = 0 by default
            Lengths[1] = halvedLength;
            Lengths[2] = Length;
            Lengths[3] = sqrRootLength;

            // Reset Count
            Count = Count3 = 0;

            // Avoid [0]
            PriceList = new double[4][];
            
            //PriceList[0] = null by default
            PriceList[1] = new double[Lengths[1]];
            PriceList[2] = new double[Lengths[2]];
            PriceList[3] = new double[Lengths[3]];
        }
        #endregion

        #region Private Methods
        private double CalcHma()
        {
            // insufficient data
            if (Count != Length) return 0;

            //PriceList1[lastvalue--] = PriceList[lastvalue--]
            for (int i = Lengths[1] - 1; i >= 0; i--)
                PriceList[1][i] = PriceList[2][i + Lengths[2] - Lengths[1]];

            // calculate 
            double wma1 = 2.0 * WeightedAverage(PriceList[1], Lengths[1]);
            double wma2 = WeightedAverage(PriceList[2], Lengths[2]);

            // shuffle to the front
            for (int i = 0; i < Lengths[3] - 1; i++)
                PriceList[3][i] = PriceList[3][i + 1];

            // Add to the back of the list
            PriceList[3][Lengths[3] - 1] = wma1 - wma2;
            Count3++;

            // remove overflow
            if (Count3 > Lengths[3]) Count3--;

            if (Count3 == Lengths[3])
                return WeightedAverage(PriceList[3], Lengths[3]);
            else
                return -1;
        }

        private double WeightedAverage(double[] dataSet, int length)
        {
            if (length < 1) return 0;

            double denominator = 1 / ((length + 1) * length * 0.5);
            double weightedSum = 0;

            for (int i = 0; i < length; i++)
                weightedSum += (length - i) * dataSet[length - 1 - i];

            return weightedSum * denominator;
        }
        #endregion
    }

    #endregion

    #region HullMovingAverage v1.0
    /// <summary>
    /// Uses a list to store and compute data
    /// </summary>
    class HullMovingAveragev10
    {
        // class properties
        public double Value { get { return CalcHma(); } }

        // class fields
        private int Length, Length1, Length2, Length3;
        private List<double> PriceList, PriceList1, PriceList3;

        // constructor
        public HullMovingAveragev10(int length)
        {
            // force to initialize HMA
            SetLength(length);
        }

        #region Public Methods
        public void AddData(double data)
        {
            // Add data
            PriceList.Add(data);

            // If overflow, remove the last added data in the list.
            if (PriceList.Count > Length) PriceList.RemoveAt(0);
        }

        public void SetLength(int length)
        {
            // parameter check
            if (length < 1) Length = 1;
            else Length = length;

            if (Length < 1) Length = 1;

            // Calculate only when length value has changed
            int halvedLength;
            if ((Math.Ceiling((double)(Length / 2)) - (Length / 2)) <= 0.5)
            {
                halvedLength = (int)Math.Ceiling((double)(Length / 2));
            }
            else
            {
                halvedLength = (int)Math.Floor((double)(Length / 2));
            }

            int sqrRootLength;
            double sqLength = Math.Sqrt(Length);

            if ((Math.Ceiling(sqLength) - sqLength) <= 0.5)
            {
                sqrRootLength = (int)Math.Ceiling(sqLength);
            }
            else
            {
                sqrRootLength = (int)Math.Floor(sqLength);
            }

            Length1 = halvedLength;
            Length2 = Length;
            Length3 = sqrRootLength;

            PriceList = new List<double>();
            PriceList3 = new List<double>();
            PriceList1 = new List<double>(Length1);

            // Need to increase the count of the list to access elements
            for (int i = 0; i < Length1; i++) PriceList1.Add(0);
        }
        #endregion

        #region Private Methods
        private double CalcHma()
        {
            // insufficient data
            if (PriceList.Count != Length) return 0;

            //PriceList1[lastvalue--] = PriceList[lastvalue--]
            for (int i = Length1 - 1; i >= 0; i--)
                PriceList1[i] = PriceList[i + Length2 - Length1];

            // calculate 
            double wma1 = 2.0 * WeightedAverage(PriceList1, Length1);
            double wma2 = WeightedAverage(PriceList, Length2);

            PriceList3.Add(wma1 - wma2);
            if (PriceList3.Count > Length3) PriceList3.RemoveAt(0);

            if (PriceList3.Count == Length3)
                return WeightedAverage(PriceList3, Length3);
            else
                return -1;
        }

        private double WeightedAverage(List<double> dataSet, int length)
        {
            if (length < 1) return 0;

            double denominator = 1 / ((length + 1) * length * 0.5);
            double weightedSum = 0;

            for (int i = 0; i < length; i++)
                weightedSum += (length - i) * dataSet[length - 1 - i];

            return weightedSum * denominator;
        }
        #endregion
    }

    #endregion
}
