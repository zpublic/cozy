using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CozyPixel.Controls
{
    public class ImageListView : ListView
    {
        private ImageList ImgList { get; set; } = new ImageList();

        private List<string> PathList { get; set; } = new List<string>();

        public int MaxNameLength { get; set; } = 12;

        public string SelectedImagePath
        {
            get
            {
                var index = SelectedIndices[0];
                if (index >= 0 && index < PathList.Count)
                {
                    return PathList[index];
                }
                return null;
            }
        }

        public ImageListView()
        {
            LargeImageList      = ImgList;
            int w               = Width - SystemInformation.VerticalScrollBarWidth * 2;
            ImgList.ImageSize   = new Size(w, w);
        }

        public void ImageClear()
        {
            Items.Clear();
            PathList.Clear();
            ImgList.Images.Clear();
        }

        public bool TryAddImage(string path)
        {
            var img = Image.FromFile(path);

            if(img.Width > 128 ||img.Height > 128)
            {
                return false;
            }

            int i = PathList.Count;

            PathList.Add(path);
            ImgList.Images.Add(img);

            var filename        = Path.GetFileNameWithoutExtension(path);
            int maxNameLength   = MaxNameLength < filename.Length ? MaxNameLength : filename.Length;

            Items.Add(filename.Substring(0, maxNameLength) + "...", i);
            Items[i].ImageIndex = i;

            return true;
        }
    }
}
