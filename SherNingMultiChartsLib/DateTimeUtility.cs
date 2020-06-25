using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SherNingMultiChartsLib
{
    static class DateTimeUtility
    {
        public static bool IsLastDayOfTheYear(DateTime date)
        {
            DateTime lastDayOfYear = new DateTime(date.Year, 12, 31);

            // if date input is lastday of the year.
            if (date.Date == lastDayOfYear.Date) 
                return true;

            return false;
        }

        public static int GetLastFridayOfMonth(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;

            DateTime lastFriday = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);

            // from the last day of the month, work back to last friday
            while (lastFriday.DayOfWeek != DayOfWeek.Friday)
                lastFriday = lastFriday.AddDays(-1);

            return lastFriday.Day;
        }

        //public static int GetLastWorkingDayOfMonth(DateTime date)
        //{
            
        //}

    }
}
