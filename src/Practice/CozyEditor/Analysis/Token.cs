using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyEditor.Analysis
{
    public class Token
    {
        public string Source { get; set; }

        public int BeginPos { get; set; }

        public int Length { get; set; }

        private string _Content;
        public string Content
        {
            get
            {
                if (_Content == null)
                {
                    _Content = Source.Substring(BeginPos, Length);
                }
                return _Content;
            }
        }

        public string TokenType { get; set; }

        public Token(string source, int begin, int length, string type)
        {
            Source      = source;
            BeginPos    = begin;
            Length      = length;
            TokenType   = type;
        }

        public override string ToString()
        {
            return string.Format("[{0} : {1}]", TokenType, Content);
        }
    }
}
