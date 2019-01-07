using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static  class EnumerableExtensions
    {

        /// <summary>
        /// 通过使用默认的相等比较器对值进行比较返回序列中的非重复元素
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.Distinct(new CommonEqualityComparer<TSource, TKey>(keySelector));
        }

        private class CommonEqualityComparer<TSource, TKey> : IEqualityComparer<TSource>
        {
            private Func<TSource, TKey> keySelector;
            private IEqualityComparer<TKey> comparer;

            public CommonEqualityComparer(Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
            {
                this.keySelector = keySelector;
                this.comparer = comparer;
            }

            public CommonEqualityComparer(Func<TSource, TKey> keySelector)
                : this(keySelector, EqualityComparer<TKey>.Default)
            { }

            public bool Equals(TSource source, TSource key)
            {
                return comparer.Equals(keySelector(source), keySelector(key));
            }

            public int GetHashCode(TSource source)
            {
                return comparer.GetHashCode(keySelector(source));
            }

        }
    }
}
