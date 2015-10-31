using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CozyPixel.Controls.ControlEventArgs;
using MetroFramework.Controls;

namespace CozyPixel.Controls
{
    public class ColorListView : FlowLayoutPanel
    {
        private Dictionary<Color, MetroButton> ColorButtonMap { get; set; } = new Dictionary<Color, MetroButton>();

        public Padding ColorItemMargin { get; set; } = new Padding(3, 0, 3, 0);

        private Color selectedColor = Color.Empty;
        public Color SelectedColor
        {
            get
            {
                return selectedColor;
            }
            set
            {
                selectedColor = value;
                if(value == Color.Empty)
                {
                    SelectedButton = null;
                }
                else
                {
                    if (!ColorButtonMap.ContainsKey(value))
                    {
                        throw new KeyNotFoundException("Cannot find color");
                    }

                    var bt                                      = ColorButtonMap[value];
                    SelectedButton                              = bt;
                    SelectedButton.FlatAppearance.BorderSize    = 1;
                }

                if (ColorSelectedEventHandler != null)
                {
                    ColorSelectedEventHandler(this, new ColorEventAgs(value));
                }
            }
        }

        private Button SelectedButton { get; set; }

        public Size ColorItemSize { get; set; }

        public event EventHandler<ColorEventAgs> ColorSelectedEventHandler;
        public event EventHandler<ColorEventAgs> ColorAddEventHandler;
        public event EventHandler<ColorEventAgs> ColorRemoveEventHandler;

        public void AddColor(Color c)
        {
            if(!ColorButtonMap.ContainsKey(c))
            {
                var bt = new MetroButton();
                SetButtonInfo(bt, c);
                ColorButtonMap[c] = bt;
                this.Controls.Add(bt);

                if(ColorButtonMap.Count == 1)
                {
                    SelectedColor = c;
                }

                if(ColorAddEventHandler  != null)
                {
                    ColorAddEventHandler(this, new ColorEventAgs(c));
                }
            }
        }

        public void RemoveColor(Color c)
        {
            if (ColorButtonMap.ContainsKey(c))
            {
                var bt = ColorButtonMap[c];
                Controls.Remove(bt);
                ColorButtonMap.Remove(c);

                if (ColorButtonMap.Count == 0)
                {
                    SelectedColor = Color.Empty;
                }

                if (ColorRemoveEventHandler != null)
                {
                    ColorRemoveEventHandler(this, new ColorEventAgs(c));
                }
            }
        }

        private void SetButtonInfo(MetroButton bt, Color c)
        {
            bt.UseCustomForeColor = true;
            bt.UseCustomBackColor           = true;
            bt.Size                         = ColorItemSize;
            bt.Margin                       = ColorItemMargin;
            bt.ForeColor                    = c;
            bt.BackColor                    = c;
            bt.Click                        += OnColorButtonClick;
        }

        private void OnColorButtonClick(object sender, EventArgs e)
        {
            if(SelectedButton != null)
            {
                SelectedButton.FlatAppearance.BorderSize = 0;
            }

            var bt                                      = (MetroButton)sender;
            SelectedColor                               = bt.BackColor;
        }
    }
}
