using System;

namespace ChileComprasProxy.Framework.Utilities.ExtensionMethod
{
    public static class DateTimeEx
    {
        public static int TotalWeeks(this DateTime later, DateTime earlier)
        {
            return later.Subtract(earlier).Days / 7;
        }

        public static int TotalMonths(this DateTime later, DateTime earlier)
        {
            var months = -1;
            var tempLaterDate = later;
            while (tempLaterDate.CompareTo(earlier) >= 0)
            {
                months++;
                tempLaterDate = tempLaterDate.AddMonths(-1);
            }
            return months;
        }

        public static DateTime LastDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
        }

        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime DatePreviousMonth(this DateTime date)
        {
            return date.AddMonths(-1);
        }
    }
}