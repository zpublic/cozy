using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPixel.Controls.ControlEventArgs
{
    public class ColorEventAgs : EventArgs
    {
        public Color SelectedColor { get; set; }

        public ColorEventAgs(Color c)
        {
            SelectedColor = c;
        }
    }
}
