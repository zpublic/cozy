using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using CozyPaPaPa.Core;

namespace CozyPaPaPa.WinForm {

    public partial class MainForm : MetroForm {

        public MainForm() {
            InitializeComponent();
            var pa = new Pa();
            pa.KeyPressEvent += Pa_KeyPressEvent;
            pa.KeyDownEvent += Pa_KeyDownEvent;
        }

        private void Pa_KeyDownEvent(object sender, KeyEventArgs e) {
            MessageBox.Show(e.KeyCode.ToString());
        }

        private void Pa_KeyPressEvent(object sender, KeyPressEventArgs e) {
            MessageBox.Show(e.KeyChar.ToString());
        }
    }
}
