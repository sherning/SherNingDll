using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SherNingMultiChartsLib.MathFunctions;

namespace SherNingMultiChartsLib
{
    public class RunSherNingMCLib
    {
        public static void Main()
        {
        }

        private static void StaticPropertyTest()
        {
            Console.WriteLine("Calling from main");
            Console.WriteLine(Test.Creed);
        }

        private static void PamaMtfTest()
        {
            PriceActionMovingAverageMTF PamaMtf = new PriceActionMovingAverageMTF();
            PamaMtf.Load(6, 10, new TimeSpan(17, 30, 00), new TimeSpan(17, 0, 0));

            PriceDataReader sr = new PriceDataReader(@"c:\multichartpricedata\GBP.JPY 60 Minute Price Data.txt");
            Bars barData = sr.GetBars;

            for (int i = 0; i < barData.GetNumOfBars[0]; i++)
            {
                Bars newBar = new Bars();
                newBar.AddTime(barData.Time[i]);
                newBar.AddHigh(barData.High[i]);
                newBar.AddLow(barData.Low[i]);
                newBar.AddOpen(barData.Open[i]);
                newBar.AddClose(barData.Close[i]);

                PamaMtf.AddData(newBar);

                for (int j = 5; j < 11; j++)
                {
                    for (int k = 0; k < PamaMtf.Values[j].Length; k++)
                    {
                        Console.WriteLine("timeframe: " + j);
                        Console.WriteLine("Pama prev value: " + PamaMtf.Values[j][1]);
                    }
                }
            }
        }
        private static void ClockTest()
        {
            InternationalClock clock = new InternationalClock();
            clock.City = City.Sydney;
            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine(clock.DisplayClock());
                Thread.Sleep(1000);
            }
        }
        private static void StdDevTest()
        {
            StandardDeviationCalculator calculator = new StandardDeviationCalculator();
            calculator.SetLength(5);
            calculator.PopulationStdDev = true;
            calculator.Add(1);
            calculator.Add(2);
            calculator.Add(3);
            calculator.Add(4);
            calculator.Add(5);
            Console.WriteLine(calculator.Value);
        }
        private static void PamaTest()
        {
            PriceDataReader sr = new PriceDataReader(@"c:\multichartpricedata\GBP.JPY 60 Minute Price Data.txt");
            //sr.Print();
            PriceActionMovingAverage pama = new PriceActionMovingAverage(6, 10);

            foreach (double close in sr.GetBars.Close)
            {
                pama.AddData(close);
                double results = pama.Value[1];
                Console.WriteLine(results);
            }
        }
        private static void PriceReaderTest()
        {
            PriceDataReader sr = new PriceDataReader(@"c:\multichartpricedata\GBP.JPY 60 Minute Price Data.txt");
            sr.Print();

            HullMovingAverage hma = new HullMovingAverage(6);
            hma.SetLength(6);

            foreach (double close in sr.GetBars.Close)
            {
                hma.AddData(close);
                Console.WriteLine(hma.Value);
            }
        }
    }
}
