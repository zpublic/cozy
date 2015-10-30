using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace CozyPixel.Controls
{
    public class VerticalTabControl : MetroTabControl
    {
        public StringFormat ItemTextFormat { get; set; }

        public VerticalTabControl()
        {
            TextAlign = ContentAlignment.MiddleCenter;

            DrawItem += On_TabControl_DrawItem;

            DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        // 没有用
        private void On_TabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            var g           = e.Graphics;
            var font        = e.Font;
            var brush       = new SolidBrush(Color.Black);
            var tabTextArea = (RectangleF)GetTabRect(e.Index);
            g.DrawString(Controls[e.Index].Text, font, brush, tabTextArea, ItemTextFormat);
        }
    }
}
