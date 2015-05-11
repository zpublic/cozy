using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI
{
    public struct Thickness
    {
        public readonly double Left;
        public readonly double Right;
        public readonly double Top;
        public readonly double Bottom;

        public Thickness(double amount)
            : this(amount, amount, amount, amount) { }

        public Thickness(double leftAndRight, double topAndBottom)
            : this(leftAndRight, topAndBottom, leftAndRight, topAndBottom) { }

        public Thickness(double left, double top, double right, double bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// Returns the total horizontal thickness, including both Left and Right components.
        /// </summary>
        public double TotalHorizontal { get { return Left + Right; } }

        /// <summary>
        /// Returns the total vertical thickness, including both top and bottom.
        /// </summary>
        public double TotalVertical { get { return Top + Bottom; } }

        public static readonly Thickness Zero = new Thickness(0);
        public static readonly Thickness One = new Thickness(1);
    }
}
