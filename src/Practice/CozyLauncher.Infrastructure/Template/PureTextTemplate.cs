using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CozyLauncher.Infrastructure.Template
{
    public class PureTextTemplate : TemplateBase
    {
        protected IEnumerable<TextInfo> Text { get; set; }

        public PureTextTemplate(IEnumerable<TextInfo> text)
        {
            if (text == null || text.Count() == 0)
            {
                throw new ArgumentNullException();
            }

            Text = text;
        }

        public override IList<FrameworkElement> GetUseTemplate()
        {
            return GetTextControl();
        }

        private IList<FrameworkElement> GetTextControl()
        {
            var list = new List<FrameworkElement>();

            foreach (var obj in Text)
            {
                var txt = new TextBlock();

                txt.Text = obj.Text;
                txt.FontSize = obj.TextSize;
                txt.FontFamily = new FontFamily(obj.Font);

                var res = obj.TextAlign.ToAlignment();
                txt.HorizontalAlignment = res.Item1;
                txt.VerticalAlignment = res.Item2;

                txt.Margin = new Thickness(txt.Margin.Left, txt.Margin.Top, txt.Margin.Right, txt.Margin.Bottom);

                list.Add(txt);
            }

            return list;
        }
    }

    public class SignalPureTextTemplate : PureTextTemplate
    {
        public SignalPureTextTemplate(TextInfo text)
            : base(new TextInfo[] { text })
        {

        }
    }
}
