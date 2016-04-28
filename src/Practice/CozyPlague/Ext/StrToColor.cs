using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CozyPlague.Ext
{
    public static class StrToColor
    {
        public static bool ToColor(this string str, ref System.Windows.Media.Brush brush)
        {
            if (!string.IsNullOrEmpty(str) && str.Length == 6)
            {
                byte r = Convert.ToByte(str.Substring(0, 2), 16);
                byte g = Convert.ToByte(str.Substring(2, 2), 16);
                byte b = Convert.ToByte(str.Substring(4, 2), 16);

                brush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r, g, b));
                return true;
            }
            return false;
        }
    }
}
