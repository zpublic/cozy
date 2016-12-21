using CozyDiscover.Warrior.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CozyDiscover.Warrior
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            MainCore.Instance.Start(OnGameUpdate);
        }
        
        void OnGameUpdate(UpdateType type)
        {
            //label1.Text = PlayerInstance.Instance.Level.ToString();
        }
    }
}
