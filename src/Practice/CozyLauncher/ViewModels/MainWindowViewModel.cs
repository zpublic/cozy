using System;
using System.Collections.Generic;
using CozyLauncher.PluginBase;
using System.Windows.Input;
using CozyLauncher.Core.Plugin;
using CozyLauncher.Ext;
using CozyLauncher.Infrastructure.Hotkey;
using CozyLauncher.Infrastructure;
using System.Diagnostics;
using CozyLauncher.Infrastructure.MVVM;
using System.Windows.Controls;

namespace CozyLauncher.ViewModels
{
    public class MainWindowViewModel : BaseViewModel, IPublicApi
    {
        #region Property

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

        #endregion

        #region Commands

        private ICommand _QueryCommand;
        public ICommand QueryCommand
        {
            get
            {
                return _QueryCommand = _QueryCommand ?? new DelegateCommand(x =>
                {
                    var text = x as string;
                    if (!String.IsNullOrEmpty(text))
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
                    SelectedResultIndex--;
                    if (SelectedResultIndex < 0)
                        SelectedResultIndex = 0;
                });
            }
        }

        private ICommand _DownCommand;
        public ICommand DownCommand
        {
            get
            {
                return _DownCommand = _DownCommand ?? new DelegateCommand(x =>
                {
                    SelectedResultIndex++;
                    if (SelectedResultIndex > ResultListView.Count - 1)
                        SelectedResultIndex = ResultListView.Count - 1;
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

        #endregion

        public MainWindowViewModel()
        {
            pm.Init(this);

            GlobalHotkey.Instance.RegistHotkeyAction("HotKey.ShowApp", () =>
            {
                if (ShowCommand.CanExecute(null))
                {
                    ShowCommand.Execute(null);
                }
            });
        }

        #region IPublicApi Impl

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

        public void Update()
        {
            try
            {
                Process.Start(PathTransform.LocalFullPath(@"update/CozyLauncher.Tool.Update.exe"));
            }
            catch (Exception)
            {

            }
        }

        public void PushResults(List<Result> results)
        {
            ResultListView.Clear();

            if (results.Count > 0)
            {
                IsResultViewVisiable = true;

                ResultListView.AddRange(results);

                SelectedResultIndex = 0;
            }
            else
            {
                IsResultViewVisiable = false;
            }
        }

        public void ShowPanel(string command)
        {
            if (command == "config")
            {
                this.OnPropertyChanged("SystemCommand.ShowConfig");
            }
            else if (command == "about")
            {
                this.OnPropertyChanged("SystemCommand.About");
            }

            pm.ShowPanel(command);
        }

        public void RunCommand(string command)
        {
            if(command == "SystemCommand.SaveSetting")
            {
                this.OnPropertyChanged("SystemCommand.SaveSetting");
            }

            pm.RunCommand(command);
        }

        #endregion
    }
}
