using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI
{
    public struct SBColor
    {
        public readonly double R;
        public readonly double G;
        public readonly double B;
        public readonly double A;

        public SBColor(byte r, byte g, byte b, byte a = 255)
            : this(r / 255.0, g / 255.0, b / 255.0, a / 255.0)
        {
        }

        public SBColor(double r, double g, double b, double a = 1)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public static readonly SBColor Red = new SBColor(255, 0, 0);
        public static readonly SBColor Orange = new SBColor(255, 165, 0);
        public static readonly SBColor Yellow = new SBColor(255, 255, 0);
        public static readonly SBColor Green = new SBColor(0, 128, 0);
        public static readonly SBColor Blue = new SBColor(0, 0, 255);
        public static readonly SBColor Purple = new SBColor(128, 0, 128);
        public static readonly SBColor White = new SBColor(255, 255, 255);
        public static readonly SBColor Black = new SBColor(0, 0, 0);
    }
}
