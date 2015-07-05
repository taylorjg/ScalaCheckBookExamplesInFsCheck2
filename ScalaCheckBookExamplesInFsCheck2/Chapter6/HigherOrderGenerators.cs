using System;
using FsCheck;
using FsCheck.Xunit;
using ScalaCheckBookExamplesInFsCheck2.Utils;

namespace ScalaCheckBookExamplesInFsCheck2.Chapter6
{
    public class HigherOrderGenerators
    {
        [Property]
        public void NumbersSample()
        {
            var genNumbers = Gen.Sequence(Gen.Choose(1, 10), Gen.Constant(20), Gen.Constant(30));
            genNumbers.DumpSamples(Formatters.FormatCollection);
        }

        static readonly Gen<int> EvenNumberGen =
            from n in Gen.Choose(2, 100000)
            select 2 * n;

        static readonly Gen<int> OddNumberGen =
            from n in Gen.Choose(0, 100000)
            select 2 * n + 1;

        static readonly Gen<int> NumberGen = Gen.Frequency(new[]
            {
                Tuple.Create(1, OddNumberGen),
                Tuple.Create(2, EvenNumberGen),
                Tuple.Create(4, Gen.Constant(0))
            });

        [Property]
        public Property PropNumberGen()
        {
            return Prop.ForAll(Arb.From(NumberGen), n =>
            {
                string l;
                if (n == 0) l = "zero";
                else if (n % 2 == 0) l = "even";
                else l = "odd";
                return true.ToProperty().Collect(l);
            });
        }

        [Property]
        public void NotZeroSample()
        {
            var genNotZero = Gen.OneOf(Gen.Choose(-10, -1), Gen.Choose(1, 10));
            genNotZero.DumpSamples();
        }

        [Property]
        public void VowelSample()
        {
            var genVowel = Gen.OneOf(
                Gen.Constant('a'),
                Gen.Constant('e'),
                Gen.Constant('i'),
                Gen.Constant('o'),
                Gen.Constant('u'),
                Gen.Constant('y'));
            genVowel.DumpSamples();
        }
    }
}
