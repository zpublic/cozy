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
    public partial class AboutForm : MetroForm
    {
        public AboutForm()
        {
            InitializeComponent();
            AboutLabel.Text = "CozyPixel " + Program.Version;
            labelAuthor.Text = "Contributors：\r\n    zapline\r\n    kingwl\r\n    wjk";
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
