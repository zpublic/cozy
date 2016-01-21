using CozyEditor.Highlight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CozyEditor.Text;

namespace CozyEditor.Controls
{
    /// <summary>
    /// CodeTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class CodeTextBox : UserControl
    {
        public CodeTextBox()
        {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawRectangle(Brushes.White, new Pen(Brushes.Black, 2), new Rect(0, 0, this.Width, this.Height));

            if (CurrPlace.LineNum >= Lines.Count || Lines[CurrPlace.LineNum].Documents.Count == 0)
            {
                drawingContext.DrawRectangle(Brushes.Black, new Pen(Brushes.Black, 2), new Rect(0, CurrPlace.LineNum * LineHeigth, 7, LineHeigth));
            }

            for (int i = 0; i < Lines.Count; ++i)
            {
                int l           = 0;
                double offset   = 0;
                foreach (var doc in Lines[i].Documents)
                {
                    FormattedText t = HighlightEngine.Instance.DrawDocument(doc);
                    drawingContext.DrawText(t, new Point(offset, doc.StartPlace.LineNum * LineHeigth));
                    offset += t.WidthIncludingTrailingWhitespace;

                    if (i == CurrPlace.LineNum)
                    {
                        l += doc.Content.Length;
                        if (l >= CurrPlace.CharNum)
                        {
                            var lastOffset      = offset - t.WidthIncludingTrailingWhitespace;
                            var docClone        = doc.Clone();
                            docClone.Content    = doc.Content.Substring(0, doc.Content.Length - (l - CurrPlace.CharNum));
                            var f               = HighlightEngine.Instance.DrawDocument(docClone).WidthIncludingTrailingWhitespace;
                            drawingContext.DrawRectangle(Brushes.Black, new Pen(Brushes.Black, 2), new Rect(lastOffset + f, CurrPlace.LineNum * LineHeigth, 7, LineHeigth));
                        }
                    }
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if(e.Key == Key.Left)
            {
                if(CurrPlace.CharNum > 0)
                {
                    CurrPlace = CurrPlace.Offset(0, -1);
                }
            }
            else if(e.Key == Key.Right)
            {
                if (CurrPlace.LineNum < Lines.Count && CurrPlace.CharNum < Lines[CurrPlace.LineNum].Content.Length)
                {
                    CurrPlace = CurrPlace.Offset(0, 1);
                }
            }
            else if (e.Key == Key.Up)
            {
                if(CurrPlace.LineNum > 0)
                {
                    CurrPlace = CurrPlace.Offset(-1, 0);
                }
            }
            else if (e.Key == Key.Down)
            {
                if (CurrPlace.LineNum < Lines.Count - 1)
                {
                    CurrPlace = CurrPlace.NextLine();
                }
            }
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);

            var t = TextMapEngine.Instance.Map(e.Text);

            if (CurrPlace.LineNum >= Lines.Count)
            {
                Lines.Lines.Add(new Document.Line { Source = Text, BeginPoint = Text.Length, });
            }

            if (e.Text == "\b")
            {
                if(Text.Length > 0 && Lines[CurrPlace.LineNum].BeginPoint + CurrPlace.CharNum > 0)
                {
                    int offset      = 1;
                    var newPlace    = Place.Zero;
                    if(CurrPlace.CharNum == 0)
                    {
                        offset      = LineBreakHolder.CurrLineBreak.Length;
                        newPlace    = CurrPlace.LastLine(Lines[CurrPlace.LineNum - 1].Content.Length);
                    }
                    else
                    {
                        newPlace = CurrPlace.Offset(0, -offset);
                    }
                    Text        = Text.Remove(Lines[CurrPlace.LineNum].BeginPoint + CurrPlace.CharNum - offset, offset);
                    CurrPlace   = newPlace;
                }
            }
            else
            {
                Text = Text.Insert(Lines[CurrPlace.LineNum].BeginPoint + CurrPlace.CharNum, t);

                if (t == LineBreakHolder.CurrLineBreak)
                {
                    CurrPlace = CurrPlace.NextLine();
                }
                else
                {
                    CurrPlace = CurrPlace.Offset(0, t.Length);
                }
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            this.Focus();
        }
    }
}
