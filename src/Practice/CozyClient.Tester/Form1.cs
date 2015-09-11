using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CozyClient.Core;
using Lidgren.Network;

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
            client.DataMessage      += OnData;
            client.StatusMessage    += OnStatus;

            client.Connect("127.0.0.1", 44360);
        }

        private void OnStatus(object sender, ClienEventArgs e)
        {
            NetConnectionStatus status = (NetConnectionStatus)e.Message.ReadByte();
            if(status == NetConnectionStatus.Connected)
            {
                label1.Text = "Connected";
            }
            else if(status == NetConnectionStatus.Disconnected)
            {
                label1.Text = "Disconnected";
            }
        }

        private void OnData(object sender, ClienEventArgs e)
        {
        }
    }
}
