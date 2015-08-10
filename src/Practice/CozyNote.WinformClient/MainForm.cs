using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework.Controls;

namespace CozyNote.WinformClient {

    public partial class MainForm : MetroForm {

        class TestModel {
            public string Title { get; set; }
            public string Content { get; set; }
        }


        public MainForm() {
            InitializeComponent();
            Init();
        }

        private void Init() {
            var dataSource = GetDataSource();

            flowLayoutPanel1.Controls.AddRange(dataSource
                .Select(x => {
                    var btn = new MetroButton {
                        Text = x.Title,
                        Width = flowLayoutPanel1.Width
                    };
                    btn.Click += on_btnClick;
                    return btn;
                }).ToArray());

            metroTabControl.TabPages.AddRange(dataSource
                .Select(x => {
                    var tab = new TabPage {
                        Text = x.Title
                    };
                    tab.Controls.Add(new RichTextBox {
                        Dock = DockStyle.Fill,
                        Text = x.Title
                    });
                    return tab;
                }).ToArray());
        }

        private void on_btnClick(object obj, EventArgs e) {
            var btn = (Button)obj;
            metroTabControl.SelectedTab = metroTabControl.TabPages
                .OfType<TabPage>().First(x => x.Text == btn.Text);
            btn.Select();
        }

        private List<TestModel> GetDataSource() {
            return Enumerable.Range(0, 20)
                .Select(x => new TestModel {
                    Title = $"文章标题{x}",
                    Content = x.ToString()
                }).ToList();
        }
    }
}
