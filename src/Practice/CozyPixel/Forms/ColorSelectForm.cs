using CozyPixel.Controls.Other;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CozyPixel.Tools;

namespace CozyPixel.Forms
{
    public partial class ColorSelectForm : Form
    {
        private Action<Color> ColorCallback { get; set; }

        public ColorSelectForm(Action<Color> callback = null)
        {
            InitializeComponent();

            ColorCallback = callback;

            var list = OstwaldColor.GetColor();
            foreach (var c in list)
            {
                ColorSelectList.AddColor(c);
            }
        }

        private void CancleButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if(ColorSelectList.SelectedColor != Color.Empty && ColorCallback != null)
            {
                ColorCallback(ColorSelectList.SelectedColor);
            }
            Close();
        }
    }
}
