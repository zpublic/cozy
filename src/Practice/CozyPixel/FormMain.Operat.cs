using CozyPixel.Model;
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

        public const int DefaultPixelWidth = 18;
        public const int DefaultGridWidth = 2;

        public const string OpenDlgFilter = @"(*.jpg,*.png,*.jpeg,*.bmp,*.gif)| *.jpg; *.png; *.jpeg; *.bmp; *.gif | All files(*.*) | *.* ";
        public const string SaveDlgFilter = @"位图(*.bmp)|*.bmp|All Files|*.*";

        private bool SaveFile()
        {
            if (PixelPainter.Image != null)
            {
                SaveFileDialog SaveDlg  = new SaveFileDialog();
                SaveDlg.Filter          = SaveDlgFilter;

                if (SaveDlg.ShowDialog() == DialogResult.OK)
                {
                    PixelPainter.Save(SaveDlg.FileName);
                    IsModified = false;

                    SetCurrPathStatusLabel(SaveDlg.FileName);
                    IsModified = false;
                    return true;
                }
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
                var bmp = new Bitmap(OpenDlg.FileName);

                if(bmp.Width > 128 || bmp.Height > 128)
                {
                    MessageBox.Show("不支持超过128 * 128的文件", "打开失败");
                    return false;
                }

                ChangePixelPainterImage(bmp);
                SetCurrPathStatusLabel(OpenDlg.FileName);
                IsModified = false;
                return true;
            }
            return false;
        }

        private void CreateFile(int w, int h)
        {
            if(w > 128 || h > 128)
            {
                MessageBox.Show("不支持超过128 * 128的文件", "创建失败");
                return;
            }

            var bmp = new Bitmap(w, h);
            using (var g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(Brushes.White, new Rectangle(0 ,0, w, h));
            }

            ChangePixelPainterImage(bmp);
            SetCurrPathStatusLabel("未命名");
            IsModified = true;
        }

        private void CloseFile()
        {
            CurrPixelMap                = null;
            PixelPainter.SourceImage    = null;
            IsModified                  = false;

            SetCurrPathStatusLabel("无");
        }
    }
}
