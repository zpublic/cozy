using CozyAnywhere.ClientCore;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CozyAnywhere.WpfClient.Command;

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

        private Tuple<uint, string> _ProcessListSelectedItem;

        public Tuple<uint, string> ProcessListSelectedItem
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

        private ICommand _TerminateCommand;

        public ICommand TerminateCommand
        {
            get
            {
                return _TerminateCommand = _TerminateCommand ?? new DelegateCommand((x) =>
                {
                    if (ProcessListSelectedItem != null)
                    {
                        clientCore.SendTerminateMessage(ProcessListSelectedItem.Item1);
                    }
                });
            }
        }

        private ICommand _RefreshCommand;

        public ICommand RefreshCommand
        {
            get
            {
                return _RefreshCommand = _RefreshCommand ?? new DelegateCommand((x) =>
                {
                    clientCore.SendEnumProcessMessage();
                });
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