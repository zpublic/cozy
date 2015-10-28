using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CozyPixel.Controls.ControlEventArgs;
using CozyPixel.Forms;
using CozyPixel.Controls.Other;
using System.IO;
using CozyPixel.Model;

namespace CozyPixel
{
    public partial class CozyPixelForm : Form
    {
        public bool IsModified { get; set; }

        public string CurrDire { get; set; } = Application.StartupPath;

        public CozyPixelForm()
        {
            InitializeComponent();
            RegisterEvent();
        }

        private void RegisterEvent()
        {
            ColorList.ColorSelectedEventHandler += OnColorSelected;

        }

        private void OnColorSelected(object sender, ColorEventAgs e)
        {
            SelectedColorButton.BackColor = e.SelectedColor;
        }

        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            if (IsModified)
            {
                if (!ShowSaveDialog())
                {
                    return;
                }
            }

            OpenFile();
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            if (IsModified)
            {
                if (!ShowSaveDialog())
                {
                    return;
                }
            }

            Close();
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            var about = new AboutForm();
            about.ShowDialog();
        }

        private void SaveMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            TestColor();
        }

        private void TestColor()
        {
            var list = OstwaldColor.GetColor();
            foreach(var c in list)
            {
                ColorList.AddColor(c);
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PixelPainter.DrawPixel(e.Location, ColorList.SelectedColor);
            if(!IsModified)
            {
                IsModified = true;
            }
        }

        private void GridColorButton_Click(object sender, EventArgs e)
        {
            var selectForm = new ColorSelectForm( c => 
            {
                GridColorButton.BackColor = c;
                if (CurrPixelMap != null)
                {
                    CurrPixelMap.GridColor = c;
                    PixelPainter.RefreshGrid();
                }
            });
            selectForm.ShowDialog();
        }

        private void ShowGridCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrPixelMap != null)
            {
                CurrPixelMap.ShowGrid = ShowGridCheckBox.Checked;
                if (CurrPixelMap.ShowGrid)
                {
                    CurrPixelMap.PixelWidth = DefaultPixelWidth - CurrPixelMap.GridWidth;
                }
                else
                {
                    CurrPixelMap.PixelWidth = DefaultPixelWidth;
                }
                PixelPainter.RefreshPixel();
            }
        }

        private void GridWidthBox_TextChanged(object sender, EventArgs e)
        {
            int w = 0;
            if(int.TryParse(GridWidthBox.Text, out w))
            {
                if (CurrPixelMap != null)
                {
                    if (w == 0) w = 1;

                    CurrPixelMap.GridWidth = w;
                    PixelPainter.RefreshPixel();
                }
            }
        }

        private void CreateMenuItem_Click(object sender, EventArgs e)
        {
            if (IsModified)
            {
                if (!ShowSaveDialog())
                {
                    return;
                }
            }

            var createDlg = new CreateNewForm((w, h) => 
            {
                CloseFile();
                CreateFile(w, h);
            });
            createDlg.ShowDialog();
        }

        private void DirectorySelectButton_Click(object sender, EventArgs e)
        {
            var direDlg = new FolderBrowserDialog();
            var r = direDlg.ShowDialog();
            if(r == DialogResult.OK)
            {
                CurrDire = direDlg.SelectedPath;

                RefreshThumb();
            }
        }

        private void RefreshThumb()
        {
            ThumbListView.Clear();

            DirectoryInfo di = new DirectoryInfo(CurrDire);
            var fs = di.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            foreach (var file in fs)
            {
                if (file.Extension == ".bmp" || file.Extension == ".jpg" || file.Extension == ".png")
                {
                    ThumbListView.TryAddImage(file.FullName);
                }
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsModified)
            {
                if(!ShowSaveDialog())
                {
                    return;
                }
            }

            var bmpPath = ThumbListView.SelectedImagePath;
            if(bmpPath != null &&　File.Exists(bmpPath))
            {
                int gw = RefreshCurrGridWidth();

                var CurrPixelMap = new PixelMap()
                {
                    ShowGrid = ShowGridCheckBox.Checked,
                    data = new Bitmap(bmpPath),
                    PixelWidth = DefaultPixelWidth,
                    GridWidth = gw,
                    GridColor = GridColorButton.BackColor,
                };

                PixelPainter.SourceImage = CurrPixelMap;
                IsModified = false;
            }
        }

        private bool ShowSaveDialog()
        {
            var r = MessageBox.Show("是否保存", "", MessageBoxButtons.YesNoCancel);
            if (r == DialogResult.Yes)
            {
                if (!SaveFile())
                {
                    return false;
                }
            }
            else if (r == DialogResult.Cancel)
            {
                return false;
            }
            return true;
        }
    }
}
