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
        public string Style { get; set; }

        public UIElement GetInfoObject(double width)
        {
            if(string.IsNullOrEmpty(Text) || Style == null)
            {
                throw new ArgumentNullException("text and fonts cannot be null");
            }

            var txt         = new TextBox();
            txt.IsEnabled   = false;
            txt.BorderThickness = new Thickness();
            txt.Width       = width;

            var style = TextStyleManager.Instance.GetStyle(Style);

            txt.Text        = Text;
            txt.FontSize    = style.TextSize;
            txt.FontFamily  = new FontFamily(style.Font);
            
            txt.Margin  = new Thickness(style.Margin.Left, style.Margin.Top, style.Margin.Right, style.Margin.Bottom);

            var res     = style.TextAlign.ToAlignment();
            txt.HorizontalContentAlignment = res.Item1;
            txt.VerticalContentAlignment    = res.Item2;


            return txt;
        }
    }
}
