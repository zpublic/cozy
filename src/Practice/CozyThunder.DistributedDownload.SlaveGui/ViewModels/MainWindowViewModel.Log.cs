using System;
using System.Collections.ObjectModel;
using CozyThunder.DistributedDownload.SlaveGui.Log;
using System.Windows;
using System.Windows.Threading;
using System.Threading;

namespace CozyThunder.DistributedDownload.SlaveGui.ViewModels
{
    public partial class MainWindowViewModel
    {
        public ObservableCollection<string> LogCollection { get; set; } = new ObservableCollection<string>();

        void InitLog()
        {
            LogManager.Instalce.DataLogHander += OnDataLog;
            LogManager.Instalce.ClearLogHander += OnClearLog;
        }

        public void OnDataLog(object sender, LogDataEventArgs args)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(Application.Current.Dispatcher));
                SynchronizationContext.Current.Send(pl =>
                {
                    LogCollection.Add(args.Data);
                }, null);
            });
        }

        public void OnClearLog(object sender, LogClearEventArgs args)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(Application.Current.Dispatcher));
                SynchronizationContext.Current.Send(pl =>
                {
                    LogCollection.Clear();
                }, null);
            });
        }
    }
}
