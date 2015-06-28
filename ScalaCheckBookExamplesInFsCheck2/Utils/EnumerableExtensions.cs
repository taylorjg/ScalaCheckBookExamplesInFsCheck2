using System.Collections.Generic;
using System.Linq;

namespace ScalaCheckBookExamplesInFsCheck2.Utils
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ToSingleton<T>(this T t)
        {
            return Enumerable.Repeat(t, 1);
        }
    }
}
