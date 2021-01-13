using System;
namespace BasicBlazorLibrary.Components.CalendarPopups
{
    internal static class DateTimeDayOfMonthExtensions
    {
        public static DateTime FirstDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }
        public static int DaysInMonth(this DateTime value)
        {
            return DateTime.DaysInMonth(value.Year, value.Month);
        }
        public static DateTime LastDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.DaysInMonth());
        }
        public static string FirstDayStringMonth(this DateTime value)
        {
            string monthName = value.ToString("MMMM");
            return $"{monthName} {value.Year}";
        }
        public static int DayOfWeekColumn(this DayOfWeek day)
        {
            return (int)day + 1; //because 1 based now.
        }
        public static string DayOfWeekShort(this DayOfWeek day)
        {
            string firstStr = day.ToString();
            return firstStr.Substring(0, 3);
        }
    }
}