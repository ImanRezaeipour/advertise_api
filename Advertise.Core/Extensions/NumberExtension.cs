using System;

namespace Advertise.Core.Extensions
{
    public static class NumberExtension
    {
        #region Public Methods

        public static decimal? GetDiscount(this decimal? price, decimal? previousPrice)
        {
            try
            {
                if (previousPrice != null)
                {
                    decimal? a = Math.Abs(Convert.ToDecimal(previousPrice) - Convert.ToDecimal(price)) /
                                 Convert.ToDecimal(previousPrice) * 100;
                    string b = a.ToString();
                    decimal c = decimal.Parse(b);
                    return Math.Floor(Math.Ceiling(Math.Floor(c * 2) / 2));
                }
                return null;
            }
            catch (DivideByZeroException)
            {
                return 0;
            }
        }

        #endregion Public Methods
    }
}