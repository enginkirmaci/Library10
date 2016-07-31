using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Library10.Common.Extensions
{
    public static class ObservableObjectExtension
    {
        public static ObservableCollection<TSource> SortASC<TSource, TKey>(this ObservableCollection<TSource> source, Func<TSource, TKey> keySelector)
        {
            var sorted = source.OrderBy(keySelector).ToList();

            int ptr = 0;
            while (ptr < sorted.Count)
            {
                if (!source[ptr].Equals(sorted[ptr]))
                {
                    TSource t = source[ptr];
                    source.RemoveAt(ptr);
                    source.Insert(sorted.IndexOf(t), t);
                }
                else
                {
                    ptr++;
                }
            }

            return source;
        }

        public static ObservableCollection<TSource> SortDSC<TSource, TKey>(this ObservableCollection<TSource> source, Func<TSource, TKey> keySelector)
        {
            var sorted = source.OrderByDescending(keySelector).ToList();

            int ptr = 0;
            while (ptr < sorted.Count)
            {
                if (!source[ptr].Equals(sorted[ptr]))
                {
                    TSource t = source[ptr];
                    source.RemoveAt(ptr);
                    source.Insert(sorted.IndexOf(t), t);
                }
                else
                {
                    ptr++;
                }
            }

            return source;
        }
    }
}