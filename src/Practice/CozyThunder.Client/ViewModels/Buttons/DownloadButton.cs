using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CozyThunder.Client
{
    public class DownloadButton : Button
    {
        public DownloadButton()
        {
            this.Text = "下载";
            this.IsEnabled = true;
            this.ButtonVisiblity = Visibility.Visible;
            this.Command = new DownloadCommand();
        }
    }

    public class DownloadCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            MessageBox.Show("下载：" + DownloadViewModel.GetInstance().Address);
            //修改地址栏为只读
            DownloadViewModel.GetInstance().IsReadOnly = true;
            DownloadViewModel.GetInstance().DownloadButton.IsEnabled = false;
            Download download = new Download(DownloadViewModel.GetInstance().Address, Path.Combine(Environment.GetEnvironmentVariable("ProgramFiles"), "Cozyhunder", String.Format("{0}.{1}", DateTime.Now.ToString("yyyyMMddhhmmss"), DownloadViewModel.GetInstance().Address.Split('.').Last())));
            download.Finished += Download_Finished;
            download.Errored += Download_Errored;
            download.Start();
        }

        private void Download_Errored(string error)
        {
            MMS.UI.Default.MessageBox.Error(error);
        }

        private void Download_Finished()
        {
            MessageBox.Show("download finish.");
            DownloadViewModel.GetInstance().IsReadOnly = false;
            DownloadViewModel.GetInstance().DownloadButton.IsEnabled = true;
        }
    }

}
