using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SherNingMultiChartsLib
{
    class MultiChartTestPad
    {
        public static void Main()
        {
            Strategy strategy = new Strategy();
            strategy.CalcBar();
        }
    }

    public interface IStudy
    {
        int PositionSide { get; }
        int TotalTrades { get; }
    }

    class Strategy : IStudy
    {
        public int PositionSide { get; set; }
        public int TotalTrades { get; set; }
        public int MyProperty { get; set; }

        private VariableSeries<int> Series;
        private bool Created;
        public Strategy()
        {
            PositionSide = 1;
            TotalTrades = 1;
        }
        public void Create()
        {
            Series = new VariableSeries<int>(this);
            Created = true;
        }

        public void CalcBar()
        {
            if (Created == false) Create();

            PositionSide++;
            TotalTrades++;
            Console.WriteLine("Call from strategy");
            Console.WriteLine("PostionSide: " + this.PositionSide);

            // call from test method
            Series.TestMethod();
        }
    }


    class VariableSeries<T> : IStudy
    {
        // properties - all 0
        public int PositionSide { get; }
        public int TotalTrades { get; }

        // makes only one change in its lifetime, which is during constructor call
        private readonly IStudy Study;

        public VariableSeries(IStudy master)
        {
            // take the reference of Strategy
            Study = master;
        }

        public void TestMethod()
        {

            // Study points to strategy 's properties
            // VariableSeries own property is 0
            Console.WriteLine("Call from Function");
            Console.WriteLine("Position Side: " + Study.PositionSide);
        }
    }
}
