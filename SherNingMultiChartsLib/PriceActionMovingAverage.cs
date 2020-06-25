//==============================================================================
// Name           : Price Actio Moving Average Single TF
// Description    : Returns the PAMA for a given length
// Version        : v.1.0
// Date Created   : 03 - June - 2020
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
    #region Price Action Moving Average
    class PriceActionMovingAverage
    {
        // class properties
        public double[] Value
        {
            get
            {
                CalcBar();
                return PamaValues;
            }
        }

        // class fields
        private double Alpha, Gain;
        private int Length, Smooth;
        private HullMovingAverage Hma;
        private double[] PamaValues;

        // class constructor
        public PriceActionMovingAverage() { }
        public PriceActionMovingAverage(int length, int smooth)
        {
            SetProperties(length, smooth);
        }

        // class methods
        public void SetProperties(int length, int smooth)
        {
            Length = length;
            Smooth = smooth;

            // Variable Calculations
            Alpha = 2.0 / (Length + 1.0);
            Gain = Smooth / 100.0;

            // 0: current 1-9: previous values
            PamaValues = new double[10];
            Hma = new HullMovingAverage(Length);
            Hma.SetLength(Length);
        }

        private void CalcBar()
        {
            for (int i = PamaValues.Length - 1; i > 0; i--)
                PamaValues[i] = PamaValues[i - 1];

            //PamaValues[1] = PamaValues[0];

            double HmaValue = Hma.Value;
            if (HmaValue <= 0) return;

            // If gain is zero, PAMA become a EMA(HMA)
            if (HmaValue - PamaValues[1] > 0)
            {
                PamaValues[0]
                = Alpha * (HmaValue
                + (-Gain) * (HmaValue - PamaValues[1]))
                + (1.0 - Alpha) * PamaValues[1];
            }
            else if (HmaValue - PamaValues[1] < 0)
            {
                PamaValues[0]
                = Alpha * (HmaValue
                + Gain * (HmaValue - PamaValues[1]))
                + (1.0 - Alpha) * PamaValues[1];
            }
            else
            {
                PamaValues[0]
               = Alpha * (HmaValue
               + 0.0 * (HmaValue - PamaValues[1]))
               + (1.0 - Alpha) * PamaValues[1];
            }
        }

        public void AddData(double data)
        {
            // add directly to HMA
            Hma.AddData(data);
        }
    }
    #endregion

}
