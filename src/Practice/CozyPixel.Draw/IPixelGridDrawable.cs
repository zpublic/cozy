using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyPixel.Draw;
using System.Drawing;

namespace CozyPixel.Tools
{
    public interface IPixelGridDrawable : IPixelDrawable
    {
        int GridWidth { get; }
    }
}
