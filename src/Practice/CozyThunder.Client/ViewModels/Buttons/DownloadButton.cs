using System;
using System.Collections.Generic;
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
        }
    }

}
