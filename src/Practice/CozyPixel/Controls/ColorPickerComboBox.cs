using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CozyPixel.Controls.ControlEventArgs;
using MetroFramework.Controls;
using MetroFramework;
using MetroFramework.Drawing;

namespace CozyPixel.Controls
{
    public partial class ColorPickerComboBox : MetroComboBox
    {
        internal class ColorPickerComboxItem
        {
            public string Text { get; set; }

            public Color DrawColor { get; set; }

            public ColorPickerComboxItem(string text, Color color)
            {
                Text        = text;
                DrawColor   = color;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        private Color selectedColor;
        public Color SelectedColor
        {
            get
            {
                return selectedColor;
            }
            set
            {
                selectedColor = value;
                if(ColorPickerSelectedColorChanged != null)
                {
                    ColorPickerSelectedColorChanged(this, new ColorEventAgs(value));
                }
            }
        }

        public int ColorWidth { get; set; } = 100;

        public event EventHandler<ColorEventAgs> ColorPickerSelectedColorChanged;

        public ColorPickerComboBox()
        {
            DrawMode            = DrawMode.OwnerDrawFixed;
            DropDownStyle       = ComboBoxStyle.DropDownList;
            SizeChanged         += (sender, e) =>
            {
                DropDownWidth = Width + ColorWidth;
            };
        }

        public void AddColor(string text, Color c)
        {
            Items.Add(new ColorPickerComboxItem(text, c));
        }

        public void Clear()
        {
            Items.Clear();
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                Color foreColor;
                Color backColor = BackColor;

                if (!UseCustomBackColor)
                {
                    backColor = MetroPaint.BackColor.Form(Theme);
                }

                if (e.State == (DrawItemState.NoAccelerator | DrawItemState.NoFocusRect) || e.State == DrawItemState.None)
                {
                    using (SolidBrush b = new SolidBrush(backColor))
                    {
                        e.Graphics.FillRectangle(b, new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height));
                    }

                    foreColor = MetroPaint.ForeColor.Link.Normal(Theme);
                }
                else
                {
                    using (SolidBrush b = new SolidBrush(MetroPaint.GetStyleColor(Style)))
                    {
                        e.Graphics.FillRectangle(b, new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height));
                    }

                    foreColor = MetroPaint.ForeColor.Tile.Normal(Theme);
                }

                Rectangle textRect = new Rectangle(0, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height);
                TextRenderer.DrawText(e.Graphics, GetItemText(Items[e.Index]), MetroFonts.ComboBox(FontSize, FontWeight), textRect, foreColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

                var item = Items[e.Index] as ColorPickerComboxItem;
                if (item != null)
                {
                    var brush   = new SolidBrush(item.DrawColor);
                    var rect    = e.Bounds;
                    var dr      = new Rectangle(rect.X + Width, rect.Y + rect.Height / 4, ColorWidth, rect.Height / 2);

                    e.Graphics.FillRectangle(brush, dr);
                    SelectedColor = item.DrawColor;
                }
            }
            else
            {
                base.OnDrawItem(e);
            }
        }
    }
}
