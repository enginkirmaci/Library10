using System;
using System.Collections.Generic;

namespace Library10.Common.Extensions
{
    public static class IEnumerableExtension
    {
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}