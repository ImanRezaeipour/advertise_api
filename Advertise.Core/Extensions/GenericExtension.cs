using System;
using System.Collections.Generic;
using System.Reflection;

namespace Advertise.Core.Extensions
{
    public static class GenericExtension
    {
        #region Public Methods

        public static List<string> GetStaticMembers<TSource>(TSource source)
        {
            var list = new List<string>();
            if (typeof(TSource).IsClass)
            {
                foreach (var Member in typeof(TSource).GetFields(BindingFlags.Static))
                {
                    list.Add(Member.Name);
                }
            }
            return list;
        }

        public static bool IsIn<TSource>(this TSource source, List<TSource> list)
        {
            return list.Contains(source);
        }

        public static bool IsNullOrDefault<T>(this T? value) where T : struct
        {
            return default(T).Equals(value.GetValueOrDefault());
        }

        public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult failureValue)
            where TInput : class
        {
            return o == null ? failureValue : evaluator(o);
        }

        #endregion Public Methods
    }
}