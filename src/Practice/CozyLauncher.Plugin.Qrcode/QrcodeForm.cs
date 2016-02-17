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
            Clipboard.SetDataObject(qrCodeImgControl1.Image);
        }
        

        private void saveImage_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "图片|.jpg";
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName.ToString();
                Bitmap image = new Bitmap(qrCodeImgControl1.Image);
                image.Save(fileName);
            }
        }
    }
}
