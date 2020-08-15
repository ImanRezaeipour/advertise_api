using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Advertise.Core.Managers.Node
{
    public static class NodeExtensions
    {
        #region Public Methods

        public static IEnumerable<TSource> Duplicates<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            var grouped = source.GroupBy(selector);
            var moreThen1 = grouped.Where(i => i.IsMultiple());

            return moreThen1.SelectMany(i => i);
        }

        public static string FormatInvariant(this string text, params object[] parameters)
        {
            // This is not the "real" implementation, but that would go out of Scope
            return string.Format(CultureInfo.InvariantCulture, text, parameters);
        }

        public static bool IsMultiple<T>(this IEnumerable<T> source)
        {
            var enumerator = source.GetEnumerator();
            return enumerator.MoveNext() && enumerator.MoveNext();
        }

        public static IEnumerable<T> ToIEnumarable<T>(this T item)
        {
            yield return item;
        }

        public static IEnumerable<T> Values<T>(this IEnumerable<NodeManager<T>> nodes)
        {
            return nodes.Select(n => n.Value);
        }

        #endregion Public Methods
    }
}