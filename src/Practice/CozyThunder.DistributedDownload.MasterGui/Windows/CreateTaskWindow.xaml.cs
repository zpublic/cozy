using CozyThunder.DistributedDownload.MasterGui.MessageCenter;
using CozyThunder.DistributedDownload.MasterGui.Models;
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
using System.Windows.Shapes;

namespace CozyThunder.DistributedDownload.MasterGui.Windows
{
    /// <summary>
    /// CreateTaskWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CreateTaskWindow : Window
    {
        private DownloadTaskInfo Result { get; set; }

        public CreateTaskWindow(DownloadTaskInfo info = null)
        {
            InitializeComponent();

            Result = info;

            Loaded  += (s, e) => { RegistMessage(); };
            Closed  += (s, e) => { UnregistMessage(); };
        }

        private void RegistMessage()
        {
            GlobalMessageCenter.Instance.RegistMessage("CreateTaskWindw.Submit", OnSubmit);
        }

        private void UnregistMessage()
        {
            GlobalMessageCenter.Instance.UnregistMessage("CreateTaskWindw.Submit", OnSubmit);
        }

        private void OnSubmit(object args)
        {
            var task = args as DownloadTaskInfo;
            if(task != null && Result != null)
            {
                Result.RemotePath           = task.RemotePath;
                Result.LocalPath            = task.LocalPath;
                Result.IsEnableDistributed  = task.IsEnableDistributed;
            }
            this.Close();
        }
    }
}
