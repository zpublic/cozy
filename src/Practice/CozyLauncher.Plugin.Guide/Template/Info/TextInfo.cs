using CozyLauncher.Plugin.Guide.Template.Info.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CozyLauncher.Plugin.Guide.Template.Info
{
    public class TextInfo : ITemplateInfo
    {
        public string Text { get; set; }
        public string Font { get; set; }
        public int TextSize { get; set; }
        public TextAlignType TextAlign { get; set; }
        public MarginInfo Margin { get; set; }

        public FrameworkElement GetInfoObject()
        {
            if(string.IsNullOrEmpty(Text) || string.IsNullOrEmpty(Font))
            {
                throw new ArgumentNullException("text and fonts cannot be null");
            }

            var txt         = new TextBlock();
            txt.Text        = Text;
            txt.FontSize    = TextSize;
            txt.FontFamily  = new FontFamily(Font);
            
            var res = TextAlign.ToAlignment();
            txt.HorizontalAlignment = res.Item1;
            txt.VerticalAlignment   = res.Item2;
            
            txt.Margin      = new Thickness(txt.Margin.Left, txt.Margin.Top, txt.Margin.Right, txt.Margin.Bottom);

            return txt;
        }
    }
}
