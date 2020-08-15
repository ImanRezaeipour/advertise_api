using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Advertise.Core.Extensions;

namespace Advertise.Core.Extensions
{

    public static class StringExtension
    {
        #region Private Fields

        private static readonly Regex MatchAllTags =
            new Regex(@"<(.|\n)*?>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        #endregion Private Fields

        #region Public Methods

        public static string AddSlug(this string url, string slug)
        {
            if (slug == null)
                return url;
            return url + "/" + slug.Replace(" ", "-");
        }

        public static string BeforeWord(this string str, string word)
        {
            if (word == null)
                return str;
            return word + str;
        }

        public static string DefaultIfNull(this string word)
        {
            return word ?? "";
        }

        public static string ExtractNumeric(this string code)
        {
            string variable = string.Empty;
            int value = 0;
            for (int i = 0; i < code.Length; i++)
            {
                if (Char.IsDigit(code[i]))
                    variable += code[i];
            }
            if (variable.Length > 0)
                value = int.Parse(variable);
            return (value + 1).ToString();
        }

        public static string GetExtension(this string word)
        {
            return Path.GetExtension(word);
        }

        public static string GetUserManagerErros(this IEnumerable<string> errors)
        {
            return errors.Aggregate(string.Empty, (current, error) => current + $"{error} \n");
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool IsValidSortDirection(this string sortDirection)
        {
            if (string.IsNullOrEmpty(sortDirection))
                return false;

            if (sortDirection != "Desc" || sortDirection != "Asc")
                return false;

            return true;
        }

        public static string PlusSlash(this string word)
        {
            return word + "/";
        }

        public static string PlusWord(this string word, string afterWord)
        {
            return word + afterWord;
        }

        public static string RemoveHtmlTags(this string text)
        {
            return string.IsNullOrEmpty(text) ? string.Empty : MatchAllTags.Replace(text, " ").Replace("&nbsp;", " ");
        }

        public static string RemoveTilde(this string word)
        {
            return word.Replace("~", "");
        }

        public static string Repeat(this string str, int count)
        {
            var result = string.Empty;
            for (int i = 0; i < count; i++)
            {
                result += str;
            }
            return result;
        }

        public static string StripHtml(this string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        public static string StripHtml(this IHtmlString input)
        {
            return Regex.Replace(input.ToString(), "<.*?>", String.Empty);
        }

        public static Dictionary<string, List<string>> ToQueryStringDictionary(this string queryString)
        {
            var queries = queryString.Replace("?", "").Split('&');
            if (queries.All(m => m == ""))
                return new Dictionary<string, List<string>>();

            var specifications = new Dictionary<string, List<string>>();
            for (int indexArray = 0; indexArray < queries.Length; indexArray++)
            {
                var key = queries[indexArray].Split('=')[0];
                var value = queries[indexArray].Split('=')[1];
                if (specifications.ContainsKey(key))
                    specifications[key].Add(value);
                else
                    specifications.Add(key, new List<string> { value });
            }

            return specifications;
        }

        public static int WordsCount(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return 0;
            }

            text = text.CleanTags().Trim();
            text = text.Replace("\t", " ");
            text = text.Replace("\n", " ");
            text = text.Replace("\r", " ");

            var words = text.Split(
                new[] { ' ', ',', ';', '.', '!', '"', '(', ')', '?', ':', '\'', '«', '»', '+', '-' },
                StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        public static string ZeroIfNull(this string word)
        {
            return word ?? "0";
        }


        public static string GetNameViewModel(this string name)
        {
            return name.Substring(name.IndexOf('<') + 1, name.IndexOf('>') - 1);
        }

        #endregion Public Methods
    }
}