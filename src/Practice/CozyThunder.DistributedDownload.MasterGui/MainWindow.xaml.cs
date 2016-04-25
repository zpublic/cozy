using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using CozyThunder.DistributedDownload.MasterGui.MessageCenter;
using CozyThunder.DistributedDownload.MasterGui.Models;
using CozyThunder.DistributedDownload.MasterGui.Windows;

namespace CozyThunder.DistributedDownload.MasterGui
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += (s, e) => { RegistMessage(); };
            this.Closed += (s, e) => 
            {
                GlobalMessageCenter.Instance.Send("App.Clear");
                UnregistMessage();
            };
        }

        private void RegistMessage()
        {
            GlobalMessageCenter.Instance.RegistMessage("CreateDownloadTask", OnCreateDownload);
            GlobalMessageCenter.Instance.RegistMessage("MainWindow.UIThreadInvoke", OnUIThradInvoke);
        }

        private void UnregistMessage()
        {
            GlobalMessageCenter.Instance.UnregistMessage("MainWindow.UIThreadInvoke", OnUIThradInvoke);
            GlobalMessageCenter.Instance.UnregistMessage("CreateDownloadTask", OnCreateDownload);
        }

        private void OnCreateDownload(object args)
        {
            var task = args as DownloadTaskInfo;
            if(task != null)
            {
                var window = new CreateTaskWindow(task);
                window.ShowDialog();
            }
        }

        private void OnUIThradInvoke(object arg1)
        {
            var act = arg1 as Action;
            if(act != null)
            {
                this.Dispatcher.Invoke(act);
            }
        }
    }
}
