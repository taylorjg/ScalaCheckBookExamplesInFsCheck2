using System;
using System.Collections.Generic;
using System.Linq;
using FsCheck;
using FsCheck.Xunit;
using Flinq;

namespace ScalaCheckBookExamplesInFsCheck2.Chapter5
{
    public class CollectingTestStatistics
    {
        [Property]
        public Property PropSlice()
        {
            return Prop.ForAll(new Func<IList<int>, Property>(xs =>
            {
                return Prop.ForAll(Arb.From(Gen.Choose(0, xs.Count - 1)), n =>
                {
                    return Prop.ForAll(Arb.From(Gen.Choose(n, xs.Count)), m =>
                    {
                        var slice = xs.Slice(n, m).ToList();
                        string label;
                        switch (slice.Count)
                        {
                            case 0: label = "none"; break;
                            case 1: label = "one"; break;
                            default: label = (slice.Count == xs.Count) ? "whole" : "part"; break;
                        }
                        return xs.ContainsSlice(slice).Collect(label);
                    });
                });
            }));
        }
    }
}
