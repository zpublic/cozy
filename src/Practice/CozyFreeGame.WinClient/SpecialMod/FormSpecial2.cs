using CozyFreeGame.GameData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CozyFreeGame.WinClient
{
    public partial class FormSpecial2 : Form
    {
        int level = 0;
        int answer = 0;
        int width = 5;
        public bool finish = false;
        Random r = new Random();
        Tuple<Button, Point>[] btnList = new Tuple<Button, Point>[25];
        TwoWordLib lib = TwoWordLibGenerator.Instance.Gen();

        public FormSpecial2()
        {
            CenterToScreen();
            InitializeComponent();
            for (var i = 0; i < width; ++i)
            {
                for (var j = 0; j < width; j++)
                {
                    var btn = new Button();
                    btn.Name = "btn" + (i * width + j);
                    btn.Font = new Font("宋体", 28);
                    btn.Size = new Size(80, 70);
                    btn.Location = new Point(10 + j * 90, 10 + i * 85);
                    btn.Click += btnClick;
                    btnList[i * width + j] = new Tuple<Button, Point>(btn, new Point(10 + j * 90, 10 + i * 85));
                    Controls.Add(btn);
                }
            }
            initLevel();
        }

        void initLevel()
        {
            this.Text = "当前关卡" + (level + 1) + "  共10关";
            if (level < lib.LevelMax)
            {
                answer = r.Next(width * width);
                foreach (var i in btnList)
                {
                    i.Item1.Text = lib.LevelWords[level].Word1.ToString();
                }
                btnList[answer].Item1.Text = lib.LevelWords[level].Word2.ToString();
            }
            else
            {
                finish = true;
                Close();
            }
        }

        void btnClick(object sender, System.EventArgs e)
        {
            var btn = sender as Button;
            var answerBtn = "btn" + answer;
            if (answerBtn == btn.Name)
            {
                level++;
                initLevel();
            }
            else
            {
                MessageBox.Show("眼瞎了吗？点错了");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var arr = btnList.OrderBy(o => Guid.NewGuid()).Take(5).ToArray();
            foreach (var i in arr)
            {
                i.Item1.Location = new Point(i.Item2.X + r.Next(17) - 8, i.Item2.Y + r.Next(17) - 8);
            }
        }
    }
}
