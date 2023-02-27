using System;
using System.Collections.Generic;
namespace AlgosAndDataStructs.Algorithms
{
    public static class Linear
    {
        public static int LinearFind<T>(this IEnumerable<T> @list, T itemToFind)
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
