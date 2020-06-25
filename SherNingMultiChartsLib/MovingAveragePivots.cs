using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SherNingMultiChartsLib
{
    public class MovingAveragePivots
    {
        // class properties
        public double PivotHighValue { get { return GetPivotHighValue(); } }
        public double PivotLowValue { get { return GetPivotLowValue(); } }

        // class fields
        private const int DataPoints = 3;
        private Bars Bars;
        private double[] DataSet;
        private int Length, Count;
        private double _PivotHighValue, _PivotLowValue;

        // class constructors
        public MovingAveragePivots(int length)
        {
            Length = length;
            Bars.SetMaxBars(Length);
            DataSet = new double[DataPoints];
        }

        public MovingAveragePivots()
        {
            Bars.SetMaxBars();
            DataSet = new double[DataPoints];
        }

        #region Main Functions
        private double GetPivotHighValue()
        {
            if (PivotHigh() == true)
                _PivotHighValue = Bars.HighestValue(0);

            return _PivotHighValue;
        }

        private double GetPivotLowValue()
        {
            if (PivotLow() == true)
                _PivotLowValue = Bars.LowestValue(1);

            return _PivotLowValue;
        }

        private bool PivotHigh()
        {
            // Insufficient Data
            if (Count < DataPoints) return false;

            if (DataSet[0] < DataSet[1] && DataSet[1] > DataSet[2])
                return true;

            return false;
        }

        private bool PivotLow()
        {
            // Insufficient Data
            if (Count < DataPoints) return false;

            if (DataSet[0] > DataSet[1] && DataSet[1] < DataSet[2])
                return true;

            return false;
        }
        #endregion

        #region Add Data
        public void AddData(double data)
        {
            for (int i = DataPoints - 1; i > 0; i--)
                DataSet[i] = DataSet[i - 1];

            DataSet[0] = data;
            Count++;

            if (Count > DataPoints) Count--;
        }

        public void AddHigh(double data)
        {
            Bars.AddHigh(data);
        }

        public void AddLow(double data)
        {
            Bars.AddLow(data);
        }
        #endregion

        #region Initalize and Clear Data
        public void SetLength(int length)
        {
            Clear();
            Bars.SetMaxBars(length);
        }

        public void Clear()
        {
            Bars.ClearData();
            Count = 0;
            _PivotHighValue = 0;
            _PivotLowValue = 0;
            for (int i = 0; i < DataPoints; i++)
                DataSet[i] = 0;
        }
        #endregion
    }
}
