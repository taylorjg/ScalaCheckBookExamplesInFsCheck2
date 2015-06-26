using System;
using System.Collections.Generic;
using System.Linq;
using FsCheck.Xunit;

namespace ScalaCheckBookExamplesInFsCheck2.Chapter4.RunLengthEncoding
{
    using RLE = RunLengthEncoding;

    [Arbitrary(typeof(CustomArbitrary))]
    public class Tests
    {
        [Property]
        public bool RoundTripTest(IEnumerable<Tuple<int, char>> r)
        {
            var original = r.ToList();
            var actual = RLE.RunLengthEnc(RLE.RunLengthDec(original));
            return actual.SequenceEqual(original);
        }
    }
}
