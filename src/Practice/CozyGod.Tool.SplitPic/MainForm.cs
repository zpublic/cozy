using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace CozyGod.Tool.SplitPic
{
    public partial class MainForm : Form
    {
        private XmlSerializer serializer { get; set; } 
            = new XmlSerializer(typeof(GodXmlEmelent));

        private Dictionary<string, Bitmap> BitmapCache { get; set; } 
            = new Dictionary<string, Bitmap>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            var openDlg     = new OpenFileDialog();
            openDlg.Filter  = @"element文件 | *.xml";
            if(openDlg.ShowDialog() == DialogResult.OK)
            {
                ReadFile(openDlg.FileName);
            }
        }

        private void ReadFile(string filename)
        {
                using (var reader = new StreamReader(filename, Encoding.UTF8))
                {
                    var obj = (GodXmlEmelent)serializer.Deserialize(reader);
                    SplitPicture(filename, obj);
                }
        }

        private void SplitPicture(string filename, GodXmlEmelent element)
        {
            var dire    = Path.GetDirectoryName(filename);
            var outdire = Path.Combine(dire, @"Pictures\");
            int w       = element.Size.Width;
            int h       = element.Size.Height;
            int c       = 0;

            Directory.CreateDirectory(outdire);
            progressBar1.Value      = 0;
            progressBar1.Maximum    = element.Layers.Count;

            foreach (var obj in element.Layers)
            {
                var picPath = Path.Combine(dire, obj.Path);
                Bitmap bmp  = null;

                if (BitmapCache.ContainsKey(picPath))
                {
                    bmp = BitmapCache[picPath];
                }
                else
                {
                    if (File.Exists(picPath))
                    {
                        bmp                     = new Bitmap(picPath);
                        BitmapCache[picPath]    = bmp;
                    }
                }

                if(bmp != null)
                {
                    var img = new Bitmap(w, h);
                    using (var g = Graphics.FromImage(img))
                    {
                        g.DrawImage(
                            bmp, 
                            new Rectangle(0, 0, w, h), 
                            new Rectangle(w * obj.X, h * obj.Y, w, h), 
                            GraphicsUnit.Pixel);
                    }
                    img.Save(Path.Combine(outdire, obj.Name + ".bmp"));

                    progressBar1.Value = ++c;
                }
            }

            MessageBox.Show("完成 共导出" + c + "个元素");
        }
    }
}
