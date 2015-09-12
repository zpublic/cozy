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

using ClientType = CozyClient.Core.CozyClient;
using Lidgren.Network;

namespace CozyAdventure.GameNetworkTester
{
    public partial class Form1 : Form
    {
        public ClientType client { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new ClientType("CozyAdventure");
            client.DataMessage      += OnData;
            client.StatusMessage    += OnStatus;

            client.Connect("127.0.0.1", 44360);
        }

        private void OnStatus(object sender, ClienEventArgs e)
        {
            NetConnectionStatus status = (NetConnectionStatus)e.Message.ReadByte();
            if (status == NetConnectionStatus.Connected)
            {
            }
            else if (status == NetConnectionStatus.Disconnected)
            {
            }
        }

        private void OnData(object sender, ClienEventArgs e)
        {
            
        }
    }
}
