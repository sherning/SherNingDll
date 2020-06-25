using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SherNingMultiChartsLib
{
    #region InternationalClock
    public enum City
    {
        Singapore, NewYork, London, Tokyo, Sydney, Melbourne, SanFrancisco, LosAngeles, Dubai
    }
    class InternationalClock
    {
        public City City { get; set; }
        public string DisplayClock()
        {
            DateTime clock = GetTime(GetZoneId());
            return clock.ToString();
        }
        private string GetZoneId()
        {
            switch (City)
            {
                default:
                case City.Singapore:
                    return "Singapore Standard Time";

                case City.NewYork:
                    return "Eastern Standard Time";

                case City.London:
                    return "GMT Standard Time";

                case City.Tokyo:
                    return "Tokyo Standard Time";

                case City.Melbourne:
                case City.Sydney:
                    return "AUS Eastern Standard Time";

                case City.LosAngeles:
                case City.SanFrancisco:
                    return "Pacific Standard Time";

                case City.Dubai:
                    return "Middle East Standard Time";
            }
        }
        private DateTime GetTime(string zoneId)
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo timeZone =
                TimeZoneInfo.FindSystemTimeZoneById(zoneId);

            return TimeZoneInfo.ConvertTime(utcTime, timeZone);
        }
    }
    #endregion
}
