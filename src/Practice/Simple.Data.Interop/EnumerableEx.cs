using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Data.Interop
{
    public static class EnumerableEx
    {
        public static int[] MultiCount<T>(this IEnumerable<T> source, params Func<T,bool>[] predicates)
        {
            int predicateCount = predicates.Count();
            int[] counts = Enumerable.Repeat(0, predicateCount).ToArray();

            foreach (var item in source)
            {
                for (int i = 0; i < predicateCount; i++)
                {
                    if (predicates[i](item))
                    {
                        ++counts[i];
                    }
                }
            }

            return counts;
        }
    }
}
