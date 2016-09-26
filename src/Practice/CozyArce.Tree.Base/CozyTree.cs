using System.Collections.Generic;
using System.Windows;

namespace CozyArce.Tree.Base
{
    public class CozyBranch
    {
        public Point begin;
        public Point end;
        public short width;
    }

    public class CozyLeaf
    {
        public Point begin;
        public Point end;
    }

    public class CozyFlower
    {
        public Point pos;
        public short size;
    }

    public class CozyTree
    {
        public List<CozyBranch>     Branchs = new List<CozyBranch>();
        public List<CozyLeaf>       Leaves = new List<CozyLeaf>();
        public List<CozyFlower>     Flowers = new List<CozyFlower>();
    }
}
