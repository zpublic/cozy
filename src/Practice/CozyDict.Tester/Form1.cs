using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CozyDict.Core;
using System.Runtime.InteropServices;
using System.Threading;

namespace CozyDict.Tester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static MouseUtil.MouseHookCallback MouseCallback;
        private static OutputUtil.IPCCallback IPCCallback;

        private SynchronizationContext syncContext { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitCallback();
            OutputUtil.SetIPCCallback(IPCCallback);
            OutputUtil.SetCBTHook();
            OutputUtil.StartPipe();
            MouseUtil.SetMouseHook(MouseCallback);

            syncContext = SynchronizationContext.Current;
        }

        int x = 0;
        int y = 0;

        private void InitCallback()
        {
            MouseCallback = (code, wparam, lparam) =>
            {
                if ((int)wparam == MouseUtil.WM_MOUSEMOVE || (int)wparam == MouseUtil.WM_NCMOUSEMOVE)
                {
                    var mhs = (MouseUtil.MouseHookStruct)Marshal.PtrToStructure(lparam, typeof(MouseUtil.MouseHookStruct));

                    x = mhs.pt.x;
                    y = mhs.pt.y;
                    MouseUtil.InvalidateMouseWindow(x, y);

                    label1.Text = string.Format("x : {0} y : {1}", mhs.pt.x, mhs.pt.y);
                }
                return IntPtr.Zero;
            };

            IPCCallback = (lpString, dwPid) =>
            {

                if (dwPid != 0 && dwPid != OutputUtil.GetCurrentProcessId() && dwPid == OutputUtil.GetMouseWindowPid(x, y))
                {
                    string str = Marshal.PtrToStringAuto(lpString);
                    syncContext.Post((o) => 
                    {
                        this.label2.Text = o.ToString();
                    }, str);
                }
                return 0;
            };
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            MouseUtil.UnSetMouseHook();
            OutputUtil.StopPipe();
            OutputUtil.UnSetCBTHook();
        }
    }
}
