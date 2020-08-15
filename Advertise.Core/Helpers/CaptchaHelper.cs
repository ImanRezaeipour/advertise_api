using System;

namespace Advertise.Core.Helpers
{
    public static class CaptchaHelperExtensions
    {
        public static int CreateSalt()
        {
            var random = new Random();
            return random.Next(1000, 9999);
        }
    }
}