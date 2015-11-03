using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPixel
{
    public partial class CozyPixelForm
    {
        public void ColorSelectCallback(Color c)
        {
            GridColorButton.BackColor = c;
            if (CurrPixelMap != null)
            {
                CurrPixelMap.GridColor = Color.FromArgb(GridAlphaTrack.Value, c);
                PixelPainter.RefreshGrid();
            }
        }

        public void CreateNewCallback(int w, int h)
        {
            CloseFile();
            CreateFile(w, h);
        }
    }
}
