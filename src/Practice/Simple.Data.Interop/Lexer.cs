using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Data.Interop
{
    using System.Text.RegularExpressions;

    public class Lexer
    {
        private static readonly Regex StringExtractor = new Regex(@"""(?:\\""|.)*?""");
        private static readonly Regex WhitespaceEliminator = new Regex(@"\s+");
        private static readonly SingleCharTokens Singles = new SingleCharTokens();
        private readonly string _source;
        private readonly Dictionary<string, string> _strings = new Dictionary<string, string>(); 

        public Lexer(string source)
        {
            _source = source;
        }

        public IEnumerable<Token> GetTokens()
        {
            var withoutStrings = StringExtractor.Replace(_source, HoldString);
            var singleSpaced = WhitespaceEliminator.Replace(withoutStrings, " ");

            var buffer = new StringBuilder();
            foreach (var ch in singleSpaced)
            {
                if (ch == ' ')
                {
                    if (buffer.Length > 0) yield return StringToToken(buffer);
                    continue;
                }
                if (ch == '.' && buffer.Length > 0 && char.IsDigit(buffer[0]))
                {
                    buffer.Append(ch);
                    continue;
                }
                Token token;
                if (Singles.TryGetToken(ch, out token))
                {
                    if (buffer.Length > 0) yield return StringToToken(buffer);
                    yield return token;
                }
                else
                {
                    buffer.Append(ch);
                }
            }

            if (buffer.Length > 0) yield return StringToToken(buffer);
        }

        private Token StringToToken(StringBuilder builder)
        {
            var str = builder.ToString();
            builder.Clear();
            if (str.StartsWith("{"))
            {
                if (!str.EndsWith("}"))
                {
                    throw new InvalidOperationException();
                }
                return new Token(TokenType.String, _strings[str]);
            }
            if (char.IsDigit(str[0]))
            {
                return new Token(TokenType.Number, str);
            }
            return new Token(TokenType.Identifier, str);
        }

        private string HoldString(Match match)
        {
            var key = string.Concat("{", _strings.Count, "}");
            var value = match.Value.Replace(@"\""", @"""");

            _strings.Add(key, value.Substring(1, value.Length - 2));
            return key;
        }

        public static int FindIndexOfOpeningToken(Token[] tokens, int startIndex, TokenType openingType)
        {
            var closingType = tokens[startIndex].Type;
            int nestCount = 0;

            for (int i = startIndex; i >= 0; i--)
            {
                if (tokens[i].Type == closingType)
                {
                    ++nestCount;
                }
                else if (tokens[i].Type == openingType)
                {
                    --nestCount;
                }
                if (nestCount == 0)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
