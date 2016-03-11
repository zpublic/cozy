using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CozyLauncher.Infrastructure.Template
{
    public class MultiImageTextTemplate : PureTextTemplate
    {
        protected IEnumerable<BitmapImage> Images { get; set; }

        protected bool IsTextTop { get; set; }

        public MultiImageTextTemplate(bool isTextTop, IEnumerable<BitmapImage> image, IEnumerable<TextInfo> text)
            : base(text)
        {
            if (image == null)
            {
                throw new ArgumentNullException();
            }

            Images      = image;
            IsTextTop   = isTextTop;
        }

        public override IList<FrameworkElement> GetUseTemplate()
        {
            var result = new List<FrameworkElement>();

            if (IsTextTop)
            {
                result.AddRange(base.GetUseTemplate());
                result.AddRange(GetImageControl());
            }
            else
            {
                result.AddRange(GetImageControl());
                result.AddRange(base.GetUseTemplate());
            }

            return result;
        }

        protected List<FrameworkElement> GetImageControl()
        {
            var result = new List<FrameworkElement>();

            foreach(var obj in Images)
            {
                var img = new Image();
                img.BeginInit();
                img.Source = obj;
                img.EndInit();
                img.Stretch = System.Windows.Media.Stretch.Fill;
                result.Add(img);
            }
            return result;
        }
    }
}
