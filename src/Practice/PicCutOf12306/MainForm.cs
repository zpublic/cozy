using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicCutOf12306
{
    public partial class MainForm : Form
    {
        //public static Stream ReadImage(string url)
        //{
        //    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        //    req.ServicePoint.Expect100Continue = false;
        //    req.Method = "GET";
        //    req.KeepAlive = true;
        //    req.ContentType = "image/jpg";
        //    HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
        //    return rsp.GetResponseStream();
        //}

        private string BaseUrl = @"https://kyfw.12306.cn/otn/passcodeNew/getPassCodeNew?module=login&rand=sjrand&";
        private Random rd       = new Random();
        private MD5 md5         = new MD5CryptoServiceProvider();

        public MainForm()
        {
            InitializeComponent();
        }

        private void SavePic( string filename)
        {
            var name    = Path.GetFileNameWithoutExtension(filename);
            var bmp     = (Bitmap)Image.FromFile(filename);

            var title   = new Bitmap(60, 30);
            using (var g = Graphics.FromImage(title))
            {
                g.DrawImage(bmp,
                    new Rectangle(Point.Empty, title.Size),
                    new Rectangle(new Point(117, 0), title.Size),
                    GraphicsUnit.Pixel);
            }
            title.Save(TitlePath + name + "_title.jpg");

            const int w = 74;
            const int h = 80;
            Image[,] imgs = new Image[2, 4];

            int c = 0;
            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    imgs[i, j] = new Bitmap(w, h);
                    using (var g = Graphics.FromImage(imgs[i, j]))
                    {
                        g.DrawImage(bmp,
                            new Rectangle(Point.Empty, imgs[i, j].Size),
                            new Rectangle(new Point(w * j, 30 + h * i), imgs[i, j].Size),
                            GraphicsUnit.Pixel);
                    }
                    imgs[i, j].Save(PartPath + name + "_" + c + ".jpg");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(TitlePath) || string.IsNullOrEmpty(PartPath) || string.IsNullOrEmpty(SourcePath))
            {
                return;
            }

            if (!Directory.Exists(TitlePath))
            {
                Directory.CreateDirectory(TitlePath);
            }

            if (!Directory.Exists(PartPath))
            {
                Directory.CreateDirectory(PartPath);
            }

            var fis = Directory.GetFiles(SourcePath, "*.jpg");

            progressBar1.Minimum = 0;
            progressBar1.Maximum = fis.Length;

            foreach(var fi in fis)
            {
                SavePic(fi);
                progressBar1.Value++;
            }

            MessageBox.Show("OK");
        }

        private string SavePath { get; set; }
        private string TitlePath { get; set; }
        private string PartPath { get; set; }

        private void button3_Click(object sender, EventArgs e)
        {
            var openDlg = new FolderBrowserDialog();
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                SavePath        = openDlg.SelectedPath;
                textBox2.Text   = SavePath;
                TitlePath       = Path.Combine(SavePath, "title/");
                PartPath        = Path.Combine(SavePath, "part/");
            }
        }

        private string SourcePath { get; set; }
        private void button2_Click(object sender, EventArgs e)
        {
            var openDlg = new FolderBrowserDialog();
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                SourcePath      = openDlg.SelectedPath;
                textBox1.Text   = SourcePath;
            }
        }
    }
}
