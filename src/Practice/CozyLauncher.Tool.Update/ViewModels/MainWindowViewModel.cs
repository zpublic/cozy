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
using System.IO.Pipes;
using System.Threading;
using System.Windows.Threading;

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

        public ObservableCollection<string> FileInfoList { get; set; }
            = new ObservableCollection<string>();

        private ICommand _OkCommand;
        public ICommand OkCommand
        {
            get
            {
                return _OkCommand = _OkCommand ?? new DelegateCommand(x =>
                {
                    OnPropertyChanged("UpdateCommand.Exit");
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
                    cancleSource?.Cancel();
                });
            }
        }

        private bool _CancleEnable = true;
        public bool CancleEnable
        {
            get
            { return _CancleEnable; }
            set
            {
                Set(ref _CancleEnable, value);
            }
        }

        private bool _OkEnable = false;
        public bool OkEnable
        {
            get
            { return _OkEnable; }
            set
            {
                Set(ref _OkEnable, value);
            }
        }

        private CancellationTokenSource cancleSource { get; set; }
        private Task UpdateTask { get; set; }

        public void DoUpdate(UpdateMgr UpdateManager)
        {
            CloseLauncher();

            var res = UpdateManager.GetUpdateResult();
            var fn  = Path.Combine("./", "update/");
            cancleSource    = new CancellationTokenSource();
            UpdateCount     = res.Count;
            UpdateNow       = 0;

            if (!Directory.Exists(fn))
            {
                Directory.CreateDirectory(fn);
            }

            UpdateTask = Task.Factory.StartNew(() =>
            {
                SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(System.Windows.Application.Current.Dispatcher));
                foreach (var file in res)
                {
                    if (cancleSource.IsCancellationRequested)
                    {
                        break;
                    }

                    HttpDownload.HttpDownloadFile(UpdateManager.GetDownloadUrl(file.Name), fn + file.Name + ".cozy_update");

                    SynchronizationContext.Current.Send(x =>
                    {
                        FileInfoList.Add(file.Name);
                        UpdateNow++;
                    }, null);
                }

                OkEnable        = true;
                CancleEnable    = false;

            }, cancleSource.Token);

            CancleEnable    = true;
            OkEnable        = false;
        }

        private void CloseLauncher()
        {
            using (var npc = new NamedPipeClientStream("CozyLauncher.CloseAppPipe"))
            {
                try
                {
                    npc.Connect(0);
                }
                catch(TimeoutException)
                {

                }

                if (npc.IsConnected)
                {
                    using (var sw = new StreamWriter(npc))
                    {
                        sw.AutoFlush = true;
                        sw.Write("SystemCommand.CloseApp");
                    }
                }
            }
        }
    }
}
