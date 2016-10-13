using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CozyThalassa.DesktopClient
{
    public partial class FormMain : Form
    {
        public ChromiumWebBrowser chromeBrowser;
        public void InitializeChromium()
        {
            Cef.EnableHighDPISupport();
            CefSettings settings = new CefSettings();
            settings.Locale = "zh-CN";
            Cef.Initialize(settings);
            var page = string.Format(@"{0}\KityMinder.html", Application.StartupPath);
            chromeBrowser = new ChromiumWebBrowser(page);
            panel1.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;
        }

        void RunJs(string js)
        {
            chromeBrowser.GetBrowser().MainFrame.ExecuteJavaScriptAsync(js);
        }

        public FormMain()
        {
            InitializeComponent();
            InitializeChromium();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RunJs("window.km.execCommand('Template', 'default')");
            NodeData xNode = new NodeData() { text = "大纲" };
            Node root = new Node() { data = xNode };
            root.children.AddRange(enumDirectorie(@"D:\data\大纲"));
            Minder md = new Minder() { root = root };
            string json = JsonConvert.SerializeObject(md);
            RunJs("window.km.importData('json','" + json + "')");
        }

        List<Node> enumDirectorie(string path)
        {
            List<Node> nodes = new List<Node>();
            DirectoryInfo d = new DirectoryInfo(path);
            foreach (var i in d.EnumerateDirectories())
            {
                Node n = new Node();
                n.data = new NodeData() { text = i.Name };
                n.children.AddRange(enumDirectorie(i.FullName));
                nodes.Add(n);
            }
            foreach (var i in d.EnumerateFiles())
            {
                Node n = new Node();
                n.data = new NodeData() { text = i.Name };
                if (i.Extension.ToLower().CompareTo(".png") == 0)
                {
                    //n.data.image = "http://www.newtonsoft.com/json/help/icons/logo.jpg";
                    n.data.image = "file:///"+i.FullName.Replace('\\', '/');
                    n.data.imageSize = new ImageSize();
                }
                nodes.Add(n);
            }
            return nodes;
        }
    }
}
