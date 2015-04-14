namespace Simple.Data.Interop
{
    public class Token
    {
        private readonly TokenType _tokenType;
        private readonly object _value;

        public Token(TokenType tokenType) : this(tokenType, null)
        {
        }

        public Token(TokenType tokenType, object value)
        {
            _tokenType = tokenType;
            _value = value;
        }

        public object Value
        {
            get { return _value; }
        }

        public TokenType Type
        {
            get { return _tokenType; }
        }

        public bool Is(TokenType type)
        {
            return Type == type;
        }
    }
}