using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CozyPixel.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            Location = new Point(
                Screen.PrimaryScreen.Bounds.Width / 2, 
                Screen.PrimaryScreen.Bounds.Height / 2);
        }
    }
}
