using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CozyBored.Client.Winform.Forms
{
    public partial class SaveForm : Form
    {
        private double Mark { get; set; }

        public SaveForm(double time)
        {
            Mark = time;
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            var name = NameTextBox.Text.Trim();
            if(name.Length > 0)
            {
                Save(name, Mark);
                Close();
            }
            else
            {
                MessageBox.Show(this, "请输入名字或取消");
            }
        }

        private void CancleButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Save(string name, double mark)
        {

        }
    }
}
