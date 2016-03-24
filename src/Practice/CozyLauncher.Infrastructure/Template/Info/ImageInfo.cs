using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace CozyLauncher.Infrastructure.Template.Info
{
    public class ImageInfo : ITemplateInfo
    {
        public string Path { get; set; }

        public FrameworkElement GetInfoObject()
        {
            if(string.IsNullOrEmpty(Path))
            {
                throw new ArgumentNullException("Path cannot be null");
            }

            var img = new Image();
            img.BeginInit();
            img.Source = new BitmapImage(new Uri("/CozyLauncher.Plugin.Guide;component/" + Path, UriKind.RelativeOrAbsolute));
            img.EndInit();
            img.Stretch = Stretch.Fill;
            return img;
        }
    }
}
