using CozyAnywhere.ClientCore;
using System;
using System.Collections.ObjectModel;

namespace CozyAnywhere.WpfClient.ViewModel
{
    public class ProcessPluginPageViewModel : BaseViewModel
    {
        public AnywhereClient clientCore { get; set; }

        private ObservableCollection<Tuple<uint, string>> _ProcessList = new ObservableCollection<Tuple<uint, string>>();

        public ObservableCollection<Tuple<uint, string>> ProcessList
        {
            get
            {
                return _ProcessList;
            }
            set
            {
                Set(ref _ProcessList, value, "ProcessList");
            }
        }

        private Tuple<string, bool> _ProcessListSelectedItem;

        public Tuple<string, bool> ProcessListSelectedItem
        {
            get
            {
                return _ProcessListSelectedItem;
            }
            set
            {
                Set(ref _ProcessListSelectedItem, value, "ProcessListSelectedItem");
            }
        }

        public ProcessPluginPageViewModel()
        {
            clientCore = MainWindowViewModel.clientCore;
            BindCoreCollections();
        }

        private void BindCoreCollections()
        {
            if (clientCore != null)
            {
                clientCore.ProcessCollection = ProcessList;
            }
        }
    }
}