namespace Simple.Data.Interop
{
    using System.Collections.Generic;
    using System.Linq;

    class TempNode
    {
        public object Value { get; set; }

        private readonly List<TempNode> _nodes = new List<TempNode>();

        public void AddNode(TempNode node)
        {
            if (node != null)
                _nodes.Add(node);
        }

        public AstNodeType Type { get; set; }

        public AstNode ToAstNode()
        {
            return new AstNode(Type, _nodes.Select(n => n.ToAstNode()).ToArray(), Value);
        }
    }
}