using FsCheck.Xunit;

namespace ScalaCheckBookExamplesInFsCheck2.Chapter6.CustomTestCaseSimplification
{
    [Arbitrary(typeof(CustomArbitrary))]
    public class Tests
    {
        [Property]
        public bool PropRewrite(Expression expr)
        {
            var rexpr = expr.Rewrite();
            return rexpr.Eval() == expr.Eval();
        }

        [Property(Skip = "This tests introduces a deliberate error in order to see shrinking in action")]
        public bool PropRewrite2(Expression expr)
        {
            var rexpr = expr.Rewrite(true);
            return rexpr.Eval() == expr.Eval();
        }

        // Add another failing test that suppresses shrinking ? (to compare shrinking vs not shrinking)
    }
}
