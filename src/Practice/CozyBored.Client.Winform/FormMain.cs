using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CozyBored.Client.Winform.Forms;

namespace CozyBored.Client.Winform
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private DateTime BeginTime { get; set; }

        private DateTime EndTime { get; set; }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            var span        = DateTime.Now - BeginTime;
            TimeLabel.Text  = string.Format("{0:D2}:{1:D2}:{2:D2}", span.Hours, span.Minutes, span.Seconds);
        }

        private void StartButton_MouseDown(object sender, MouseEventArgs e)
        {
            MainTimer.Enabled   = true;
            BeginTime           = DateTime.Now;
        }

        private void StartButton_MouseUp(object sender, MouseEventArgs e)
        {
            EndTime             = DateTime.Now;
            MainTimer.Enabled   = false;
            var span            = EndTime - BeginTime;
            var rank            = Query(span.TotalMilliseconds);

            if (rank == -1)
            {
                MessageBox.Show(this, "你的无聊程度未能进入前1000名。", "遗憾");
            }
            else
            {
                if(rank > 10)
                {
                    MessageBox.Show(this, string.Format("你的无聊程度排名为{0},未能上榜。", rank), "遗憾");
                }
                else
                {
                    var saveForm = new SaveForm(span.TotalMilliseconds);
                    saveForm.ShowDialog();
                }
            }
        }

        private void ViewBoradButton_Click(object sender, EventArgs e)
        {
            var form = new RankListForm();
            form.ShowDialog();
        }

        private int Query(double time)
        {
            return 1;
        }
    }
}
