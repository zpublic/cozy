using System.Collections.Generic;

namespace CozyArce.Tree.Base.Model
{
    public class CozyTree
    {
        public List<CozyBranch>     Branchs = new List<CozyBranch>();
        public List<CozyLeaf>       Leaves = new List<CozyLeaf>();
        public List<CozyFlower>     Flowers = new List<CozyFlower>();
    }
}
