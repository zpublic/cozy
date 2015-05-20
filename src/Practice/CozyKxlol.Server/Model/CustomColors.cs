using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Server.Model
{
    public static class CustomColors
    {
        // ABGR
        public static readonly uint[] Colors = new uint[] {
            0xFF0014E5U,   // Red
            0xFF339933U,   // Green
            0xFFE2A11BU,   // Blue
            0xFF0996F0U,   // Orange
            0xFF26BF8CU,   // Grass Green
            0xFFA9AB00U,   // Acid Blue
            0xFF9700FFU,   // Magenta
            0xFFB871E6U,   // Pink
            0xFF006699U,   // Brown
            0xFFFF00A2U};  // Purple

        public static Random ColorRandomMaker = new Random();
        public static uint RandomColor
        {
            get
            {
                return Colors[ColorRandomMaker.Next(Colors.Length)];
            }
        }
    }
}
