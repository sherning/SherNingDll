using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SherNingMultiChartsLib
{
    class WeightedAverage
    {
        // Public properties
        public int Length { private get; set; }
        public List<double> PriceList { get; set; }
        public double Value { get { return CalcBar(); } }
        public bool HasStartCalc { get; set; }

        // Fields.
        private double Denominator;
        private void StartCalc()
        {
            if (Length < 1) Length = 1;

            Denominator = 1 / ((Length + 1) * Length * 0.5);
            HasStartCalc = true;
        }

        private double CalcBar()
        {
            if (HasStartCalc == false) StartCalc();

            double weightedSum = 0;
            for (int i = 0; i < Length; i++)
                weightedSum += (Length - i) * PriceList[Length - 1 - i];

            return weightedSum * Denominator;
        }
    }

}
