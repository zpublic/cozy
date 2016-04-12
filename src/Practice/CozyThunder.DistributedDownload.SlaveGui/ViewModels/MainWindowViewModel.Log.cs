using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyThunder.DistributedDownload.SlaveGui.Log;

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
            LogCollection.Add(args.Data);
        }

        public void OnClearLog(object sender, LogClearEventArgs args)
        {
            LogCollection.Clear();
        }
    }
}
