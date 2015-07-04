using System.Text;
using ProcessTester.Ext;
using ProcessTester.Model;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

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
                string result = null;
                ProcessUtil.GetProcessName(pid, (x) => 
                {
                    result = Marshal.PtrToStringAuto(x);
                });
                if(result != null)
                {
                    ProcessInfoList.Add(
                    new ProcessInfo
                    {
                        Name = result,
                        Pid = pid,
                    }
                    );
                }
                return false;
            });
        }
    }
}