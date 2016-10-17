using CozyFreeGame.GameData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CozyFreeGame.WinClient
{
    public partial class FormNormal4 : Form
    {
        int level = 0;
        int answer = 0;
        int width = 9;
        public bool finish = false;
        Random r = new Random();
        List<Button> btnList = new List<Button>();
        TwoWordLib lib = WordPairGenerator.Instance.Gen();

        public FormNormal4()
        {
            CenterToScreen();
            InitializeComponent();
            for (var i = 0; i < width; ++i)
            {
                for (var j = 0; j < width; j++)
                {
                    var btn = new Button();
                    btn.Name = "btn" + (i * width + j);
                    btn.Font = new Font("宋体", 22);
                    btn.Size = new Size(50, 50);
                    btn.Location = new Point(10 + j * 60, 10 + i * 60);
                    btn.Click += btnClick;
                    btnList.Add(btn);
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
                    i.Text = lib.LevelWords[level].Word1.ToString();
                }
                btnList[answer].Text = lib.LevelWords[level].Word2.ToString();
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
    }
}
