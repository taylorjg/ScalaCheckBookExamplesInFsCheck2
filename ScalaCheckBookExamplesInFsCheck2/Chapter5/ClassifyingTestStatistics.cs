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
            return (n + n == 2*n)
                .Classify(n%2 == 0, "even")
                .Classify(n%2 != 0, "odd")
                .Classify(n < 0, "neg")
                .Classify(n >= 0, "pos")
                .Classify(Math.Abs(n) > 50, "large");
        }
    }
}
