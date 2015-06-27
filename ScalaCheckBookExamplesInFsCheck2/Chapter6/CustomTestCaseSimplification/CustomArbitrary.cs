using System;
using System.Collections.Generic;
using System.Linq;
using FsCheck;

namespace ScalaCheckBookExamplesInFsCheck2.Chapter6.CustomTestCaseSimplification
{
    public class CustomArbitrary
    {
        private static Gen<Expression> GenExpr
        {
            get
            {
                return Gen.Sized(sz =>
                    Gen.Frequency(new[]
                    {
                        Tuple.Create(Math.Max(sz, 1), GenConst),
                        Tuple.Create(sz - (int) Math.Sqrt(sz), Gen.Resize(sz/2, GenAdd)),
                        Tuple.Create(sz - (int) Math.Sqrt(sz), Gen.Resize(sz/2, GenMul))
                    }));
            }
        }

        private static Gen<Expression> GenConst
        {
            get { return Gen.Choose(0, 10).Select(value => new Const(value) as Expression); }
        }

        private static Gen<Expression> GenAdd
        {
            get
            {
                return
                    from e1 in GenExpr
                    from e2 in GenExpr
                    select new Add(e1, e2) as Expression;
            }
        }

        private static Gen<Expression> GenMul
        {
            get
            {
                return
                    from e1 in GenExpr
                    from e2 in GenExpr
                    select new Mul(e1, e2) as Expression;
            }
        }

        private static IEnumerable<Expression> ShrinkExpr(Expression e)
        {
            return e.Match(ConstShrinks, AddShrinks, MulShrinks);
        }

        private static IEnumerable<Expression> ConstShrinks(Const e)
        {
            return Arb.Shrink(e.Value).Select(value => new Const(value) as Expression);
        }

        private static IEnumerable<Expression> AddShrinks(Add e)
        {
            yield return e.Expression1;
            yield return e.Expression2;
            foreach (var x in ShrinkExpr(e.Expression1).Select(shrink => new Add(shrink, e.Expression2) as Expression)) yield return x;
            foreach (var x in ShrinkExpr(e.Expression2).Select(shrink => new Add(e.Expression1, shrink) as Expression)) yield return x;
        }

        private static IEnumerable<Expression> MulShrinks(Mul e)
        {
            yield return e.Expression1;
            yield return e.Expression2;
            foreach (var x in ShrinkExpr(e.Expression1).Select(shrink => new Mul(shrink, e.Expression2) as Expression)) yield return x;
            foreach (var x in ShrinkExpr(e.Expression2).Select(shrink => new Mul(e.Expression1, shrink) as Expression)) yield return x;
        }

        private class ArbExpr : Arbitrary<Expression>
        {
            public override Gen<Expression> Generator
            {
                get { return GenExpr; }
            }

            public override IEnumerable<Expression> Shrinker(Expression expr)
            {
                return ShrinkExpr(expr);
            }
        }

        // ReSharper disable once UnusedMember.Global
        public static Arbitrary<Expression> ArbitraryExpr
        {
            get
            {
                return new ArbExpr();
            }
        }
    }
}
