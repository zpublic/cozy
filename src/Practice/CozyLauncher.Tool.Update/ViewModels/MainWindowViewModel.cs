using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CozyLauncher.Tool.Update.Commands;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows;
using System.Reflection;
using CozyLauncher.Core.Update;
using CozyLauncher.Infrastructure.Http;
using CozyLauncher.Infrastructure.IPC;

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
                    var psi = new ProcessStartInfo()
                    {
                        FileName = "CozyLauncher.exe",
                        WorkingDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "..\\"),
                    };
                    Process.Start(psi);

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

        private const string UpdatePath = @"update\";

        public void DoUpdate(UpdateMgr UpdateManager)
        {
            CloseLauncher();

            var res = UpdateManager.GetUpdateResult();
            var fn  = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), UpdatePath);
            cancleSource    = new CancellationTokenSource();
            UpdateCount     = res.Count;
            UpdateNow       = 0;

            if (!Directory.Exists(fn))
            {
                Directory.CreateDirectory(fn);
            }

            UpdateTask = Task.Factory.StartNew(() =>
            {
                SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(Application.Current.Dispatcher));
                foreach (var file in res)
                {
                    if (cancleSource.IsCancellationRequested)
                    {
                        break;
                    }

                    var url = UpdateManager.GetDownloadUrl(file.Name);

                    HttpDownload.HttpDownloadFile(url, fn + file.Name + ".cozy_update");

                    SynchronizationContext.Current.Send(x =>
                    {
                        FileInfoList.Add(file.Name);
                        UpdateNow++;
                    }, null);
                }

                MoveFile();

                OkEnable        = true;
                CancleEnable    = false;

            }, cancleSource.Token);

            CancleEnable    = true;
            OkEnable        = false;
        }

        private void CloseLauncher()
        {
            using (var npc = new PipeIPCClient("CozyLauncher.CloseAppPipe"))
            {
                npc.Send("SystemCommand.CloseApp");
            }
        }

        private void MoveFile()
        {
            if (Directory.Exists(UpdatePath))
            {
                var files = Directory.GetFiles(UpdatePath);
                var filelist = files.Where(x => x.EndsWith(".cozy_update"));
                foreach (var file in filelist)
                {
                    File.Copy(file, Path.Combine("..\\", Path.GetFileName(file.Replace(".cozy_update", ""))), true);
                    File.Delete(file);
                }
            }
        }
    }
}
