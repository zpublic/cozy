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
        private Pa pa = new Pa();
        System.Media.SoundPlayer player;

        public MainForm() {
            InitializeComponent();
            pa.KeyPressEvent += Pa_KeyPressEvent;
            pa.Start();
            player = new System.Media.SoundPlayer("g:\\code\\dotnet\\a5\\Tickeys\\Resources\\data\\mechanical\\1.wav");
            player.Load();
        }

        private void Pa_KeyPressEvent(object sender, KeyPressEventArgs e) {
            player.Play();
        }
    }
}
