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

        private bool SaveFile()
        {
            if (PixelPainter.Image != null)
            {
                SaveFileDialog SaveDlg = new SaveFileDialog();
                SaveDlg.Filter = @"位图(*.bmp)|*.bmp|All Files|*.*";

                if (SaveDlg.ShowDialog() == DialogResult.OK)
                {
                    PixelPainter.Save(SaveDlg.FileName);
                }
                IsModified = false;
                return true;
            }
            return false;
        }

        private bool OpenFile()
        {
            int gw = RefreshCurrGridWidth();

            OpenFileDialog OpenDlg = new OpenFileDialog();
            OpenDlg.Filter = @"(*.jpg,*.png,*.jpeg,*.bmp,*.gif)| *.jpg; *.png; *.jpeg; *.bmp; *.gif | All files(*.*) | *.* ";

            if (OpenDlg.ShowDialog() == DialogResult.OK)
            {
                CurrPixelMap = new PixelMap()
                {
                    ShowGrid    = ShowGridCheckBox.Checked,
                    data        = new Bitmap(OpenDlg.FileName),
                    PixelWidth  = DefaultPixelWidth,
                    GridWidth   = gw,
                    GridColor   = GridColorButton.BackColor,
                };

                PixelPainter.SourceImage    = CurrPixelMap;
                IsModified                  = false;
                return true;
            }
            return false;
        }

        private void CreateFile(int w, int h)
        {
            int gw = RefreshCurrGridWidth();

            CurrPixelMap = new PixelMap()
            {
                ShowGrid    = ShowGridCheckBox.Checked,
                data        = new Bitmap(w, h),
                PixelWidth  = DefaultPixelWidth,
                GridWidth   = gw,
                GridColor   = GridColorButton.BackColor,
            };
            PixelPainter.SourceImage    = CurrPixelMap;
            IsModified                  = true;
        }

        private int RefreshCurrGridWidth()
        {
            int gw = 0;
            if (!int.TryParse(GridWidthBox.Text, out gw))
            {
                gw                  = DefaultGridWidth;
                GridWidthBox.Text   = DefaultGridWidth.ToString();
            }
            return gw;
        }
    }
}
