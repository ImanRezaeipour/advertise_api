using System.Text.RegularExpressions;

namespace Advertise.Core.Extensions
{
    public static class PersianProofWriter
    {
        public const char ArabicKeChar = (char)1603;
        public const char ArabicYeChar = (char)1610;
        public const char PersianKeChar = (char)1705;
        public const char PersianYeChar = (char)1740;

        public static string ApplyHalfSpaceRule(this string text)
        {
            //put zwnj between word and prefix (mi* nemi*)
            var phase1 = Regex.Replace(text, @"\s+(ن?می)\s+", @" $1‌");

            //put zwnj between word and suffix (*tar *tarin *ha *haye)
            var phase2 = Regex.Replace(phase1, @"\s+(تر(ی(ن)?)?|ها(ی)?)\s+", @"‌$1 ");
            return phase2;
        }

        public static string ApplyModeratePersianRules(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            if (!text.ContainsFarsi())
                return text;

            return text
                .ApplyPersianYeKe()
                .ApplyHalfSpaceRule()
                .YeHeHalfSpace()
                .CleanupZwnj()
                .CleanupExtraMarks();
        }

        public static string ApplyPersianYeKe(this string text)
        {
            return string.IsNullOrEmpty(text) ? string.Empty : text.Replace(ArabicYeChar, PersianYeChar).Replace(ArabicKeChar, PersianKeChar).Trim();
        }

        public static string CleanupExtraMarks(this string text)
        {
            var phase1 = Regex.Replace(text, @"(!){2,}", "$1");
            var phase2 = Regex.Replace(phase1, "(؟){2,}", "$1");
            return phase2;
        }

        public static string CleanupZwnj(this string text)
        {
            return Regex.Replace(text, @"\s+‌|‌\s+", " ");
        }

        public static bool ContainsFarsi(this string text)
        {
            return Regex.IsMatch(text, @"[\u0600-\u06FF]");
        }

        public static string YeHeHalfSpace(this string text)
        {
            return Regex.Replace(text, @"(\S)(ه[\s‌]+[یي])(\s)", "$1ه‌ی‌$3"); // fix zwnj
        }
    }
}