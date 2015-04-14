using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Data.Interop
{
    using System.Collections.ObjectModel;

    public class AstNode
    {
        private readonly AstNodeType _type;
        private readonly object _value;
        private readonly ReadOnlyCollection<AstNode> _nodes;

        public AstNode(AstNodeType type) : this(type, null)
        {
        }

        public AstNode(AstNodeType type, object value) : this(type, new AstNode[0], value)
        {
        }

        public AstNode(AstNodeType type, IList<AstNode> nodes) : this(type, nodes, null)
        {
        }

        public AstNode(AstNodeType type, IList<AstNode> nodes, object value)
        {
            _type = type;
            _nodes = new ReadOnlyCollection<AstNode>(nodes);
            _value = value;
        }

        public object Value
        {
            get { return _value; }
        }

        public ReadOnlyCollection<AstNode> Nodes
        {
            get { return _nodes; }
        }

        public AstNodeType Type
        {
            get { return _type; }
        }
    }
}
