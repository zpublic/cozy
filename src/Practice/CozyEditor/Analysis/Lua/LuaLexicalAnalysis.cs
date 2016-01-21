using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyEditor.Analysis.Lua
{
    public class LuaLexicalAnalysis : LexicalAnalysis
    {
        private static HashSet<string> KeywordSet { get; set; } = new HashSet<string>();
        private static string OperatorIndexList { get; set; }

        static LuaLexicalAnalysis()
        {
            KeywordSet = new HashSet<string>
            {
                "and",
                "break",
                "do",
                "else",
                "elseif",
                "end",
                "false",
                "for",
                "function",
                "if",
                "in",
                "local",
                "nil",
                "not",
                "or",
                "repeat",
                "return",
                "then",
                "true",
                "until",
                "while",
            };

            OperatorIndexList = @"+-*/%^#(){}[];:,";
        }

        public override List<Token> Analysis(string text)
        {
            int pos             = 0;
            Status status       = Status.Begin;
            List<Token> result  = new List<Token>();

            while (pos < text.Length)
            {
                switch (status)
                {
                    case Status.Begin:
                        status = OnBegin(text, ref pos, result);
                        break;
                    case Status.Empty:
                        status = OnEmpty(text, ref pos, result);
                        break;
                    case Status.Comment:
                        status = OnComment(text, ref pos, result);
                        break;
                    case Status.Idendity:
                        status = OnIdendity(text, ref pos, result);
                        break;
                    case Status.Keyword:
                        status = OnKeyword(text, ref pos, result);
                        break;
                    case Status.Number:
                        status = OnNumber(text, ref pos, result);
                        break;
                    case Status.Operator:
                        status = OnOperator(text, ref pos, result);
                        break;
                    case Status.String:
                        status = OnString(text, ref pos, result);
                        break;
                    case Status.Error:
                        status = OnError(text, ref pos, result);
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        private Status OnBegin(string text, ref int pos, List<Token> result)
        {
            Status retVal = Status.Begin;

            if (char.IsWhiteSpace(text[pos]))
            {
                retVal = Status.Empty;
            }
            else if (text[pos] == '/')
            {
                retVal = Status.Comment;
            }
            else if (text[pos] == '"')
            {
                retVal = Status.String;
            }
            else if (text[pos] == '_')
            {
                retVal = Status.Idendity;
            }
            else if (char.IsDigit(text[pos]))
            {
                retVal = Status.Number;
            }
            else if (char.IsLetter(text[pos]))
            {
                retVal = Status.Keyword;
            }
            else
            {
                retVal = Status.Operator;
            }
            return retVal;
        }

        private Status OnComment(string text, ref int pos, List<Token> result)
        {
            Status retVal = Status.Begin;

            if (text[pos] == '/' && pos + 1 < text.Length && text[pos + 1] == '/')
            {
                int m = 2;
                while (pos + m < text.Length && text[pos + m] != '\n')
                {
                    ++m;
                }
                result.Add(new Token(text, pos, m, Status.Comment.ToString()));
                pos += m;
            }
            else if (text[pos] == '/' && pos + 1 < text.Length && text[pos + 1] == '*')
            {
                int m = 2;
                while (pos + m < text.Length && text[pos + m] != '*' && pos + m + 1 <text.Length && text[pos + m + 1] != '/')
                {
                    ++m;
                }

                if(pos + m + 1 >= text.Length)
                {
                    retVal = Status.Error;
                }
                else
                {
                    result.Add(new Token(text, pos, m + 2, Status.Comment.ToString()));
                    pos += (m + 2);
                }
            }
            else
            {
                retVal = Status.Operator;
            }
            return retVal;
        }

        private Status OnNumber(string text, ref int pos, List<Token> result)
        {
            Status retVal = Status.Begin;

            if (char.IsDigit(text[pos]))
            {
                int m = 1;
                while (pos + m < text.Length && (char.IsDigit(text[pos + m]) || text[pos + m] == '.'))
                {
                    ++m;
                }
                result.Add(new Token(text, pos, m, Status.Number.ToString()));
                pos += m;
            }
            else
            {
                retVal = Status.Error;
            }

            return retVal;
        }

        private Status OnString(string text, ref int pos, List<Token> result)
        {
            Status retVal = Status.Begin;

            if (text[pos] == '"')
            {
                int m = 1;
                while (pos + m < text.Length && text[pos + m] != '"')
                {
                    ++m;
                }
                result.Add(new Token(text, pos + 1, m, Status.String.ToString()));
                pos += m;
            }
            else
            {
                retVal = Status.Operator;
            }

            return retVal;
        }

        private Status OnOperator(string text, ref int pos, List<Token> result)
        {
            Status retVal = Status.Begin;

            if (OperatorIndexList.IndexOf(text[pos]) != -1)
            {
                result.Add(new Token(text, pos, 1, Status.Operator.ToString()));
                pos++;
            }
            else if (text[pos] == '=')
            {
                if (pos + 1 < text.Length && text[pos + 1] == '=')
                {
                    result.Add(new Token(text, pos, 2, Status.Operator.ToString()));
                    pos += 2;
                }
                else
                {
                    result.Add(new Token(text, pos, 1, Status.Operator.ToString()));
                    pos++;
                }
            }
            else if (text[pos] == '~' && pos + 1 < text.Length && text[pos + 1] == '=')
            {
                result.Add(new Token(text, pos, 2, Status.Operator.ToString()));
                pos += 2;
            }
            else if (text[pos] == '<')
            {
                if (pos + 1 < text.Length && text[pos + 1] == '=')
                {
                    result.Add(new Token(text, pos, 2, Status.Operator.ToString()));
                    pos += 2;
                }
                else
                {
                    result.Add(new Token(text, pos, 1, Status.Operator.ToString()));
                    pos++;
                }
            }
            else if (text[pos] == '>')
            {
                if (pos + 1 < text.Length && text[pos + 1] == '=')
                {
                    result.Add(new Token(text, pos, 2, Status.Operator.ToString()));
                    pos += 2;
                }
                else
                {
                    result.Add(new Token(text, pos, 1, Status.Operator.ToString()));
                    pos++;
                }
            }
            else
            {
                retVal = Status.Error;
            }

            return retVal;
        }

        private Status OnKeyword(string text, ref int pos, List<Token> result)
        {
            Status retVal = Status.Begin;

            if (char.IsLetter(text[pos]))
            {
                int m = 1;
                while (pos + m < text.Length && char.IsLetter(text[pos + m]))
                {
                    ++m;
                }
                if (KeywordSet.Contains(text.Substring(pos, m)))
                {
                    result.Add(new Token(text, pos, m, Status.Keyword.ToString()));
                    pos += m;
                }
                else
                {
                    retVal = Status.Idendity;
                }
            }
            else
            {
                retVal = Status.Error;
            }
            return retVal;
        }

        private Status OnIdendity(string text, ref int pos, List<Token> result)
        {
            Status retVal = Status.Begin;

            if (char.IsLetter(text[pos]) || text[pos] == '_')
            {
                int m = 1;
                while (pos + m < text.Length && (char.IsLetterOrDigit(text[pos + m]) || text[pos + m] == '_'))
                {
                    ++m;
                }
                result.Add(new Token(text, pos, m, Status.Idendity.ToString()));
                pos += m;
            }
            else
            {
                retVal = Status.Error;
            }

            return retVal;
        }

        private Status OnError(string text, ref int pos, List<Token> result)
        {
            int m = 1;
            while(true)
            {
                if (pos + m >= text.Length || 
                    char.IsWhiteSpace(text[pos + m]) ||
                    char.IsLetterOrDigit(text[pos + m]) ||
                    OperatorIndexList.IndexOf(text[pos + m]) != -1 ||
                    text[pos + m] == '/' ||
                    text[pos + m] == '"' ||
                    text[pos + m] == '_' ||
                    text[pos + m] == '~' ||
                    text[pos + m] == '=' ||
                    text[pos + m] == '<' ||
                    text[pos + m] == '>')
                {
                    break;
                }
                ++m;
            }
            result.Add(new Token(text, pos, m, Status.Error.ToString()));
            pos += m;
            return Status.Begin;
        }

        private Status OnEmpty(string text, ref int pos, List<Token> result)
        {
            int m = 0;
            while(pos + m < text.Length && char.IsWhiteSpace(text[pos + m]))
            {
                ++m;
            }
            result.Add(new Token(text, pos, m, Status.Error.ToString()));
            pos += m;
            return Status.Begin;
        }
    }
}
