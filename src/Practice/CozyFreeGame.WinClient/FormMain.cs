using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CozyFreeGame.WinClient
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        void NoImpl()
        {
            MessageBox.Show("不好意思！还没做。。。");
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Hide();
            Stopwatch sw = new Stopwatch();
            FormNormal1 f = new FormNormal1();
            sw.Start();
            f.ShowDialog();
            sw.Stop();
            if (f.finish)
                MessageBox.Show("极易模式完成时间:" + sw.ElapsedMilliseconds + "ms");
            Show();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            Hide();
            Stopwatch sw = new Stopwatch();
            FormNormal2 f = new FormNormal2();
            sw.Start();
            f.ShowDialog();
            sw.Stop();
            if (f.finish)
                MessageBox.Show("简单模式完成时间:" + sw.ElapsedMilliseconds + "ms");
            Show();
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            Hide();
            Stopwatch sw = new Stopwatch();
            FormNormal3 f = new FormNormal3();
            sw.Start();
            f.ShowDialog();
            sw.Stop();
            if (f.finish)
                MessageBox.Show("普通模式完成时间:" + sw.ElapsedMilliseconds + "ms");
            Show();
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            Hide();
            Stopwatch sw = new Stopwatch();
            FormNormal4 f = new FormNormal4();
            sw.Start();
            f.ShowDialog();
            sw.Stop();
            if (f.finish)
                MessageBox.Show("噩梦模式完成时间:" + sw.ElapsedMilliseconds + "ms");
            Show();
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            Hide();
            Stopwatch sw = new Stopwatch();
            FormNormal5 f = new FormNormal5();
            sw.Start();
            f.ShowDialog();
            sw.Stop();
            if (f.finish)
                MessageBox.Show("地狱模式完成时间:" + sw.ElapsedMilliseconds + "ms");
            Show();
        }

        private void button6_Click(object sender, System.EventArgs e)
        {
            Hide();
            Stopwatch sw = new Stopwatch();
            FormNormal6 f = new FormNormal6();
            sw.Start();
            f.ShowDialog();
            sw.Stop();
            if (f.finish)
                MessageBox.Show("炼狱模式完成时间:" + sw.ElapsedMilliseconds + "ms");
            Show();
        }

        private void button7_Click(object sender, System.EventArgs e)
        {
            NoImpl();
        }

        private void button8_Click(object sender, System.EventArgs e)
        {
            NoImpl();
        }

        private void button9_Click(object sender, System.EventArgs e)
        {
            NoImpl();
        }

        private void button10_Click(object sender, System.EventArgs e)
        {
            NoImpl();
        }

        private void button11_Click(object sender, System.EventArgs e)
        {
            NoImpl();
        }

        private void button12_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            NoImpl();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            NoImpl();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Hide();
            Stopwatch sw = new Stopwatch();
            FormSpecial1 f = new FormSpecial1();
            sw.Start();
            f.ShowDialog();
            sw.Stop();
            if (f.finish)
                MessageBox.Show("百里挑一模式完成时间:" + sw.ElapsedMilliseconds + "ms");
            Show();
        }
    }
}
