using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using CozyDitto.Utils;
using System.Threading;
using System.Windows.Input;
using CozyDitto.Exe.Command;

namespace CozyDitto.Exe.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<string> clipboardList = new ObservableCollection<string>();
        public ObservableCollection<string> ClipboardList
        {
            get
            {
                return clipboardList;
            }
            set
            {
                Set(ref clipboardList, value, "ClipboardList");
            }
        }

        private Visibility windowVisibility = Visibility.Visible;
        public Visibility WindowVisibility
        {
            get
            {
                return windowVisibility;
            }
            set
            {
                Set(ref windowVisibility, value, "WindowVisibility");
            }
        }

        private string selectedClipboardText;
        public string SelectedClipboardText
        {
            get
            {
                return selectedClipboardText;
            }
            set
            {
                Set(ref selectedClipboardText, value, "SelectedClipboardText");
            }
        }

        private ICommand deactivateCommand;
        public ICommand DeactivateCommand
        {
            get
            {
                return deactivateCommand = deactivateCommand ?? new DelegateCommand((x) =>
                {
                    WindowVisibility = Visibility.Collapsed;
                });
            }
        }

        private ICommand copyCommand;
        public ICommand CopyCommand
        {
            get
            {
                return copyCommand = copyCommand ?? new DelegateCommand((x) =>
                {
                    if(SelectedClipboardText != null && SelectedClipboardText.Length > 0)
                    {
                        Util.SetClipboardText(SelectedClipboardText);

                        WindowVisibility = Visibility.Collapsed;
                    }
                });
            }
        }

        private static Util.HotKeyCallback callback { get; set; }

        public MainWindowViewModel()
        {
            callback = new Util.HotKeyCallback(OnHotKey);

            Util.RegisterHotKeyWithName("Visibility", Util.KeyModifiers.Ctrl, VirtualKey.VK_F1);
            Util.SetHotKeyCallback(callback);

            var MessageLoopThread = new Thread(new ThreadStart(() => { Util.EnterMessageLoop(); }));
            MessageLoopThread.IsBackground = true;

            MessageLoopThread.Start();
        }

        private bool OnHotKey(uint w, uint l)
        {
            if (w == Util.GetHotKeyIdWithName("Visibility"))
            {
                if (WindowVisibility == Visibility.Visible)
                {
                    WindowVisibility = Visibility.Collapsed;
                }
                else
                {
                    WindowVisibility = Visibility.Visible;

                    var clipdata = Util.GetClipboardText();
                    if (clipdata != null && clipdata.Length > 0)
                    {
                        if (clipboardList.Count > 0 && clipdata == clipboardList.Last())
                        {
                            return true;
                        }
                        ClipboardList.Add(clipdata);
                    }
                }
                return true;
            }
            return false;
        }
    }
}
