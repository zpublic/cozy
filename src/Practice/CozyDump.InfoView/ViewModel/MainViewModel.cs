using CozyDump.Core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CozyDump.InfoView.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        MiniDumpParser _dumpParser;

        string _DumpPath;
        public string DumpPath
        {
            get { return _DumpPath; }
            set { Set("DumpPath", ref _DumpPath, value); }
        }
        string _ModuleText;
        public string ModuleText
        {
            get { return _ModuleText; }
            set { Set("ModuleText", ref _ModuleText, value); }
        }

        public RelayCommand SelectFileCommand { get; private set; }
        public RelayCommand AnalyseCommand { get; private set; }

        void SelectFile()
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = ".dmp";
            ofd.Filter = "dump file|*.dmp";
            if (ofd.ShowDialog() == true)
            {
                DumpPath = ofd.FileName;
            }
        }

        void Analyse()
        {
            _dumpParser?.Dispose();
            _dumpParser = new MiniDumpParser();
            _dumpParser.Parse(DumpPath);
            string output = "";
            for (var i = 0; i < _dumpParser.ModuleNum; ++i)
            {
                var moduleName = _dumpParser.GetStringFromRva(_dumpParser.ModuleInfo(i).ModuleNameRva);
                output += moduleName;
                output += "\r\n";
            }
            ModuleText = output;
        }

        public MainViewModel()
        {
            SelectFileCommand = new RelayCommand(SelectFile);
            AnalyseCommand = new RelayCommand(Analyse); ;
        }
    }
}
