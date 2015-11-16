using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CozyGod.Game.Model;
using System.IO;

namespace CozyGod.CardEditor
{
    public partial class MainForm : Form
    {
        private Card CurrElement { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private string NamePath { get; set; }

        private void SelectPictureButton_Click(object sender, EventArgs e)
        {
            var openDlg     = new OpenFileDialog();
            openDlg.Filter  = @"图片|*.jpg;*.png;*.bmp";

            if(openDlg.ShowDialog() == DialogResult.OK)
            {
                NamePath            = openDlg.FileName;
                NameTextBox.Text    = Path.GetFileNameWithoutExtension(NamePath);
            }
        }

        private void GenButton_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (NameTextBox.Text != string.Empty
                && CN_NameTextBox.Text != string.Empty
                && int.TryParse(LevelTextBox.Text, out result))
            {
                CurrElement = new Card()
                {
                    Name    = NamePath,
                    CN_Name = CN_NameTextBox.Text,
                    Level   = result,
                };
                cozyGodEditor1.Element = CurrElement;
            }
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            if(CurrElement != null)
            {
                CurrElement.Name = NameTextBox.Text;
            }
        }

        private void CN_NameTextBox_TextChanged(object sender, EventArgs e)
        {
            if(CurrElement != null)
            {
                CurrElement.CN_Name = CN_NameTextBox.Text;
            }
        }

        private void LevelTextBox_TextChanged(object sender, EventArgs e)
        {
            if (LevelTextBox.Text != string.Empty)
            {
                int result = 0;
                if (int.TryParse(LevelTextBox.Text, out result))
                {
                    if(CurrElement != null)
                    {
                        CurrElement.Level = result;
                    }
                }
                else
                {
                    LevelTextBox.Text = "0";
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if(CurrElement != null)
            {
                var saveDlg     = new SaveFileDialog();
                saveDlg.Filter  = @"jpg|*.jpg | png|*.png | bmp|*.bmp";

                if (saveDlg.ShowDialog() == DialogResult.OK)
                {
                    cozyGodEditor1.Image.Save(saveDlg.FileName);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var openDlg = new OpenFileDialog();
            openDlg.Filter = @"图片|*.jpg;*.png;*.bmp";

            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                cozyGodEditor1.ElementBorder = Image.FromFile(openDlg.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var openDlg     = new OpenFileDialog();
            openDlg.Filter  = @"图片|*.jpg;*.png;*.bmp";

            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                cozyGodEditor1.LevelImage = Image.FromFile(openDlg.FileName);
            }
        }
    }
}
