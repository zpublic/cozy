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

namespace CozyImage.BatchResize
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void OpenFoldersButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog open_dialog = new FolderBrowserDialog();
            open_dialog.Description = "请选择源路径";
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                FolderBrowserDialog save_dialog = new FolderBrowserDialog();
                save_dialog.Description = "请选择目标路径";
                if (save_dialog.ShowDialog() == DialogResult.OK)
                {
                    var fileInfo = Directory.GetFiles(open_dialog.SelectedPath);
                    foreach(var file in fileInfo)
                    {
                        var ext = Path.GetExtension(file);
                        if (ext == ".jpg" || ext == ".bmp" || ext == ".png")
                        {
                            var img = Image.FromFile(file);
                            var result = CozyImage.Core.ImageResize.Resize(img, int.Parse(WidthBox.Text), int.Parse(HeightBox.Text));
                            result.Save(save_dialog.SelectedPath + @"/" + Path.GetFileName(file));
                        }
                    }
                }
            }
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            var open_file_dlg = new OpenFileDialog();
            open_file_dlg.Filter = "(*.jpg,*.png,*.bmp)| *.jgp; *.png; *.bmp;";
            if(open_file_dlg.ShowDialog() == DialogResult.OK)
            {
                var img = Image.FromFile(open_file_dlg.FileName);
                var result = CozyImage.Core.ImageResize.Resize(img, int.Parse(WidthBox.Text), int.Parse(HeightBox.Text));

                var save_file_dlg = new SaveFileDialog();
                save_file_dlg.Filter = "jpg(*.jpg)|*.jpg|bmp(*.bmp)|*.bmp;|png(*.png)|*.png";
                if (save_file_dlg.ShowDialog() == DialogResult.OK)
                {
                    result.Save(save_file_dlg.FileName);
                }
            }
        }
    }
}
