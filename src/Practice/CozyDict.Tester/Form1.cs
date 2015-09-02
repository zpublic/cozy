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

namespace CozyDict.Tester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static MouseUtil.MouseHookCallback MouseCallback;

        private void Form1_Load(object sender, EventArgs e)
        {
            InitCallback();

            OutputUtil.StartPipe();
            MouseUtil.SetMouseHook(MouseCallback);
            OutputUtil.InitHookEnv();
            OutputUtil.SetTextOutWHook();
        }

        private void InitCallback()
        {
            MouseCallback = (code, wparam, lparam) =>
            {
                if ((int)wparam == MouseUtil.WM_MOUSEMOVE || (int)wparam == MouseUtil.WM_NCMOUSEMOVE)
                {
                    var mhs = (MouseUtil.MouseHookStruct)Marshal.PtrToStructure(lparam, typeof(MouseUtil.MouseHookStruct));
                    label1.Text = string.Format("x : {0} y : {1}", mhs.pt.x, mhs.pt.y);
                }
                return IntPtr.Zero;
            };
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            OutputUtil.UnsetAllHook();
            MouseUtil.UnSetMouseHook();
            OutputUtil.StopPipe();
        }
    }
}
