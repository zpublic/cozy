using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnLinq.Details
{
    public static class CustomSequenceOperatorsExt
    {
        public static IEnumerable<T> Combine<T>(this IEnumerable<T> first, IEnumerable<T> second,
        Func<T, T, T> func)
        {
            using (IEnumerator<T> e1 = first.GetEnumerator(), e2 = second.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext())
                {
                    yield return func(e1.Current, e2.Current);
                }
            }
        }
    } 
}
