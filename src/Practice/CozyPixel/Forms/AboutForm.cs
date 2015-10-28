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
            AboutLabel.Text = "CozyPixel " + Program.Version;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
