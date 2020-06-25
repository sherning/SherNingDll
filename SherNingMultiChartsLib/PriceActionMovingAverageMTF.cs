//==============================================================================
// Name           : Price Action Moving Average MTF
// Description    : Returns a double[][] of values for each PAMA Timeframe
// Version        : v1.0
// Date Created   : 05 - June - 2020
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
    #region Price Action Moving Average MTF
    class PriceActionMovingAverageMTF
    {
        // class properties
        public int Length { get; set; }
        public int Smooth { get; set; }
        public int PriceType { get; set; }
        public int NumOfTfs { get { return Timeframes; } }
        public double[][] Values
        {
            get
            {
                Calculate();
                return PamaValues;
            }
        }

        // class fields
        private List<PriceActionMovingAverage> PAMAs;
        private Bars Bars;
        private const int Timeframes = 11;
        private const int MaxBars = 3;
        private bool[][] IsCurrBarClose;
        private double[][] Highs, Lows, Opens, Closes, Price, PamaValues;
        private TimeSpan SessionStartTime;
        private TimeSpan SessionEndTime;

        // class constructor
        public PriceActionMovingAverageMTF()
        {
            PriceType = 1;
            Length = 6;
            Smooth = 10;
        }

        // class methods
        public void Load(int length, int smooth, TimeSpan sessStart, TimeSpan sessEnd)
        {
            Length = length;
            Smooth = smooth;

            SessionStartTime = sessStart;
            SessionEndTime = sessEnd;

            // load method to be called in startcalc
            PAMAs = new List<PriceActionMovingAverage>();
            IsCurrBarClose = new bool[Timeframes][];
            PamaValues = new double[Timeframes][];
            Highs = new double[Timeframes][];
            Lows = new double[Timeframes][];
            Opens = new double[Timeframes][];
            Closes = new double[Timeframes][];
            Price = new double[Timeframes][];

            // instaniate each Price action object
            for (int i = 0; i < Timeframes; i++)
            {
                PAMAs.Add(new PriceActionMovingAverage());
                IsCurrBarClose[i] = new bool[MaxBars];
                PamaValues[i] = new double[MaxBars];
                Highs[i] = new double[MaxBars];
                Lows[i] = new double[MaxBars];
                Opens[i] = new double[MaxBars];
                Closes[i] = new double[MaxBars];
                Price[i] = new double[MaxBars];
            }

            // set price action properties
            for (int i = 0; i < Timeframes; i++)
            {
                PAMAs[i].SetProperties(Length, Smooth);
            }

            Bars.SetMaxBars(MaxBars);
        }

        public void AddData(Bars bars)
        {
            Bars = bars;
        }

        private void Calculate()
        {
            // for each timeframe in total timeframes
            for (int timeframe = 0; timeframe < Timeframes; timeframe++)
            {
                // add bar state for each timeframe, e.g. 60 min close, 240 min close.
                AddBarState(IsTimeFrameOnCloseTick(timeframe), timeframe);

                // build multi time frame price data
                AddPriceData(timeframe);

                // determine what price to use for calculation
                SetPriceTypes(timeframe);

                // Calculate and cache pama values
                SetAndCalcPama(timeframe);
            }
        }
        private void SetAndCalcPama(int timeframe)
        {
            if (IsCurrBarClose[timeframe][0] == true)
            {
                PAMAs[timeframe].AddData(Price[timeframe][0]);
                AddBarValue(PamaValues, PAMAs[timeframe].Value[0], timeframe);
            }
        }

        private void SetPriceTypes(int timeframe)
        {
            // -------------------------- Price Input -------------------------- //
            //          Option 1: Close Price                                    //
            //          Option 2: Range Price (High - Low)                       //
            //          Option 3: HLOC Average Price                             //
            //          Option 4: OC Average Price                               //
            // ----------------------------------------------------------------- //

            if (IsCurrBarClose[timeframe][0] == true)
            {
                switch (PriceType)
                {
                    default:
                    case 1:
                        AddBarValue(Price, Closes[timeframe][0], timeframe);
                        break;

                    case 2:
                        double range = (Highs[timeframe][0] - Lows[timeframe][0]) * 0.5;
                        AddBarValue(Price, range, timeframe);
                        break;

                    case 3:
                        double average =
                            (Highs[timeframe][0]
                            + Lows[timeframe][0]
                            + Opens[timeframe][0]
                            + Closes[timeframe][0]) * 0.25;

                        AddBarValue(Price, average, timeframe);
                        break;

                    case 4:
                        double openclose = (Opens[timeframe][0] + Closes[timeframe][0]) / 2;
                        AddBarValue(Price, openclose, timeframe);
                        break;
                }
            }
        }
        private void AddPriceData(int timeframe)
        {
            // if previous bar, bar's state is close then
            if (IsCurrBarClose[timeframe][1] == true)
            {
                AddBarValue(Highs, Bars.High[0], timeframe);
                AddBarValue(Lows, Bars.Low[0], timeframe);
                AddBarValue(Opens, Bars.Open[0], timeframe);
            }
            else
            {
                // if the current tick high is higher than the timeframe high
                if (Bars.High[0] > Highs[timeframe][0])
                {
                    Highs[timeframe][0] = Bars.High[0];
                }

                if (Bars.Low[0] < Lows[timeframe][0])
                {
                    Lows[timeframe][0] = Bars.Low[0];
                }
            }


            // close on current bar. add close to list.
            if (IsCurrBarClose[timeframe][0] == true)
            {
                AddBarValue(Closes, Bars.Close[0], timeframe);
            }
        }
        private void AddBarValue(double[][] property, double value, int timeframe)
        {
            // this method works, because you are passing by reference
            int length = property[timeframe].Length;
            for (int i = length - 1; i > 0; i--)
                property[timeframe][i] = property[timeframe][i - 1];

            property[timeframe][0] = value;
        }
        private void AddBarState(bool isBarClose, int timeframe)
        {
            int length = IsCurrBarClose[timeframe].Length;

            for (int i = length - 1; i > 0; i--)
                IsCurrBarClose[timeframe][i] = IsCurrBarClose[timeframe][i - 1];

            IsCurrBarClose[timeframe][0] = isBarClose;
        }
        private bool IsTimeFrameOnCloseTick(int timeframe)
        {
            bool isCloseTick;
            int currTimeStartMin = GetCurrentTimeFromSessionOpen();
            int currTimeStartReg = GetCurrentTime();
            int sessEndTimeReg = GetSessionEndTime();

            switch (timeframe)
            {
                case 0: // 5 min
                    isCloseTick = (currTimeStartMin % 005) == 0 || (currTimeStartReg == sessEndTimeReg);
                    break;

                case 1: // 10 min
                    isCloseTick = (currTimeStartMin % 010) == 0 || (currTimeStartReg == sessEndTimeReg);
                    break;

                case 2: // 15 min
                    isCloseTick = (currTimeStartMin % 015) == 0 || (currTimeStartReg == sessEndTimeReg);
                    break;

                case 3: // 20 min
                    isCloseTick = (currTimeStartMin % 020) == 0 || (currTimeStartReg == sessEndTimeReg);
                    break;

                case 4: // 30 min
                    isCloseTick = (currTimeStartMin % 030) == 0 || (currTimeStartReg == sessEndTimeReg);
                    break;

                case 5: // 60 min
                    isCloseTick = (currTimeStartMin % 060) == 0 || (currTimeStartReg == sessEndTimeReg);
                    break;

                case 6: // 120 min
                    isCloseTick = (currTimeStartMin % 120) == 0 || (currTimeStartReg == sessEndTimeReg);
                    break;

                case 7: // 240 min
                    isCloseTick = (currTimeStartMin % 240) == 0 || (currTimeStartReg == sessEndTimeReg);
                    break;

                case 8: // daily
                    isCloseTick = (currTimeStartReg == sessEndTimeReg);
                    break;

                case 9: // weekly
                    bool isFx = true;
                    bool isSessionEnd = currTimeStartReg == sessEndTimeReg;
                    bool isFriday = Bars.Time[0].DayOfWeek == DayOfWeek.Friday;
                    isCloseTick = (isFx && isSessionEnd && isFriday);
                    break;

                case 10: // monthly
                    bool isLastDayOfMonth = GetLastTradingDayOfMonth();

                    // last day of the year is tricky ..
                    if (IsLastDayOfYear() == true && GetCurrentTime() == 1630)
                        isSessionEnd = true;
                    else
                        isSessionEnd = currTimeStartReg == sessEndTimeReg;

                    isFx = true;
                    isCloseTick = isLastDayOfMonth && isSessionEnd && isFx;
                    break;

                default:
                    isCloseTick = false;
                    break;
            }


            return isCloseTick;
        }
        private bool IsLastDayOfYear()
        {
            DateTime lastDay = new DateTime(Bars.Time[0].Year, 12, 31);
            if (Bars.Time[0].Date == lastDay.Date) return true;

            return false;
        }
        private bool GetLastTradingDayOfMonth()
        {
            int year = Bars.Time[0].Year;
            int month = Bars.Time[0].Month;
            int day = Bars.Time[0].Day;

            // get the total number of days in this month.
            int totalDaysInMonth = DateTime.DaysInMonth(year, month);

            // if last day of month is current bar
            if (Bars.Time[0].Day == totalDaysInMonth) return true;

            // else check for last trading day
            DateTime lastDayOfMth = new DateTime(year, month, totalDaysInMonth);

            // if the last day of the month is a weekend
            if (lastDayOfMth.DayOfWeek == DayOfWeek.Saturday || lastDayOfMth.DayOfWeek == DayOfWeek.Sunday)
            {
                // if current day is the last friday of the month.
                if (day == GetLastFridayOfMonth()) return true;
            }

            return false;
        }
        private int GetLastFridayOfMonth()
        {
            int year = Bars.Time[0].Year;
            int month = Bars.Time[0].Month;
            DateTime lastFriday = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);

            while (lastFriday.DayOfWeek != DayOfWeek.Friday)
                lastFriday = lastFriday.AddDays(-1);

            return lastFriday.Day;
        }
        private int GetSessionEndTime()
        {
            // regular, not in total minutes.
            int hours = SessionEndTime.Hours * 100;
            int minutes = SessionEndTime.Minutes;
            return hours + minutes;
        }
        private int GetCurrentTime()
        {
            int hours = Bars.Time[0].Hour * 100;
            int minutes = Bars.Time[0].Minute;
            return hours + minutes;
        }
        private int GetCurrentTimeFromSessionOpen()
        {
            // minutes from open
            int minFromSessOpen = 0;

            // total min from Midnight. e.g. 1050 / 60 = 17.5 hours
            int sessStartMin = (int)SessionStartTime.TotalMinutes;
            int sessEndMin = (int)SessionEndTime.TotalMinutes;

            // current time in minutes from midnight
            int currTimeMin = 60 * Bars.Time[0].Hour + Bars.Time[0].Minute;

            // no. of minutes in a day 
            int totalMinDay = 24 * 60;

            // if no condition applies. Return 0.
            if (currTimeMin >= sessStartMin && currTimeMin < totalMinDay)
            {
                minFromSessOpen = currTimeMin - sessStartMin;
            }
            else if (currTimeMin > 0 && currTimeMin <= sessEndMin)
            {
                minFromSessOpen = totalMinDay - sessStartMin + currTimeMin;
            }
            else if (currTimeMin == 0)
            {
                minFromSessOpen = totalMinDay - sessStartMin;
            }

            return minFromSessOpen;
        }
    }
    #endregion

}
