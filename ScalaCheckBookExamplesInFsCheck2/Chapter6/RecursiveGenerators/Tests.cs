using System;
using FsCheck;
using FsCheck.Xunit;

namespace ScalaCheckBookExamplesInFsCheck2.Chapter6.RecursiveGenerators
{
    public class Tests
    {
        [Property]
        public void Sample()
        {
            var samples = Gen.Sample(10, 10, CustomGenerator.GenIntTree);
            foreach (var sample in samples)
            {
                Console.WriteLine(sample);
            }
        }
    }
}
