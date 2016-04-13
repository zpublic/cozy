using CozyThunder.DistributedDownload.MasterGui.Log;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CozyThunder.DistributedDownload.MasterGui.ViewModels
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
