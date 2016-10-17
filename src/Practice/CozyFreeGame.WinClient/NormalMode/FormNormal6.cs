using CozyFreeGame.GameData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CozyFreeGame.WinClient
{
    public partial class FormNormal6 : Form
    {
        int level = 0;
        int answer = 0;
        int width = 20;
        public bool finish = false;
        Random r = new Random();
        List<Button> btnList = new List<Button>();

        public FormNormal6()
        {
            CenterToScreen();
            InitializeComponent();
            for (var i = 0; i < width; ++i)
            {
                for (var j = 0; j < width; j++)
                {
                    var btn = new Button();
                    btn.Name = "btn" + (i * width + j);
                    btn.Font = new Font("宋体", 12);
                    btn.Size = new Size(25, 25);
                    btn.Location = new Point(5 + j * 30, 5 + i * 30);
                    btn.Click += btnClick;
                    btnList.Add(btn);
                    Controls.Add(btn);
                }
            }
            initLevel();
        }

        void initLevel()
        {
            this.Text = "当前关卡" + (level + 1) + "  共14关";
            if (level < TwoWord.LevelMax)
            {
                answer = r.Next(width * width);
                foreach (var i in btnList)
                {
                    i.Text = TwoWord.Word1[level].ToString();
                }
                btnList[answer].Text = TwoWord.Word2[level].ToString();
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
