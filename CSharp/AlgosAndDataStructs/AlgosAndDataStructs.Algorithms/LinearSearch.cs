using System;
using System.Collections.Generic;
namespace AlgosAndDataStructs.Algorithms
{
    public static class LinearSearch
    {
        /// <summary>
        /// O(n)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="itemToFind"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static int Find<T>(this IEnumerable<T> @list, T itemToFind)
        {
            if (@list is null)
            {
                throw new ArgumentNullException(nameof(@list));
            }

            for (int i = 0; i < @list.Count(); ++i)
            {
                T? element = @list.ElementAtOrDefault(i);

                if (element is not null && element.Equals(itemToFind))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
