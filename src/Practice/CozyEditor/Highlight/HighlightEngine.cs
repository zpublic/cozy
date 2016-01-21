using CozyEditor.Document;
using CozyEditor.Highlight.Model;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace CozyEditor.Highlight
{
    public class HighlightEngine
    {
        public HighlightStyle DefaultStyle { get; set; } = new HighlightStyle() { FontName = "宋体", FontSize = 14, FontColor = Colors.Black};

        public HighlightStyle CommentStyle { get; set; } = new HighlightStyle() { FontName = "宋体", FontSize = 14, FontColor = Colors.Green};

        public HighlightStyle KeywordStyle { get; set; } = new HighlightStyle() { FontName = "宋体", FontSize = 14, FontColor = Colors.Blue};

        public static readonly HighlightEngine Instance = new HighlightEngine();

        public void Init()
        {

        }

        public FormattedText DrawDocument(DocumentBlock doc)
        {
            HighlightStyle style = null;
            if (doc.DocumentType == DocumentTypeEnum.Comment)
            {
                style = CommentStyle;
            }
            else if(doc.DocumentType == DocumentTypeEnum.Keyword)
            {
                style = KeywordStyle;
            }
            else
            {
                style = DefaultStyle;
            }

            return new FormattedText(
                doc.Content,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(style.FontName),
                style.FontSize,
                new SolidColorBrush(style.FontColor));
        }
    }
}
