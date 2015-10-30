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

namespace CozyPixel.Controls
{
    public partial class ColorPickerComboBox : ComboBox
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
            e.DrawBackground();

            if (e.Index != -1)
            {
                var rect = e.Bounds;
                var item = Items[e.Index] as ColorPickerComboxItem;
                if (item != null)
                {
                    string text = item.Text;
                    var brush = new SolidBrush(item.DrawColor);
                    var dr = new Rectangle(rect.X + Width, rect.Y + rect.Height / 4, ColorWidth, rect.Height / 2);

                    e.Graphics.DrawString(text, e.Font, SystemBrushes.ControlText, rect.X, rect.Y);
                    e.Graphics.FillRectangle(brush, dr);

                    SelectedColor = item.DrawColor;
                }
            }
        }
    }
}
