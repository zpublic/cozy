using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CozyEditor.Text
{
    public struct Place
    {
        private static Place _Zero = new Place(0, 0);
        public static Place Zero { get { return _Zero; } }

        public int LineNum { get; set; }

        public int CharNum { get; set; }

        public Place(Place o)
        {
            LineNum = o.LineNum;
            CharNum = o.CharNum;
        }

        public Place(int _line, int _char)
        {
            LineNum = _line;
            CharNum = _char;
        }

        public Point ToPoint(int size)
        {
            return new Point(CharNum * size, LineNum * size);
        }

        public Place NextLine()
        {
            return new Place(LineNum + 1, 0);
        }

        public Place Offset(int _line, int _char)
        {
            return new Place(LineNum + _line, CharNum + _char);
        }

        public Place LastLine(int _char)
        {
            return new Place(LineNum - 1, _char);
        }
    }
}
