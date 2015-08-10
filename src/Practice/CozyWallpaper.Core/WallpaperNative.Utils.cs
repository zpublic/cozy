using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CozyWallpaper.Core
{
    public partial class WallpaperNative
    {
        public const int SPI_SETDESKWALLPAPER   = 20;
        public const int SPIF_SENDCHANGE        = 0x2;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
    }
}
