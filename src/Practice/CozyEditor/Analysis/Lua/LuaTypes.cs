using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyEditor.Analysis.Lua
{
    public enum Status : uint
    {
        Begin,
        Empty,
        Comment,
        Number,
        String,
        Operator,
        Keyword,
        Idendity,
        Error,
    }
}
