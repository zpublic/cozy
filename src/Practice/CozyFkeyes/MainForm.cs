using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CozyFkeyes
{
    public partial class MainForm : Form
    {
        static char[] arr1 = { '巴', '失', '悄', '大', '戍', '白', '兵', '竟', '鸣', '柬', '壳', '含', '士', '己' };
        static char[] arr2 = { '巳', '矢', '俏', '太', '戌', '自', '乒', '竞', '呜', '束', '亮', '合', '土', '已' };
        int level = 0;
        int levelMax = arr1.Count();
        int answer = 0;
        Random r = new Random();
        List<Button> btnList = new List<Button>();

        public MainForm()
        {
            CenterToScreen();
            InitializeComponent();

            for (var i = 0; i < 10; ++i)
            {
                for (var j = 0; j < 10; j++)
                {
                    var btn = new Button();
                    btn.Name = "btn" + (i * 10 + j);
                    btn.Size = new Size(50, 20);
                    btn.Location = new Point(10 + j * 60, 10 + i * 30);
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
            if (level < levelMax)
            {
                answer = r.Next(100);
                foreach (var i in btnList)
                {
                    i.Text = arr1[level].ToString();
                }
                btnList[answer].Text = arr2[level].ToString();
            }
            else
            {
                MessageBox.Show("通关！略屌");
            }
        }

        void btnClick(object sender, System.EventArgs e)
        {
            var btn = sender as Button;
            var answerBtn = "btn" + answer;
            if (answerBtn == btn.Name)
            {
                MessageBox.Show("过关！");
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
