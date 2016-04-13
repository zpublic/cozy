using CozyThunder.DistributedDownload.MasterGui.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CozyThunder.DistributedDownload.MasterGui.ViewModels
{
    public partial class MainWindowViewModel
    {
        private string _Url;
        public string Url
        {
            get { return _Url; }
            set { Set(ref _Url, value); }
        }

        private string _StartButtoContent = "开始下载";
        public string StartButtoContent
        {
            get { return _StartButtoContent; }
            set { Set(ref _StartButtoContent, value); }
        }

        private ICommand _StartCommand;
        public ICommand StartCommand
        {
            get
            {
                return _StartCommand = _StartCommand ?? new DelegateCommand(x =>
                {

                });
            }
        }
    }
}
