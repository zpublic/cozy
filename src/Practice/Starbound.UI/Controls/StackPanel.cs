using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.Controls
{
    public enum Orientation { Horizontal, Veritical };

    public class StackPanel : Panel
    {
        private Orientation orientation;

        public Orientation Orientation
        {
            get { return orientation; }
            set
            {
                if (orientation == value) { return; }
                orientation = value;
                UpdateLayout();
            }
        }

        public override void UpdateLayout()
        {
            if(Orientation == Orientation.Horizontal) { UpdateLayoutHorizontal(); }
            else { UpdateLayoutVertical(); }
        }

        private void UpdateLayoutHorizontal()
        {
            double remainingSize = this.ActualWidth;
            double[] sizes = new double[Children.Count];

            // Allocate sizes up until each child has at least their minimum size.
            // Stop early if you run out of space to allocate.
            for(int index = 0; index < Children.Count && remainingSize > 0; index++)
            {
                sizes[index] = Children[index].MinimumWidth + Children[index].Margin.TotalHorizontal;
                remainingSize -= Children[index].MinimumWidth;

                // If we overallocate, take back some size until we're at the breakeven point.
                if (remainingSize < 0) { sizes[index] -= remainingSize; }
            }

            // Allocate more space to each child until each child has their preferred size.
            // Stop early if you run out of space to allocate.
            for (int index = 0; index < Children.Count && remainingSize > 0; index++)
            {
                sizes[index] = Children[index].PreferredWidth - Children[index].MinimumWidth;
                remainingSize -= Children[index].PreferredWidth - Children[index].MinimumWidth;

                // If we overallocate, take back some size until we're at the breakeven point.
                if (remainingSize < 0) { sizes[index] -= remainingSize; }
            }

            double x = X;
            // Give each child their allocated space.
            for(int index = 0; index < Children.Count; index++)
            {
                Children[index].ActualHeight = Children[index].PreferredHeight;
                Children[index].ActualWidth = sizes[index] - Children[index].Margin.TotalHorizontal;
                Children[index].X = x + Children[index].Margin.Left;
                Children[index].Y = Y + Children[index].Margin.Top;
                x += sizes[index];
            }
        }

        private void UpdateLayoutVertical()
        {
            double remainingSize = this.ActualHeight;
            double[] sizes = new double[Children.Count];

            // Allocate sizes up until each child has at least their minimum size.
            // Stop early if you run out of space to allocate.
            for (int index = 0; index < Children.Count && remainingSize > 0; index++)
            {
                sizes[index] = Children[index].MinimumHeight + Children[index].Margin.TotalVertical;
                remainingSize -= Children[index].MinimumHeight;

                // If we overallocate, take back some size until we're at the breakeven point.
                if (remainingSize < 0) { sizes[index] -= remainingSize; }
            }

            // Allocate more space to each child until each child has their preferred size.
            // Stop early if you run out of space to allocate.
            for (int index = 0; index < Children.Count && remainingSize > 0; index++)
            {
                sizes[index] = Children[index].PreferredHeight - Children[index].MinimumHeight;
                remainingSize -= Children[index].PreferredHeight - Children[index].MinimumHeight;

                // If we overallocate, take back some size until we're at the breakeven point.
                if (remainingSize < 0) { sizes[index] -= remainingSize; }
            }

            double y = Y;
            // Give each child their allocated space.
            for (int index = 0; index < Children.Count; index++)
            {
                Children[index].ActualWidth = Children[index].PreferredWidth;
                Children[index].ActualHeight = sizes[index] - Children[index].Margin.TotalVertical;
                Children[index].X = X + Children[index].Margin.Left;
                Children[index].Y = y + Children[index].Margin.Top;
                y += sizes[index];
            }
        }
    }
}
