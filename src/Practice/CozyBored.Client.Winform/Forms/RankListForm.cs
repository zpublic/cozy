using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CozyBored.Client.Winform.Forms
{
    public partial class RankListForm : Form
    {
        internal const string ChnNums = @"零一二三四五六七八九十";

        public RankListForm()
        {
            InitializeComponent();
        }

        private void RankListForm_Load(object sender, EventArgs e)
        {
            var data = GetRankListInfo();
            if(data != null)
            {
                var ListItems = new List<ListViewItem>();

                for (int i = 0; i < data.Count; ++i)
                {
                    var item = new ListViewItem()
                    {
                        Text = string.Format("第{0}名", ChnNums[i + 1]),
                    };

                    var span = TimeSpan.FromMilliseconds(data[i].Item2);
                    var time = string.Format("{0:D2}:{1:D2}:{2:D2} {3:D3}", span.Hours, span.Minutes, span.Seconds, span.Milliseconds);

                    item.SubItems.Add(data[i].Item1);
                    item.SubItems.Add(time);
                    ListItems.Add(item);
                }

                RankListView.BeginUpdate();
                foreach (var obj in ListItems)
                {
                    RankListView.Items.Add(obj);
                }
                RankListView.EndUpdate();
            }
        }

        private List<Tuple<string, double>> GetRankListInfo()
        {
            return TestData();
        }

        private List<Tuple<string, double>> TestData()
        {
            var result = new List<Tuple<string, double>>();
            result.Add(Tuple.Create("李文超", 3000000.0));
            result.Add(Tuple.Create("朱显章", 300000.0));
            result.Add(Tuple.Create("王文璐", 30000.0));
            return result;
        }

        private void SupportButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "加入QQ群373862115, 赞助Cozy, 即可上榜!", "赞助");
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
