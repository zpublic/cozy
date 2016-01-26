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
using CozyLauncher.Infrastructure.Hotkey;
using System.IO;

namespace CozyLauncher.ViewModels
{
    public class MainWindowViewModel : BaseViewModel, IPublicApi
    {
        private PluginMgr pm = new PluginMgr();

        public ExtObservableCollection<Result> ResultListView { get; set; } = new ExtObservableCollection<Result>();

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

        private int _SelectedResultIndex;
        public int SelectedResultIndex
        {
            get { return _SelectedResultIndex; }
            set { this.Set(ref _SelectedResultIndex, value); }
        }

        private ICommand _QueryCommand;
        public ICommand QueryCommand
        {
            get
            {
                return _QueryCommand = _QueryCommand ?? new DelegateCommand(x =>
                {
                    var text = x as string;
                    if (text != null && text != "")
                    {
                        Query q = new Query();
                        q.RawQuery = text;
                        pm.Query(q);
                    }
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
                    SelectedResult?.Action(null);
                });
            }
        }

        private ICommand _UpCommand;
        public ICommand UpCommand
        {
            get
            {
                return _UpCommand = _UpCommand ?? new DelegateCommand(x =>
                {
                    if (SelectedResultIndex == -1)
                    {
                        SelectedResultIndex = 0;
                    }
                    else
                    {
                        if (SelectedResultIndex > 0 && ResultListView.Count > 1)
                        {
                            SelectedResultIndex--;
                        }
                    }
                });
            }
        }

        private ICommand _DwonCommand;
        public ICommand DownCommand
        {
            get
            {
                return _DwonCommand = _DwonCommand ?? new DelegateCommand(x =>
                {
                    if (SelectedResultIndex == -1)
                    {
                        SelectedResultIndex = 0;
                    }
                    else
                    {
                        if (SelectedResultIndex < ResultListView.Count - 1 && ResultListView.Count > 1)
                        {
                            SelectedResultIndex++;
                        }
                    }
                });
            }
        }

        private ICommand _ShowCommand;
        public ICommand ShowCommand
        {
            get
            {
                return _ShowCommand = _ShowCommand ?? new DelegateCommand(x => 
                {
                    ShowApp();
                });
            }
        }

        public MainWindowViewModel()
        {
            pm.Init(this);

            GlobalHotkey.Instance.RegistHotkeyAction("HotKey.ShowApp", ()=> 
            {
                if(ShowCommand.CanExecute(null))
                {
                    ShowCommand.Execute(null);
                }
            });
        }

        public void CloseApp()
        {
            this.OnPropertyChanged("SystemCommand.CloseApp");
        }

        public void HideApp()
        {
            this.OnPropertyChanged("SystemCommand.HideApp");
        }

        public void Clear()
        {
            this.OnPropertyChanged("SystemCommand.ClearEditBox");
            ResultListView.Clear();
        }

        public void HideAndClear()
        {
            HideApp();
            Clear();
        }

        public void ShowApp()
        {
            this.OnPropertyChanged("SystemCommand.ShowApp");
        }

        public void Config()
        {
            this.OnPropertyChanged("SystemCommand.ShowConfig");
        }

        public void About()
        {
            this.OnPropertyChanged("SystemCommand.About");
        }

        public void PushResults(List<Result> results)
        {
            ResultListView.Clear();

            if (results.Count > 0)
            {
                if (!IsResultViewVisiable)
                {
                    IsResultViewVisiable = true;
                }

                ResultListView.AddRange(results);

                SelectedResultIndex = 0;
            }
            else
            {
                IsResultViewVisiable = false;
            }
        }
    }
}
