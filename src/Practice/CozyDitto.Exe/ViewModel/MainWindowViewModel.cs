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

        private const string Enabled = "Enabled";
        private const string Disabled = "Disabled";
        private string visibility = Enabled;
        public string Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                Set(ref visibility, value, "Visibility");
            }
        }

        private ICommand lostFocusCommand;
        public ICommand LostFocusCommand
        {
            get
            {
                return lostFocusCommand = lostFocusCommand ?? new DelegateCommand((x)=> 
                {
                    Visibility = Disabled;
                });
            }
        }

        private static Util.HotKeyCallback callback { get; set; }

        public MainWindowViewModel()
        {
            callback = new Util.HotKeyCallback(OnHotKey);

            Util.RegisterHotKeyWithName("Visibility", Util.KeyModifiers.Ctrl, VirtualKey.VK_F1);
            Util.SetHotKeyCallback(callback);

            new Thread(new ThreadStart(() => { Util.EnterMessageLoop(); })).Start();
        }

        private bool OnHotKey(uint w, uint l)
        {
            if (w == Util.GetHotKeyIdWithName("Visibility"))
            {
                if (Visibility == Enabled)
                {
                    Visibility = Disabled;
                }
                else
                {
                    Visibility = Enabled;

                    var clipdata = Util.GetClipboardText();
                    if(clipdata != null && clipdata.Length > 0)
                    {
                        if(clipboardList.Count > 0 && clipdata == clipboardList.Last())
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
