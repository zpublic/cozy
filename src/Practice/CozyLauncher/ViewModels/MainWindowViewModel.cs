using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CozyLauncher.PluginBase;
using CozyLauncher.Commands;
using System.Windows.Input;
using CozyLauncher.Core.Plugin;
using CozyLauncher.Ext;

namespace CozyLauncher.ViewModel
{
    public class MainWindowViewModel : BaseViewModel, IPublicApi
    {
        private PluginMgr pm = new PluginMgr();

        public ExtObservableCollection<Result> ResultListView { get; set; } = new ExtObservableCollection<Result>();

        private string _InputText;
        public string InputText
        {
            get { return _InputText; }
            set { this.Set(ref _InputText, value); }
        }

        private bool _IsResultViewVisiable;
        public bool IsResultViewVisiable
        {
            get { return _IsResultViewVisiable; }
            set { this.Set(ref _IsResultViewVisiable, value); }
        }

        private Result _SelectedResult;
        public Result SelectedResult
        {
            get { return _SelectedResult; }
            set { this.Set(ref _SelectedResult, value); }
        }

        private ICommand _QueryCommand;
        public ICommand QueryCommand
        {
            get
            {
                return _QueryCommand = _QueryCommand ?? new DelegateCommand(x =>
                {
                    Query q = new Query();
                    q.RawQuery = InputText;
                    pm.Query(q);
                });
            }
        }

        private ICommand _DoCommand;
        public ICommand DoCommand
        {
            get
            {
                return _DoCommand = _DoCommand ?? new DelegateCommand(x => 
                {
                    if(SelectedResult != null)
                    {
                        SelectedResult.Action(null);
                    }
                });
            }
        }


        public MainWindowViewModel()
        {
            pm.Init(this);
        }

        public void CloseApp()
        {
            this.OnPropertyChanged("CloseApp");
        }

        public void HideApp()
        {
            this.OnPropertyChanged("HideApp");
        }

        public void PushResults(List<Result> results)
        {
            ResultListView.Clear();

            if (results.Count > 0)
            {
                if(!IsResultViewVisiable)
                {
                    IsResultViewVisiable = true;
                }

                ResultListView.AddRange(results);
            }
            else
            {
                IsResultViewVisiable = false;
            }
        }
    }
}
