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
using System.IO;
using CozyColor.Core.Color;
using CozyPixel.Tools;
using MetroFramework.Forms;
using CozyPixel.Command;
using System.Diagnostics;

namespace CozyPixel
{
    public partial class CozyPixelForm : MetroForm, IPixelColor
    {
        public bool IsModified { get; set; }

        public string CurrDire { get; set; } = Application.StartupPath;

        public string SelectedImagePath { get; set; } = string.Empty;

        public PixelToolBase CurrPixelTool { get; set; }

        public Color CurrColor
        {
            get
            {
                return SelectedColorButton.BackColor;
            }

            set
            {
                SelectedColorButton.BackColor = value;
            }
        }

        private bool IsControlDown { get; set; }

        public CozyPixelForm()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            InitTools();
            TestColor();
            RefreshThumb();
        }

        private void InitTools()
        {
            CurrPixelTool = new PixelPencil(this);

            PixelToolContainer.Instance.RegisterPixelTool("PixelPencil", CurrPixelTool);
            PixelToolContainer.Instance.RegisterPixelTool("PixelLine", new PixelLine(this));
            PixelToolContainer.Instance.RegisterPixelTool("PixelStraw", new PixelStraw(this));
            PixelToolContainer.Instance.RegisterPixelTool("PixelFill", new PixelFill(this));
            PixelToolContainer.Instance.RegisterPixelTool("PixelEraser", new PixelEraser());
        }

        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            if (IsModified && !ShowSaveDialog())
            {
                return;
            }

            CloseFile();
            OpenFile();
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            if (IsModified && !ShowSaveDialog())
            {
                return;
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

        private void GridColorButton_Click(object sender, EventArgs e)
        {
            var selectForm = new ColorSelectForm(ColorSelectCallback);
            selectForm.ShowDialog();
        }

        private void ShowGridCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrPixelMap != null)
            {
                CurrPixelMap.ShowGrid   = ShowGridCheckBox.Checked;
                CurrPixelMap.PixelWidth = DefaultPixelWidth - (CurrPixelMap.ShowGrid ? CurrPixelMap.GridWidth : 0);

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
                    CurrPixelMap.GridWidth = (w == 0 ? 1 : w);
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

            var createDlg = new CreateNewForm(CreateNewCallback);
            createDlg.ShowDialog();
        }

        private void DirectorySelectButton_Click(object sender, EventArgs e)
        {
            var direDlg = new FolderBrowserDialog();
            var r       = direDlg.ShowDialog();
            if(r == DialogResult.OK)
            {
                CurrDire = direDlg.SelectedPath;

                RefreshThumb();
            }
        }

        private void RefreshThumbListButton_Click(object sender, EventArgs e)
        {
            RefreshThumb();
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            var arr     = DradientColor.Generate(RandomColor.Generate(), RandomColor.Generate(), 9);
            CozyColorListView.Clear();

            foreach(var c in arr)
            {
                CozyColorListView.AddColor(c);
            }
        }

        private void ColorList_ColorSelectedEventHandler(object sender, ColorEventAgs e)
        {
            CurrColor = e.SelectedColor;
        }

        private void ThumbListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                if (IsModified && !ShowSaveDialog())
                {
                        return;
                }

                CloseFile();

                var bmpPath = ThumbListView.SelectedImagePath;
                if (bmpPath != null && File.Exists(bmpPath))
                {
                    Bitmap res          = CozyPixelHelper.ReadBitmapFromFile(bmpPath);
                    SelectedImagePath   = bmpPath;

                    ChangePixelPainterImage(res);
                    SetCurrPathStatusLabel(bmpPath);
                }
            }
        }

        private void PixelPainter_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                CurrPixelTool.Begin(PixelPainter, e.Location);
            }
        }

        private void PixelPainter_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left &&　CurrPixelTool.End(e.Location))
            {
                if (!IsModified && CurrPixelTool.WillModify)
                {
                    IsModified = true;
                }
            }
        }

        private void PixelPainter_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CurrPixelTool.Move(e.Location);
            }
        }

        private void PencilToolButton_Click(object sender, EventArgs e)
        {
            if(CurrPixelTool.GetType() != typeof(PixelPencil))
            {
                var tool = PixelToolContainer.Instance.GetPixelTool("PixelPencil");
                if (tool != null)
                {
                    CurrPixelTool = tool;
                }
            }
        }

        private void LineToolButton_Click(object sender, EventArgs e)
        {
            if (CurrPixelTool.GetType() != typeof(PixelLine))
            {
                var tool = PixelToolContainer.Instance.GetPixelTool("PixelLine");
                if (tool != null)
                {
                    CurrPixelTool = tool;
                }
            }
        }

        private void EarserToolButton_Click(object sender, EventArgs e)
        {
            if (CurrPixelTool.GetType() != typeof(PixelEraser))
            {
                var tool = PixelToolContainer.Instance.GetPixelTool("PixelEraser");
                if (tool != null)
                {
                    CurrPixelTool = tool;
                }
            }
        }

        private void StrawToolButton_Click(object sender, EventArgs e)
        {
            if (CurrPixelTool.GetType() != typeof(PixelStraw))
            {
                var tool = PixelToolContainer.Instance.GetPixelTool("PixelStraw");
                if (tool != null)
                {
                    CurrPixelTool = tool;
                }
            }
        }

        private void FillToolButton_Click(object sender, EventArgs e)
        {
            if (CurrPixelTool.GetType() != typeof(PixelFill))
            {
                var tool = PixelToolContainer.Instance.GetPixelTool("PixelFill");
                if (tool != null)
                {
                    CurrPixelTool = tool;
                }
            }
        }

        private void ColorPicker_ColorPickerSelectedColorChanged(object sender, ColorEventAgs e)
        {
            ColorSelectCallback(e.SelectedColor);
        }

        private void CancleMenuItem_Click(object sender, EventArgs e)
        {
            CommandManager.Instance.Undo();
            PixelPainter.UpdateDrawable();
        }

        private void ResumeMenuItem_Click(object sender, EventArgs e)
        {
            CommandManager.Instance.Redo();
            PixelPainter.UpdateDrawable();
        }

        private void CozyPixelForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control)
            {
                var tool = PixelToolContainer.Instance.ShortcutsMapping.GetToolByShortcuts(e.KeyCode);
                if (tool != null)
                {
                    CurrPixelTool = tool;
                }
            }

            int value = (int)e.KeyCode;
            if(value >= (int)Keys.D1 && value <= (int)Keys.D9)
            {
                CozyColorListView.ChangeSelectedColor(value - (int)Keys.D0);
            }
        }

        private void GridAlphaTrack_Scroll(object sender, ScrollEventArgs e)
        {
            ColorSelectCallback(GridColorButton.BackColor);
        }

        const int WHEEL_DELTA = 120;
        private void CozyPixelForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None && ModifierKeys == Keys.Control)
            {
                Zoom(e.Delta / WHEEL_DELTA);
            }
        }

        private void ZoomInMenuItem_Click(object sender, EventArgs e)
        {
            Zoom(1);
        }

        private void ZoomOutMenuItem_Click(object sender, EventArgs e)
        {
            Zoom(-1);
        }

        private void ZoomResetMenuItem_Click(object sender, EventArgs e)
        {
            ZoomReset();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SelectedImagePath != null && SelectedImagePath != "")
            {
                Process.Start(System.Environment.CurrentDirectory + "\\CozyAscii.Console.exe", SelectedImagePath);
            }
        }
    }
}
