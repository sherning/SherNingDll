//==============================================================================
// Name           : Bars
// Description    : Container for storing price data
// Version        : v1.0
// Date Created   : 02 - June - 2020
// Time Taken     : 
// Remarks        :
//==============================================================================
// Copyright      : 2020, Sher Ning Technologies           
// License        :      
//==============================================================================

/* ------------------------------- Version History -------------------------------
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SherNingMultiChartsLib
{
    #region Bars
    struct Bars
    {
        // class properties
        public DateTime[] Time { get { return GetTimes(); } }
        public double[] High { get { return GetHighs(); } }
        public double[] Low { get { return GetLows(); } }
        public double[] Open { get { return GetOpens(); } }
        public double[] Close { get { return GetCloses(); } }
        public int[] GetNumOfBars { get { return NumOfBars; } }

        // class fields
        // [0]: Time [1]: High, [2]: Low, [3]: Open, [4]: Close
        private const int BarTypes = 5;
        private int MaxBars;
        private DateTime[] Times;
        private double[] Highs, Lows, Opens, Closes;
        private int[] NumOfBars;

        #region Setup Functions
        public void SetMaxBars()
        {
            SetMaxBars(1);
        }
        public void SetMaxBars(int maxbars)
        {
            if (maxbars >= 1)
                MaxBars = maxbars;
            else
                MaxBars = 1;

            // Initialize containers and reset fields
            Times = new DateTime[MaxBars];
            Highs = new double[MaxBars];
            Lows = new double[MaxBars];
            Opens = new double[MaxBars];
            Closes = new double[MaxBars];
            NumOfBars = new int[BarTypes];
        }

        #endregion

        #region Analytical Functions

        public double HighestValue(int datatype)
        {
            double[] data = ExportData(datatype);
            if (data == null) return 0;

            double highestValue = data[0];

            for (int i = 1; i < MaxBars; i++)
            {
                if (data[i] > highestValue)
                    highestValue = Math.Max(data[i], highestValue);
            }

            return highestValue;
        }

        public double LowestValue(int datatype)
        {
            double[] data = ExportData(datatype);
            if (data == null) return 0;

            double lowestValue = data[0];
            for (int i = 1; i < MaxBars; i++)
            {
                if (data[i] < lowestValue)
                    lowestValue = Math.Min(data[i], lowestValue);
            }

            return lowestValue;
        }
        #endregion

        #region Add Data Functions
        public void AddTime(DateTime dateTime)
        {
            if (MaxBars < 1) SetMaxBars(1);

            int barId = 0;

            for (int i = MaxBars - 1; i > 0; i--)
                Times[i] = Times[i - 1];

            // Cache data and update bar count
            Times[0] = dateTime;
            NumOfBars[barId]++;

            if (NumOfBars[barId] > MaxBars) NumOfBars[barId]--;
        }

        public void AddHigh(double high)
        {
            if (MaxBars < 1) SetMaxBars(1);

            int barId = 1;

            for (int i = MaxBars - 1; i > 0; i--)
                Highs[i] = Highs[i - 1];

            // Cache data and update bar count
            Highs[0] = high;
            NumOfBars[barId]++;

            if (NumOfBars[barId] > MaxBars) NumOfBars[barId]--;
        }

        public void AddLow(double low)
        {
            if (MaxBars < 1) SetMaxBars(1);

            int barId = 2;

            for (int i = MaxBars - 1; i > 0; i--)
                Lows[i] = Lows[i - 1];

            // Cache data and update bar count
            Lows[0] = low;
            NumOfBars[barId]++;

            if (NumOfBars[barId] > MaxBars) NumOfBars[barId]--;
        }

        public void AddOpen(double open)
        {
            if (MaxBars < 1) SetMaxBars(1);

            int barId = 3;

            for (int i = MaxBars - 1; i > 0; i--)
                Opens[i] = Opens[i - 1];

            // Cache data and update bar count
            Opens[0] = open;
            NumOfBars[barId]++;

            if (NumOfBars[barId] > MaxBars) NumOfBars[barId]--;
        }

        public void AddClose(double close)
        {
            if (MaxBars < 1) SetMaxBars(1);

            int barId = 4;

            for (int i = MaxBars - 1; i > 0; i--)
                Closes[i] = Closes[i - 1];

            // Cache data and update bar count
            Closes[0] = close;
            NumOfBars[barId]++;

            if (NumOfBars[barId] > MaxBars) NumOfBars[barId]--;
        }
        #endregion

        #region Get Data Functions
        private DateTime[] GetTimes()
        {
            int barId = 0;

            DateTime[] tempArr = new DateTime[NumOfBars[barId]];

            for (int i = 0; i < NumOfBars[barId]; i++)
                tempArr[i] = Times[NumOfBars[barId] - i - 1];

            return tempArr;
        }

        private double[] GetHighs()
        {
            int barId = 1;

            double[] tempArr = new double[NumOfBars[barId]];

            for (int i = 0; i < NumOfBars[barId]; i++)
                tempArr[i] = Highs[NumOfBars[barId] - i - 1];

            return tempArr;
        }

        private double[] GetLows()
        {
            int barId = 2;

            double[] tempArr = new double[NumOfBars[barId]];

            for (int i = 0; i < NumOfBars[barId]; i++)
                tempArr[i] = Lows[NumOfBars[barId] - i - 1];

            return tempArr;
        }

        private double[] GetOpens()
        {
            int barId = 3;

            double[] tempArr = new double[NumOfBars[barId]];

            for (int i = 0; i < NumOfBars[barId]; i++)
                tempArr[i] = Opens[NumOfBars[barId] - i - 1];

            return tempArr;
        }

        private double[] GetCloses()
        {
            int barId = 4;

            double[] tempArr = new double[NumOfBars[barId]];

            for (int i = 0; i < NumOfBars[barId]; i++)
                tempArr[i] = Closes[NumOfBars[barId] - i - 1];

            return tempArr;
        }

        #endregion

        #region Data Management Functions
        public void ClearData()
        {
            if (Highs == null || Lows == null || Opens == null
                || Closes == null || NumOfBars == null) return;

            for (int i = 0; i < MaxBars; i++)
            {
                Times[i] = DateTime.MinValue;
                Highs[i] = 0;
                Lows[i] = 0;
                Opens[i] = 0;
                Closes[i] = 0;
            }

            for (int i = 0; i < BarTypes; i++)
                NumOfBars[i] = 0;
        }
        /// <summary>
        /// 1: High, 2: Low, 3: Open, 4: Close
        /// </summary>
        /// <param name="datatype"></param>
        /// <returns></returns>
        public double[] ExportData(int datatype)
        {
            double[] data;

            switch (datatype)
            {
                case 1: // High
                    data = Highs;
                    break;

                case 2: // Low
                    data = Lows;
                    break;

                case 3: // Open
                    data = Opens;
                    break;

                case 4: // Close
                    data = Closes;
                    break;

                case 0:
                default:
                    data = null;
                    break;
            }

            return data;
        }

        #endregion
    }

    #endregion
}
