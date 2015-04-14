namespace Simple.Data.Interop
{
    using System.Collections.Generic;

    class SingleCharTokens
    {
        private static readonly Dictionary<char, Token> Tokens = new Dictionary<char, Token>
                                                                     {
                                                                         { '(', new Token(TokenType.OpenParen) },
                                                                         { ')', new Token(TokenType.CloseParen) },
                                                                         { '.', new Token(TokenType.Dot) },
                                                                         { ',', new Token(TokenType.Comma) },
                                                                         { '!', new Token(TokenType.BangSign) },
                                                                         { '=', new Token(TokenType.EqualSign) },
                                                                         { '<', new Token(TokenType.LessThanSign) },
                                                                         { '>', new Token(TokenType.GreaterThanSign) },
                                                                         { ':', new Token(TokenType.Colon) },
                                                                         { '+', new Token(TokenType.PlusSign) },
                                                                         { '-', new Token(TokenType.MinusSign) },
                                                                         { '*', new Token(TokenType.Asterisk) },
                                                                         { '/', new Token(TokenType.ForwardSlash) },
                                                                         { '%', new Token(TokenType.Percent) },
                                                                         { '&', new Token(TokenType.Ampersand) },
                                                                         { '|', new Token(TokenType.Pipe) },
                                                                     };

        public bool TryGetToken(char ch, out Token token)
        {
            return Tokens.TryGetValue(ch, out token);
        }
    }
}