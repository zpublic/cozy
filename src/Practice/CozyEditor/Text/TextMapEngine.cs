using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyEditor.Text
{
    public class TextMapEngine
    {
        public static TextMapEngine Instance = new TextMapEngine();

        public string Map(string str)
        {
            str = LineBreakHolder.ConvertLineBreakToCurr(str);
            return str;
        }
    }
}
