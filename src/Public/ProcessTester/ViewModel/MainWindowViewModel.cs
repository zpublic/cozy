using ProcessTester.Ext;
using ProcessTester.Model;
using System.Collections.ObjectModel;

namespace ProcessTester.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<ProcessInfo> _ProcessInfoList = new ObservableCollection<ProcessInfo>();

        public ObservableCollection<ProcessInfo> ProcessInfoList
        {
            get
            {
                return _ProcessInfoList;
            }
            set
            {
                Set(ref _ProcessInfoList, value, "ProcessInfoList");
            }
        }

        public MainWindowViewModel()
        {
            TestData();
        }

        private void TestData()
        {
            ProcessUtil.ProcessEnum((pid) =>
            {
                ProcessInfoList.Add(
                new ProcessInfo
                {
                    Name    = "",
                    Pid     = pid,
                }
                );
            });
        }
    }
}