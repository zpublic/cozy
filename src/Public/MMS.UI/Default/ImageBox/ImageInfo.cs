using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MMS.UI.Default
{
    public class ImageInfo
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public ICommand DownloadImage { get; set; }
        public ICommand SetWallpaper { get; set; }
    }
}
