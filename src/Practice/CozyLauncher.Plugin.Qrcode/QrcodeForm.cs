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
    }
}
