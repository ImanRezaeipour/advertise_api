using DNTPersianUtils.Core;
using System;
using System.Threading;

namespace Advertise.Core.Extensions
{
    public static class CastExtension
    {
        #region Public Methods

        public static string CastToRegularCurrency(this decimal? input)
        {
            return input == null ? "0" : Convert.ToDecimal(input).ToString("N0");
        }

        public static string CastToRegularCurrency(this string input)
        {
            return input == null ? "0" : Convert.ToDecimal(input).ToString("N0");
        }

        public static string CastToRegularCurrency(this decimal input)
        {
            return Convert.ToDecimal(input).ToString("N0");
        }

        public static string CastToRegularDate(this DateTime? input)
        {
            return input == null ? "" : input.ToPersianDateTextify();
        }

        public static string CastToRegularDate(this DateTime input)
        {
            if (Thread.CurrentThread.CurrentCulture.Name == "fa-IR")
                return FriendlyPersianDateUtils.ToPersianDateTextify(input.GetPersianYear(), input.GetPersianMonth(), input.GetPersianDayOfMonth());
            return input.ToPersianDateTextify();
        }

        public static string CastToRelativeFullDate(this DateTime? input)
        {
            return input == null ? "" : input.ToFriendlyPersianDateTextify();
        }

        public static string CastToRelativeFullDate(this DateTime input)
        {
            return input.ToFriendlyPersianDateTextify();
        }

        public static string CastToShortDateTime(this DateTime input)
        {
            return input.ToShortPersianDateTimeString();
        }

        public static string CastToShortDateTime(this DateTime? input)
        {
            return input == null ? "" : input.ToShortPersianDateTimeString();
        }

        public static string CastToSlug(this string input)
        {
            return input?.Replace(" ", "-") ?? "";
        }

        #endregion Public Methods
    }
}