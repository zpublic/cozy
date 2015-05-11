using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.Controls
{
    public class UIElement
    {
        public double MinimumWidth { get; set; }
        public double MaximumWidth { get; set; }
        public double PreferredWidth { get; set; }
        public double MinimumHeight { get; set; }
        public double MaximumHeight { get; set; }
        public double PreferredHeight { get; set; }
        public double ActualWidth { get; set; }
        public double ActualHeight { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public Thickness Margin { get; set; }
    }
}
