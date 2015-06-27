using System;
using System.Collections.Generic;
using System.Linq;
using FsCheck;
using FsCheck.Xunit;

namespace ScalaCheckBookExamplesInFsCheck2.Chapter5.Interleaving
{
    public class Tests
    {
        [Property]
        public Property PropInterleave(IList<int> xs, IList<int> ys)
        {
            var res = Interleaving.Interleave(xs, ys);
            var @is = Enumerable.Range(0, Math.Min(xs.Count, ys.Count)).ToList();
            var p1 = (xs.Count + ys.Count == res.Count()).Label("length");
            var p2 = xs.SequenceEqual(@is.Select(i => res[2 * i]).Concat(res.Skip(2 * ys.Count))).Label("zip xs");
            var p3 = ys.SequenceEqual(@is.Select(i => res[2 * i + 1]).Concat(res.Skip(2 * xs.Count))).Label("zip ys");
            return p1.And(p2).And(p3);
        }
    }
}
