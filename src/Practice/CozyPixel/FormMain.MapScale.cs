using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPixel
{
    public partial class CozyPixelForm
    {
        public const int DefaultPixelWidth      = 20;
        public const int DefaultGridWidth       = 2;
        public const int DefaultScale           = 5;

        private readonly int[] ScaleSeed        = { 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30};

        private int CurrScale { get; set; }     = DefaultScale;

        public int CurrPixelWidth { get { return ScaleSeed[CurrScale]; } }

        public void Zoom(int n)
        {
            var newScale = Clamp(CurrScale + n, 0, ScaleSeed.Length - 1);
            if (CurrScale != newScale)
            {
                CurrScale = newScale;
                RefreshPixelMapSize();
            }
        }

        public void ZoomReset()
        {
            if (CurrScale != DefaultScale)
            {
                CurrScale = DefaultScale;
                RefreshPixelMapSize();
            }
        }

        private void RefreshPixelMapSize()
        {
            if(CurrPixelMap != null)
            {
                CurrPixelMap.PixelWidth = ScaleSeed[CurrScale];
                PixelPainter.RefreshPixel();
            }
        }

        private int Clamp(int value, int min, int max)
        {
            if (value > max) return max;
            if (value < min) return min;
            return value;
        }
    }
}
