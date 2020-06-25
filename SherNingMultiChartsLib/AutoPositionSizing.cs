//==============================================================================
// Name           : AutoPositionSizing
// Description    : Calculates the position size for FX
// Version        : v1.0
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
    class AutoPositionSizingFX
    {
        // class properties
        public double PipValue { get; set; }
        public double OnePip { get; set; }
        public double AccountSize { get; set; }
        public double Risk { get; set; }
        public int StepSize { get; set; }
        public int MinimumContracts { get; set; }
        public int MaximumContracts { get; set; }

        // class fields
        private const int StandardFxLot = 100000;

        // class constructor
        public AutoPositionSizingFX() { }
        public AutoPositionSizingFX(double accountSize, double risk)
        {
            AccountSize = accountSize;
            Risk = risk;

            // 20 thousand
            StepSize = 20000;

            // 20 thousand
            MinimumContracts = 20000;
            
            // 10 million
            MaximumContracts = 10000000;
        }

        // class methods
        public int Calculate(double entryPrice, double stopLossPrice)
        {
            // return tradesize
            int tradeSize;

            // calculate price risk
            double priceRisk = Math.Abs(entryPrice - stopLossPrice);

            // check if risk is less than one pip.
            if (priceRisk < OnePip) priceRisk = OnePip;

            // calculate numerator and denominator values
            double valueRisk = (priceRisk / OnePip) * PipValue;
            double dollarRisk = AccountSize * 0.01 * Risk;

            // obtain raw trade size
            double rawTradeSize = (dollarRisk / valueRisk) * StandardFxLot;

            // convert to min step
            rawTradeSize = StepSize * Math.Floor(rawTradeSize / StepSize);

            // check if less than min value
            if (rawTradeSize < MinimumContracts) rawTradeSize = 0;
            if (rawTradeSize > MaximumContracts) rawTradeSize = MaximumContracts;

            tradeSize = (int)rawTradeSize;

            return tradeSize;
        }
    }
}
