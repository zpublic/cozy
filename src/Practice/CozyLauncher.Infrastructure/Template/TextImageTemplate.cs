using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CozyLauncher.Infrastructure.Template
{
    public class TextImageTemplate : MultiImageTextTemplate
    {
        public TextImageTemplate(bool isTextTop, BitmapImage image, IEnumerable<TextInfo> text)
            : base(isTextTop, new BitmapImage[] { image }, text)
        {

        }
    }

    public class SignalTextImageTemplate : TextImageTemplate
    {
        public SignalTextImageTemplate(bool isTextTop, BitmapImage image, TextInfo text)
            : base(isTextTop, image, new TextInfo[]{ text})
        {

        }
    }

}
