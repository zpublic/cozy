namespace Simple.Data.Interop
{
    using System;
    using System.Collections.Generic;

    public class Parser
    {
        private readonly IEnumerable<Token> _tokens;

        public Parser(IEnumerable<Token> tokens)
        {
            _tokens = tokens;
        }

        public AstNode Parse()
        {
            using (var e = _tokens.GetEnumerator())
            {
                e.MoveNext();
                return Parse(e).ToAstNode();
            }
        }

        private static TempNode Parse(IEnumerator<Token> e)
        {
            if (e.Current.Type == TokenType.Identifier)
            {
                var node = new TempNode { Type = AstNodeType.Identifier, Value = e.Current.Value };
                e.MoveNext();
                node.AddNode(Parse(e));
                return node;
            }
            if (e.Current.Type == TokenType.Dot)
            {
                var callNode = new TempNode { Type = AstNodeType.Call };
                callNode.AddNode(ParseCallNode(e));
                return callNode;
            }
            if (e.Current.Type == TokenType.String || e.Current.Type == TokenType.Number)
            {
                return new TempNode { Type = AstNodeType.Literal, Value = e.Current.Value };
            }

            if (e.MoveNext())
                return Parse(e);

            return null;
        }

        private static TempNode ParseCallNode(IEnumerator<Token> e)
        {
            TempNode currentNode;
            e.MoveNext();
            string identifier = e.Current.Value.ToString();
            e.MoveNext();
            if (e.Current.Type == TokenType.OpenParen)
            {
                currentNode = new TempNode { Type = AstNodeType.Method, Value = identifier };
                ParseArguments(e, currentNode);
            }
            else
            {
                currentNode = new TempNode { Type = AstNodeType.PropertyGetter, Value = identifier };
            }
            return currentNode;
        }

        private static void ParseArguments(IEnumerator<Token> e, TempNode methodNode)
        {
            e.MoveNext();
            while (e.Current.Type != TokenType.CloseParen)
            {
                methodNode.AddNode(Parse(e));
                e.MoveNext();
            }
        }
    }
}