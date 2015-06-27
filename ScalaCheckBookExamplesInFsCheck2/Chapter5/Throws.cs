using System;
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
    }
}
