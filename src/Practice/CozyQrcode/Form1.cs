using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CozyQrcode
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Cinbox_TextChanged(object sender, EventArgs e)
        {
            qrCodeImgControl1.Text = this.Cinbox.Text;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var dlg     = new SaveFileDialog();
            dlg.Filter  = "位图文件| *.bmp";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                qrCodeImgControl1.Image.Save(dlg.FileName);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            qrCodeImgControl1.Text = Cinbox.Text = "Cozy";
        }
    }
}