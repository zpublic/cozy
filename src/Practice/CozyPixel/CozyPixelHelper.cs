using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPixel
{
    public static class CozyPixelHelper
    {

        public static Bitmap ReadBitmapFromFile(string filename)
        {
            Bitmap res = null;
            using (var bmp = new Bitmap(filename))
            {
                res = new Bitmap(bmp);
            }
            return res;
        }
    }
}
