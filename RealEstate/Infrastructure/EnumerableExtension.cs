namespace RealEstate.Infrastructure
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public static class EnumerableExtension
    {
        public static IEnumerable<T> Safe<T>(this IEnumerable<T> source)
            => source ?? Enumerable.Empty<T>();

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<int, T> action)
        {
            int index = 0;
            foreach (var item in source)
                action(index++, item);
        }
    }
}