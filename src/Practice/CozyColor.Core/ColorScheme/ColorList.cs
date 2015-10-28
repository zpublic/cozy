using CozyColor.Core.Color;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyColor.Core.ColorScheme
{
    public class ColorList
    {
        public static System.Drawing.Color[] RandomColorLine()
        {
            var arr = new System.Drawing.Color[9];
            int index = CozyColor.Core.Color.Definition.R.Next(CozyColor.Core.Color.Definition.ColorGroupSize);
            for (int i = 0; i < 9; ++i)
            {
                arr[i] = System.Drawing.ColorTranslator.FromHtml(CozyColor.Core.Color.Definition.ColorGroup[index, i]);
            }
            return arr;
        }
    }
}
