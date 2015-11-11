using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CozyGod.Model;

namespace CozyGod.CardEditor
{
    public partial class MainForm : Form
    {
        private CozyGodElement CurrElement { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void SelectPictureButton_Click(object sender, EventArgs e)
        {
            var openDlg     = new OpenFileDialog();
            openDlg.Filter  = @"图片|*.jpg;*.png;*.bmp";

            if(openDlg.ShowDialog() == DialogResult.OK)
            {
                PictureTextBox.Text = openDlg.FileName;
            }
        }

        private void GenButton_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (NameTextBox.Text != string.Empty
                && CN_NameTextBox.Text != string.Empty
                && PictureTextBox.Text != string.Empty
                && int.TryParse(LevelTextBox.Text, out result))
            {
                CurrElement = new CozyGodElement()
                {
                    Name    = NameTextBox.Text,
                    CN_Name = CN_NameTextBox.Text,
                    Picture = PictureTextBox.Text,
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

        private void PictureTextBox_TextChanged(object sender, EventArgs e)
        {
            if(CurrElement != null)
            {
                CurrElement.Picture = PictureTextBox.Text;
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
    }
}
