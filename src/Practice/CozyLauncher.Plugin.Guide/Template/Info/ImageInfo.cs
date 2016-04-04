using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace CozyLauncher.Plugin.Guide.Template.Info
{
    public class ImageInfo : ITemplateInfo
    {
        public string Path { get; set; }

        public UIElement GetInfoObject(double width)
        {
            if(string.IsNullOrEmpty(Path))
            {
                throw new ArgumentNullException("Path cannot be null");
            }

            var img = new Image();
            img.BeginInit();
            img.Source = new BitmapImage(new Uri("/CozyLauncher.Plugin.Guide;component/" + Path, UriKind.RelativeOrAbsolute));
            img.Stretch = Stretch.Fill;
            img.EndInit();
            img.MaxWidth = width;
            return img;
        }
    }
}
