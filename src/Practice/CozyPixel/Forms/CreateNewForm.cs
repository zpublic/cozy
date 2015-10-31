using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace CozyPixel.Forms
{
    public partial class CreateNewForm : MetroForm
    {
        private Action<int, int> CreateCallback { get; set; }

        public CreateNewForm(Action<int, int> callback = null)
        {
            InitializeComponent();

            CreateCallback = callback;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            int w = 0;
            int h = 0;
            if(int.TryParse(WidthBox.Text, out w) && int.TryParse(HeightBox.Text, out h) && w > 0 && h > 0)
            {
                CreateCallback(w, h);
            }
            Close();
        }
    }
}
