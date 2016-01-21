using CozyEditor.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyEditor.Document
{
    public class LineCollection : IEnumerable<Line>
    {
        public List<Line> Lines { get; set; } = new List<Line>();

        public int Count
        {
            get
            {
                return Lines.Count;
            }
        }

        public IEnumerator<Line> GetEnumerator()
        {
            return Lines.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Lines.GetEnumerator();
        }

        public void Clear()
        {
            Lines.Clear();
        }

        public void FillWithLines(string text)
        {
            int b       = 0;
            var strs    = LineBreakHolder.SplitWithLineBreakWithCurr(text);

            foreach(var obj in strs)
            {
                Lines.Add(new Line()
                {
                    Content     = obj,
                    BeginPoint  = b,
                    Source      = text,
                });
                b += (obj.Length + LineBreakHolder.CurrLineBreak.Length);
            }
        }

        public Line this[int index]
        {
            get
            {
                return Lines[index];
            }
        }
    }
}
