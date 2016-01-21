using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CozyEditor.Text
{
    public static class LineBreakHolder
    {
        public static string DefaultLineBreak
        {
            get { return Environment.NewLine; }
        }

        private static string _CurrLineBreak = DefaultLineBreak;
        public static string CurrLineBreak
        {
            get { return _CurrLineBreak; }
            set { _CurrLineBreak = value; }
        }

        private static Regex _rgx = new Regex(@"\n|\r\n|\r");

        public static string ConvertLineBreakToCurr(string str)
        {
            return ConvertLineBreak(str, CurrLineBreak);
        }

        public static string ConvertLineBreak(string str, string lineBreak)
        {
            return _rgx.Replace(str, lineBreak);
        }

        public static string[] SplitWithLineBreak(string str, string lineBreak)
        {
            return Regex.Split(str, lineBreak);
        }

        public static string[] SplitWithLineBreakWithCurr(string str)
        {
            return SplitWithLineBreak(str, CurrLineBreak);
        }
    }
}
