using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CozyPixel.Controls.ControlEventArgs;

namespace CozyPixel.Controls
{
    public class ColorListView : FlowLayoutPanel
    {
        private Dictionary<Color, Button> ColorButtonMap { get; set; } = new Dictionary<Color, Button>();

        public Padding ColorItemMargin { get; set; } = new Padding(3, 0, 3, 0);

        public Color SelectedColor { get; set; } = Color.Empty;
        private Button SelectedButton { get; set; }

        public Size ColorItemSize { get; set; }

        public event EventHandler<ColorEventAgs> ColorSelectedEventHandler;
        public event EventHandler<ColorEventAgs> ColorAddEventHandler;
        public event EventHandler<ColorEventAgs> ColorRemoveEventHandler;

        public void AddColor(Color c)
        {
            if(!ColorButtonMap.ContainsKey(c))
            {
                var bt = new Button();
                SetButtonInfo(bt, c);
                ColorButtonMap[c] = bt;
                this.Controls.Add(bt);

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

                if (ColorRemoveEventHandler != null)
                {
                    ColorRemoveEventHandler(this, new ColorEventAgs(c));
                }
            }
        }

        private void SetButtonInfo(Button bt, Color c)
        {
            bt.Size                         = ColorItemSize;
            bt.Margin                       = ColorItemMargin;
            bt.BackColor                    = c;
            bt.FlatStyle                    = FlatStyle.Flat;
            bt.FlatAppearance.BorderSize    = 0;
            bt.Cursor                       = Cursors.Hand;
            bt.Click                        += OnColorButtonClick;
        }

        private void OnColorButtonClick(object sender, EventArgs e)
        {
            if(SelectedButton != null)
            {
                SelectedButton.FlatAppearance.BorderSize = 0;
            }

            var bt                                      = (Button)sender;
            SelectedColor                               = bt.BackColor;
            SelectedButton                              = bt;
            SelectedButton.FlatAppearance.BorderSize    = 1;

            if (ColorSelectedEventHandler != null)
            {
                ColorSelectedEventHandler(this, new ColorEventAgs(SelectedColor));
            }
        }
    }
}
