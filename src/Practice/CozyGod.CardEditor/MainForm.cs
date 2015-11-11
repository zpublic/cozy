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

        private void MainForm_Load(object sender, EventArgs e)
        {
            NameTextBox.Text = "kingwl";
            CurrElement = new CozyGodElement()
            {
                CN_Name = "王文璐",
                Name    = "kingwl",
                Level   = 233,
                Picture = @"C:\Users\AngelC\Desktop\elements\Pictures\Bird.bmp",
            };
            cozyGodEditor1.Element = CurrElement;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (CurrElement != null)
            {
                CurrElement.Name = NameTextBox.Text;
            }
        }
    }
}
