using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using FileTester.Model;
using System.ComponentModel;
using FileTester.Ext;
using System.Runtime.InteropServices;

namespace FileTester.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<FileModel> _FileList = new ObservableCollection<FileModel>();
        public ObservableCollection<FileModel> FileList 
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

        private string _CurrPath;
        public string CurrPath
        {
            get
            {
                return _CurrPath;
            }
            set
            {
                var result = value.Trim();
                if(result.EndsWith(@"\"))
                {
                    result += '*';
                }
                else
                {
                    if(!result.EndsWith(@"*"))
                    {
                        result += @"\*";
                    }
                }
                Set(ref _CurrPath, result, "CurrPath");
            }
        }

        public MainWindowViewModel()
        {
            PropertyChanged += OnProptrtyChanged;
            CurrPath = @".\";
        }

        public void OnProptrtyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "CurrPath":
                    UpdateFileList();
                    break;
                default:
                    break;
            }
        }

        private void UpdateFileList()
        {
            ObservableCollection<FileModel> newList = new ObservableCollection<FileModel>();
            FileUtil.FileEnum(CurrPath, (x) => 
            {
                var str = Marshal.PtrToStringAuto(x);
                newList.Add(
                    new FileModel 
                    {
                        Name = CurrPath + str,
                    });
            });
            FileList = newList;
        }
    }
}
