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
        private static OutputUtil.TextOutWCallback TextOutputCallback;

        private void Form1_Load(object sender, EventArgs e)
        {
            InitCallback();

            MouseUtil.SetMouseHook(MouseCallback);

            OutputUtil.InitHookEnv();
            OutputUtil.SetTextOutWHook(TextOutputCallback);
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

            TextOutputCallback = (hdc, x, y, lpString, c) =>
            {
                var str = Marshal.PtrToStringAuto(lpString);
                label2.Text = str;
                return false;
            };
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            MouseUtil.UnSetMouseHook();
            OutputUtil.UnsetAllHook();
        }
    }
}
