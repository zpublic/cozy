using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CozyLauncher.Core.Update;
using System.Windows.Input;
using CozyLauncher.Tool.Update.Commands;
using CozyLauncher.Infrastructure.Http;
using System.IO;

namespace CozyLauncher.Tool.Update.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private int _UpdateCount;
        public int UpdateCount
        {
            get
            { return _UpdateCount; }
            set
            {
                Set(ref _UpdateCount, value);
            }
        }

        private int _UpdateNow;
        public int UpdateNow
        {
            get
            { return _UpdateNow; }
            set
            {
                Set(ref _UpdateNow, value);
            }
        }

        public ObservableCollection<FileVersionInfo> FileInfoList { get; set; }
            = new ObservableCollection<FileVersionInfo>();

        private UpdateMgr UpdateManager { get; set; } = new UpdateMgr();

        private bool IsCancle { get; set; }

        private ICommand _OkCommand;
        public ICommand OkCommand
        {
            get
            {
                return _OkCommand = _OkCommand ?? new DelegateCommand(x =>
                {
                    Task.Run( () => 
                    {
                        if (UpdateManager.CheckUpdate())
                        {
                            // TODO Close CozyLauncher

                            var res = UpdateManager.GetUpdateResult();

                            UpdateCount = res.Count;
                            UpdateNow = 0;
                            IsCancle = false;

                            var fn = Path.Combine("./", "backup/");
                            if (!Directory.Exists(fn))
                            {
                                Directory.CreateDirectory(fn);
                            }

                            foreach (var file in res)
                            {
                                if (IsCancle)
                                {
                                    return;
                                }

                                System.Threading.Thread.Sleep(300);
                                HttpDownload.HttpDownloadFile(UpdateManager.GetDownloadUrl(file.Name), fn + file.Name + ".cozy_update");
                                UpdateNow++;
                            }
                        }
                    });
                });
            }
        }

        private ICommand _CancleCommand;
        public ICommand CancleCommand
        {
            get
            {
                return _CancleCommand = _CancleCommand ?? new DelegateCommand(x => 
                {
                    IsCancle = true;
                });
            }
        }
    }
}
