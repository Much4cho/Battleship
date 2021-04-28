using System.Collections;
using System.Collections.Generic;

namespace Battleship.Utility
{
    public static class IEnumerableExtensions
    {
        //https://stackoverflow.com/questions/13709626/split-an-ienumerablet-into-fixed-sized-chunks-return-an-ienumerableienumerab
        //Lesson learned IEnumerator<T> inherits IDisposable while IEnumerator doesn't
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
        {
            using (var enumerator = source.GetEnumerator())
                while (enumerator.MoveNext())
                    yield return YieldBatchElements(enumerator, batchSize - 1);
        }

        private static IEnumerable<T> YieldBatchElements<T>(IEnumerator<T> source, int batchSize)
        {
            yield return source.Current;
            for (int i = 0; i < batchSize && source.MoveNext(); i++)
                yield return source.Current;
        }
    }
}
