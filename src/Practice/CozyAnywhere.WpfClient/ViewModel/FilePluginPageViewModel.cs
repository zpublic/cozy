using CozyAnywhere.ClientCore;
using CozyAnywhere.WpfClient.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CozyAnywhere.WpfClient.ViewModel
{
    public class FilePluginPageViewModel : BaseViewModel
    {
        public AnywhereClient clientCore { get; set; }

        private ObservableCollection<Tuple<string, bool>> _FileList = new ObservableCollection<Tuple<string, bool>>();

        public ObservableCollection<Tuple<string, bool>> FileList
        {
            get
            {
                return _FileList;
            }
            set
            {
                Set(ref _FileList, value, "FileList");
            }
        }

        private Tuple<string, bool> _FileListSelectedItem;

        public Tuple<string, bool> FileListSelectedItem
        {
            get
            {
                return _FileListSelectedItem;
            }
            set
            {
                Set(ref _FileListSelectedItem, value, "FileListSelectedItem");
            }
        }

        private ICommand _DeleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                return _DeleteCommand = _DeleteCommand ?? new DelegateCommand((x) =>
                {
                    if (FileListSelectedItem != null)
                    {
                        clientCore.SendDeleteMessage(FileListSelectedItem.Item1);
                    }
                });
            }
        }

        public FilePluginPageViewModel()
        {
            clientCore = MainWindowViewModel.clientCore;
            BindCoreCollections();
        }

        private void BindCoreCollections()
        {
            if (clientCore != null)
            {
                clientCore.FileCollection = FileList;
            }
        }
    }
}