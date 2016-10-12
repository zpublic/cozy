using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace CozyThalassa.DesktopClient
{
    public partial class FormMain : Form
    {
        public ChromiumWebBrowser chromeBrowser;
        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);
            var page = string.Format(@"{0}\KityMinder.html", Application.StartupPath);
            chromeBrowser = new ChromiumWebBrowser(page);
            this.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;
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
    }
}
