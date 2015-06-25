using System;
using System.Collections.Generic;
using System.Linq;
using FsCheck;

namespace ScalaCheckBookExamplesInFsCheck2.Chapter4.RunLengthEncoding
{
    using RLE = RunLengthEncoding;

    [FsCheck.Xunit.Arbitrary(typeof(MyArbitraties))]
    public class Tests
    {
        [FsCheck.Xunit.Property]
        public bool RoundTripTest(IEnumerable<Tuple<int, char>> r)
        {
            var original = r.ToList();
            var actual = RLE.RunLengthEnc(RLE.RunLengthDec(original));
            return actual.SequenceEqual(original);
        }

        private static Gen<IEnumerable<Tuple<int, char>>> GenOutput
        {
            get
            {
                return Gen.Sized(RleList);
            }
        }

        private static Gen<IEnumerable<Tuple<int, char>>> RleList(int size)
        {
            if (size <= 1)
            {
                return RleItem.Select(t => Enumerable.Repeat(t, 1));
            }

            return from tail in RleList(size - 1)
                   let c1 = tail.First().Item2
                   from head in RleItem.Where(t => t.Item2 != c1)
                   select Enumerable.Repeat(head, 1).Concat(tail);
        }

        private static Gen<Tuple<int, char>> RleItem
        {
            get
            {
                return from n in Gen.Choose(1, 20)
                       from c in GenLowerCaseChar
                       select Tuple.Create(n, c);
            }
        }

        private static Gen<char> GenLowerCaseChar
        {
            get { return Gen.Choose('a', 'z').Select(i => (char) i); }
        }

        private class ArbOutput : Arbitrary<IEnumerable<Tuple<int, char>>>
        {
            public override Gen<IEnumerable<Tuple<int, char>>> Generator
            {
                get { return GenOutput; }
            }
        }

        private class MyArbitraties
        {
            // ReSharper disable once UnusedMember.Local
            public static Arbitrary<IEnumerable<Tuple<int, char>>> ArbitraryOutput
            {
                get
                {
                    return new ArbOutput();
                }
            }
        }
    }
}
