using System;
using System.Collections.Generic;
using FsCheck;
using FsCheck.Xunit;

namespace ScalaCheckBookExamplesInFsCheck2.Chapter5
{
    public class Throws
    {
        [Property]
        public Property PropDivByZero(int n)
        {
            return Prop.throws<DivideByZeroException, int>(new Lazy<int>(() => n/(n - n)));
        }

        [Property]
        public Property PropListBounds(IList<string> xs, int i)
        {
            var inside = i >= 0 && i < xs.Count;
            var p1 = inside.ToProperty();
            var p2 = Prop.throws<ArgumentOutOfRangeException, string>(new Lazy<string>(() => xs[i]));
            return p1.Or(p2)
                .Classify(inside, "inside")
                .Classify(!inside, "outside");
        }
    }
}
