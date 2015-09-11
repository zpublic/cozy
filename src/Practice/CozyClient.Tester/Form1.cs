using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ClientType = CozyClient.Core.CozyClient;

namespace CozyClient.Tester
{
    public partial class Form1 : Form
    {
        ClientType client { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new ClientType("CozyServerTester");
            client.Connect("127.0.0.1", 44360);
        }
    }
}
