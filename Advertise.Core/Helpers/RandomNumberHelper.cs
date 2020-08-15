using System;
using System.Security.Cryptography;

namespace Advertise.Core.Helpers
{
    public static class RandomNumber
    {
        #region Private Fields

        private static readonly RNGCryptoServiceProvider Rand = new RNGCryptoServiceProvider();
        private static readonly byte[] Randb = new byte[4];

        #endregion Private Fields

        #region Public Methods

        public static int Next(int max)
        {
            return Next() % (max + 1);
        }

        public static int Next(int min, int max)
        {
            return Next(max - min) + min;
        }

        #endregion Public Methods

        #region Private Methods

        private static int Next()
        {
            Rand.GetBytes(Randb);
            var value = BitConverter.ToInt32(Randb, 0);
            return Math.Abs(value);
        }

        #endregion Private Methods
    }
}