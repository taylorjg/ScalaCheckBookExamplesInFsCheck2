using System.Linq;
using FsCheck;

namespace ScalaCheckBookExamplesInFsCheck2.Chapter6.RecursiveGenerators
{
    public static class CustomGenerator
    {
        public static Gen<Tree<int>> GenIntTree
        {
            get { return GenTree(Arb.Generate<int>()); }
        }

        private static Gen<Tree<T>> GenTree<T>(Gen<T> genT)
        {
            return Gen.OneOf(GenLeaf(genT), GenNode(genT));
        }

        private static Gen<Tree<T>> GenLeaf<T>(Gen<T> genT)
        {
            return genT.Select(t => new Leaf<T>(t) as Tree<T>);
        }

        private static Gen<Tree<T>> GenNode<T>(Gen<T> genT)
        {
            return Gen.Sized(size =>
                from s in Gen.Choose(0, size)
                let g = Gen.Resize(size / (s + 1), GenTree(genT))
                from children in Gen.ListOf(s, g).Select(fsharpList => fsharpList.ToList())
                select new Node<T>(children) as Tree<T>);
        }
    }
}
