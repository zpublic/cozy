using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyEditor.Analysis
{
    public abstract class LexicalAnalysis
    {
        public abstract List<Token> Analysis(string text);
    }
}
