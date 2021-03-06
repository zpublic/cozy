﻿using System;
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
using CozyNetworkHelper;
using CozyAdventure.Protocol;
using CozyAdventure.Protocol.Msg;

using ClientType = CozyClient.Core.CozyClient;

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
            client.Connect("127.0.0.1", 44360);
        }

        private void Init()
        {
            InitMessage();
            InitNetwork();
        }

        private void InitMessage()
        {
            MessageReader.RegisterTypeWithAssembly("CozyAdventure.Protocol");
            MessageCallbackManager.RegisterCallback(this);
        }

        private void InitNetwork()
        {
            client = new ClientType("CozyAdventure");
            client.DataMessage += OnData;
            client.StatusMessage += OnStatus;
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
            var r = MessageReader.GetMessageInstance(e.Message);
            MessageCallbackManager.ShellCallback(r, e.Message);
        }

        private void Register_Click(object sender, EventArgs e)
        {
            if(user.Text.Length > 0 && nickname.Text.Length > 0 && pass.Text.Length > 0)
            {
                var msg = new RegisterMessage()
                {
                    Name = user.Text,
                    Pass = nickname.Text,
                    NickName = pass.Text,
                };

                client.SendMessage(msg);
            }
        }

        private void Login_Click(object sender, EventArgs e)
        {
            if (user.Text.Length > 0 && nickname.Text.Length > 0 && pass.Text.Length > 0)
            {
                var msg = new LoginMessage()
                {
                    Name = user.Text,
                    Pass = nickname.Text,
                };

                client.SendMessage(msg);
            }
        }
    }
}
