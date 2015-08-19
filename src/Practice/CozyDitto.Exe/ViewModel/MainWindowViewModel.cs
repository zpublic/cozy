using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CozyDitto.Utils;
using System.Threading;
using CozyDitto.Exe.DataBase;
using CozyDitto.Exe.EventArg;

namespace CozyDitto.Exe.ViewModel
{
    public partial class MainWindowViewModel : BaseViewModel
    {
        public event EventHandler<ActivateEventArgs> ActivateEventHandler;

        private static Util.HotKeyCallback callback { get; set; }

        public MainWindowViewModel()
        {
            callback = new Util.HotKeyCallback(OnHotKey);

            ReadDBData();
            RegisterHotKey();
            StartMessageThread();
        }

        private void ReadDBData()
        {
            var data = ClipboardDB.Instance.GetAll();
            foreach(var obj in data)
            {
                ClipboardList.Add(obj.text);
            }
        }

        private void RegisterHotKey()
        {
            Util.RegisterHotKeyWithName("Visibility", Util.KeyModifiers.Ctrl, VirtualKey.VK_F1);
            Util.SetHotKeyCallback(callback);
        }

        private void StartMessageThread()
        {
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
                    if(ActivateEventHandler != null)
                    {
                        ActivateEventHandler(this, new ActivateEventArgs());
                    }

                    var clipdata = Util.GetClipboardText().Trim();
                    if (clipdata != null && clipdata.Length > 0 && !string.IsNullOrWhiteSpace(clipdata))
                    {
                        if (clipboardList.Count > 0 && clipdata == clipboardList.Last())
                        {
                            return true;
                        }

                        ClipboardList.Add(clipdata);
                        ClipboardDB.Instance.Create(new ClipboardRecord()
                        {
                            text = clipdata,
                            time = DateTime.Now,
                        });
                    }
                }
                return true;
            }
            return false;
        }
    }
}

