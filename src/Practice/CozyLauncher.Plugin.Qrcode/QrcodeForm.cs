using System.Windows.Forms;
using Gma.QrCodeNet.Encoding;
using System.Drawing;

namespace CozyLauncher.Plugin.Qrcode
{
    public partial class QrcodeForm : Form
    {
        public QrcodeForm(string text)
        {
            InitializeComponent();
            qrCodeImgControl1.Text = text;
        }

        private void qrCodeImgControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                ContextMenuStrip rightOperation = new ContextMenuStrip();
                ToolStripMenuItem copyImage = new ToolStripMenuItem("复制图片");
                copyImage.Click += copyImage_Click;
                ToolStripMenuItem saveImage = new ToolStripMenuItem("保存图片");
                saveImage.Click += saveImage_Click;
                rightOperation.Items.Add(copyImage);
                rightOperation.Items.Add(saveImage);
                rightOperation.Show(qrCodeImgControl1, e.Location);
            }
        }

        private void copyImage_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }
        

        private void saveImage_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
