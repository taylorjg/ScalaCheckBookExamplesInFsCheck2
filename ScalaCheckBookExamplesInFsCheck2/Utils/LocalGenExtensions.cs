using System;
using System.Linq;
using Flinq;
using FsCheck;

namespace ScalaCheckBookExamplesInFsCheck2.Utils
{
    public static class LocalGenExtensions
    {
        public static void DumpSamples<T>(this Gen<T> gen)
        {
            gen.DumpSamples(Formatters.DefaultItemFormatter<T>());
        }

        public static void DumpSamples<T>(this Gen<T> gen, Func<T, string> itemFormatter)
        {
            const int size = 10;
            const int n = 10;
            Console.WriteLine("Sample for generator {0} (size: {1}, n: {2})", gen, size, n);
            var sample = Gen.Sample(size, n, gen).AsEnumerable();
            sample.ForEach((T item, int index) => Console.WriteLine("Sample[{0}]: {1}", index, itemFormatter(item)));
        }
    }
}
