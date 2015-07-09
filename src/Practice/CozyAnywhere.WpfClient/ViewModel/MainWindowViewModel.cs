using CozyAnywhere.ClientCore;
using System;
using System.Collections.ObjectModel;

namespace CozyAnywhere.WpfClient.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
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

        public int Port { get; set; }

        public AnywhereClient clientCore { get; set; }

        public MainWindowViewModel()
        {
            clientCore = new AnywhereClient(1000, Port);
            BindCoreCollections();

            TestData();
        }

        private void BindCoreCollections()
        {
            if (clientCore != null)
            {
                clientCore.FileCollection = FileList;
            }
        }

        private void TestData()
        {
            FileList.Add(Tuple.Create("testA", true));
            FileList.Add(Tuple.Create("testB", false));
        }
    }
}