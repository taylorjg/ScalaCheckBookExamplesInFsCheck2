using FsCheck.Xunit;
using ScalaCheckBookExamplesInFsCheck2.Utils;

namespace ScalaCheckBookExamplesInFsCheck2.Chapter6.RecursiveGenerators
{
    public class Tests
    {
        [Property]
        public void IntTreeSample()
        {
            CustomGenerator.GenIntTree.DumpSamples();
        }
    }
}
