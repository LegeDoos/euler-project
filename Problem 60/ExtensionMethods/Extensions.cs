using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_60.ExtensionMethods
{
    static internal class Extensions
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int splitSize)
        {
            using (IEnumerator<T> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    yield return InnerSplit(enumerator, splitSize);
                }
            }

        }

        private static IEnumerable<T> InnerSplit<T>(IEnumerator<T> enumerator, int splitSize)
        {
            int count = 0;
            do
            {
                count++;
                yield return enumerator.Current;
            }
            while (count % splitSize != 0
                 && enumerator.MoveNext());
        }
    }
}
