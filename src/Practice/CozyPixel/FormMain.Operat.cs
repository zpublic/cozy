using CozyPixel.Model;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CozyPixel
{
    public partial class CozyPixelForm
    {
        private PixelMap CurrPixelMap { get; set; }

        public const string OpenDlgFilter = @"(*.jpg,*.png,*.jpeg,*.bmp,*.gif)| *.jpg; *.png; *.jpeg; *.bmp; *.gif | All files(*.*) | *.* ";
        public const string SaveDlgFilter = @"位图(*.bmp)|*.bmp|All Files|*.*";

        private bool SaveFile()
        {
            if (PixelPainter.Image != null)
            {
                if(SelectedImagePath == string.Empty)
                {
                    SaveFileDialog SaveDlg  = new SaveFileDialog();
                    SaveDlg.Filter          = SaveDlgFilter;

                    if (SaveDlg.ShowDialog() == DialogResult.OK)
                    {
                        SelectedImagePath   = SaveDlg.FileName;
                    }
                    else
                    {
                        return false;
                    }
                }

                SetCurrPathStatusLabel(SelectedImagePath);
                PixelPainter.Save(SelectedImagePath);
                IsModified = false;

                return true;
            }
            return false;
        }

        private bool OpenFile()
        {
            int gw                  = RefreshCurrGridWidth();
            OpenFileDialog OpenDlg  = new OpenFileDialog();
            OpenDlg.Filter          = OpenDlgFilter;

            if (OpenDlg.ShowDialog() == DialogResult.OK)
            {
                Bitmap res = CozyPixelHelper.ReadBitmapFromFile(OpenDlg.FileName);

                if (res.Width > 128 || res.Height > 128)
                {
                    MetroMessageBox.Show(this, "不支持超过128 * 128的文件", "打开失败");
                    return false;
                }

                ChangePixelPainterImage(res);
                SetCurrPathStatusLabel(OpenDlg.FileName);
                SelectedImagePath = OpenDlg.FileName;
                return true;
            }
            return false;
        }

        private void CreateFile(int w, int h)
        {
            if(w > 128 || h > 128)
            {
                MetroMessageBox.Show(this, "不支持超过128 * 128的文件", "创建失败");
                return;
            }

            var bmp = new Bitmap(w, h);
            using (var g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(Brushes.White, new Rectangle(0, 0, w, h));
            }

            ChangePixelPainterImage(bmp);
            SetCurrPathStatusLabel("未命名");
            IsModified          = true;
            SelectedImagePath   = string.Empty;
        }

        private void CloseFile()
        {
            CurrPixelMap                = null;
            PixelPainter.SourceImage    = null;
            IsModified                  = false;
            SelectedImagePath           = string.Empty;

            SetCurrPathStatusLabel("无");
        }
    }
}
