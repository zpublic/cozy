using CozyEditor.Analysis.Lua;
using CozyEditor.Document;
using CozyEditor.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyEditor.Controls
{
    public partial class CodeTextBox
    {
        private void ReAnalysis()
        {
            Lines.Clear();

            Lines.FillWithLines(Text);

            var nowPlace    = Place.Zero;
            var lexical     = new LuaLexicalAnalysis();

            foreach (var line in Lines)
            {
                var tokens = lexical.Analysis(line.Content);
                if (tokens != null)
                {
                    foreach (var token in tokens)
                    {
                        line.Documents.Add(new DocumentBlock()
                        {
                            Content         = token.Content,
                            StartPlace      = nowPlace,
                            DocumentType    = (DocumentTypeEnum)Enum.Parse(typeof(DocumentTypeEnum), token.TokenType),
                        });
                        nowPlace = nowPlace.Offset(0, token.Length);
                    }
                }

                nowPlace = nowPlace.NextLine();
            }
        }
    }
}
