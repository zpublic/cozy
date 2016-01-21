using System.Windows;
using System.Windows.Media;

namespace CozyEditor.Highlight.Model
{
    public class HighlightStyle
    {
        private static HighlightStyle _DefaultStyle = new HighlightStyle();
        public static HighlightStyle DefaultStyle
        {
            get { return _DefaultStyle; }
        }

        public string Name;
        public string FontName;
        public FontWeight FontWeight;
        public FontStyle FontStyle;
        public int FontSize;
        public Color FontColor;
        bool Underline;
        Color Foreground;
        Color Background;
    }
}
