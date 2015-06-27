using System;
using FsCheck;
using FsCheck.Xunit;

namespace ScalaCheckBookExamplesInFsCheck2.Chapter5
{
    public class ClassifyingTestStatistics
    {
        [Property]
        public Property P(int n)
        {
            var p1 = n + n == 2*n;
            var p2 = p1.Classify(n%2 == 0, "even");
            var p3 = p2.Classify(n%2 != 0, "odd");
            var p4 = p3.Classify(n < 0, "neg");
            var p5 = p4.Classify(n >= 0, "pos");
            var p6 = p5.Classify(Math.Abs(n) > 50, "large");
            return p6;
        }
    }
}
