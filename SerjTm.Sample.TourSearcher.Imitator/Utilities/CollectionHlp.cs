using System;
using System.Collections.Generic;
using System.Text;

namespace SerjTm.Sample.TourSearcher.Imitator.Utilities
{
    public static class CollectionHlp
    {
        public static T ByRandom<T>(this T[] items, Random rnd) => items[rnd.Next(0, items.Length)];
        public static T ByRandom<T>(this IList<T> items, Random rnd) => items[rnd.Next(0, items.Count)];

    }
}
