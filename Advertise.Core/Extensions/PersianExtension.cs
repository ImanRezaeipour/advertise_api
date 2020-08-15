using System;
using System.Globalization;
using System.Threading;

namespace Advertise.Core.Extensions
{
    public static class PersianExtension
    {
        public static string GetPersianNumber(this string data)
        {
            if (string.IsNullOrEmpty(data)) return string.Empty;
            for (var i = 48; i < 58; i++)
            {
                data = data.Replace(Convert.ToChar(i), Convert.ToChar(1728 + i));
            }
            return data;
        }

        public static string GetEnglishNumber(this string data)
        {
            if (string.IsNullOrEmpty(data)) return string.Empty;
            for (var i = 1777; i < 1786; i++)
            {
                data = data.Replace(Convert.ToChar(i), Convert.ToChar(i - 1728));
            }
            return data;
        }

        public static string GetPersianNumber(this long data)
        {
            return data.ToString(CultureInfo.InvariantCulture).GetPersianNumber();
        }

        public static string GetPersianNumber(this double data)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0:0.00}", data).GetPersianNumber();
        }

        public static string GetPersianNumber(this int data)
        {
            return data.ToString(CultureInfo.InvariantCulture).GetPersianNumber();
        }

        public static string GetPersianNumber(this decimal data)
        {
            return data.ToString(CultureInfo.InvariantCulture).GetPersianNumber();
        }

        public static string GetPersianNumber(this byte data)
        {
            return data.ToString(CultureInfo.InvariantCulture).GetPersianNumber();
        }

        public static string ToPersianDateTime(this DateTime dateTime)
        {
            var year = dateTime.Year;
            var month = dateTime.Month;
            var day = dateTime.Day;
            var persianCalendar = new PersianCalendar();
            var pYear = persianCalendar.GetYear(new DateTime(year, month, day, new GregorianCalendar()));
            var pMonth = persianCalendar.GetMonth(new DateTime(year, month, day, new GregorianCalendar()));
            var pDay = persianCalendar.GetDayOfMonth(new DateTime(year, month, day, new GregorianCalendar()));
            return string.Format("{0}{1}{2}{1}{3}", pYear, "/", pMonth.ToString("00", CultureInfo.InvariantCulture),
                pDay.ToString("00", CultureInfo.InvariantCulture));
        }

        public static string ToPersianDateTimeNullable(this DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                var year = ((DateTime)dateTime).Year;
                var month = ((DateTime)dateTime).Month;
                var day = ((DateTime)dateTime).Day;
                var persianCalendar = new PersianCalendar();
                var pYear = persianCalendar.GetYear(new DateTime(year, month, day, new GregorianCalendar()));
                var pMonth = persianCalendar.GetMonth(new DateTime(year, month, day, new GregorianCalendar()));
                var pDay = persianCalendar.GetDayOfMonth(new DateTime(year, month, day, new GregorianCalendar()));
                return string.Format("{0}{1}{2}{1}{3}", pYear, "/", pMonth.ToString("00", CultureInfo.InvariantCulture),
                    pDay.ToString("00", CultureInfo.InvariantCulture));
            }
            return string.Format("{0}{1}{2}{1}{3}", "1300", "/", "01", "01");
        }

        public static DateTime ToGeorgeDateTime(this string userInput)
        {
            var en = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = en;

            var year = int.Parse(userInput.Substring(0, 4));
            var month = int.Parse(userInput.Substring(5, 2));
            var day = int.Parse(userInput.Substring(8, 2));
            var p = new PersianCalendar();
            var date = p.ToDateTime(year, month, day, 0, 0, 0, 0);
            return date;
        }

        public static DateTime? ToGeorgeDateTimeNullable(this string userInput)
        {
            var en = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = en;

            var year = int.Parse(userInput.Substring(0, 4));
            var month = int.Parse(userInput.Substring(5, 2));
            var day = int.Parse(userInput.Substring(8, 2));
            var p = new PersianCalendar();
            var date = p.ToDateTime(year, month, day, 0, 0, 0, 0);
            return date;
        }
    }
}