using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CozyAnywhere.Plugin.WinProcess.Model;

namespace CozyAnywhere.Plugin.WinProcess
{
    public static class ProcessUtil
    {
        // bool(CALLBACK*)(DWORD dwProcessId);
        public delegate bool ProcessEnumFunc(uint pid);

        // void(CALLBACK*)(LPTSTR lpProcName);
        public delegate void GetProcessNameFunc(IntPtr ptr);

        // bool ProcessEnum(PROCESSENUMPROC lpEnumFunc);
        [DllImport(@"ProcessUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern uint ProcessEnum(ProcessEnumFunc func);

        // bool ProcessTerminate(DWORD dwProcessId);
        [DllImport(@"ProcessUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern bool GetProcessName(uint Pid, GetProcessNameFunc func);

        // bool ProcessTerminateWithTimeOut(DWORD dwProcessId, DWORD dwTimeOut);
        [DllImport(@"ProcessUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern bool ProcessTerminate(uint Pid);

        // bool ProcessTerminateWithTimeOut(DWORD dwProcessId, DWORD dwTimeOut);
        [DllImport(@"ProcessUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern bool ProcessTerminateWithTimeOut(uint dwProcessId, uint dwTimeOut);

        // bool ProcessCreate(LPTSTR lpPath);
        [DllImport(@"ProcessUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern bool ProcessCreate(string lpPath);

        #region DefaultMethod

        public static string DefGetProcessName(uint Pid)
        {
            string name = null;
            GetProcessName(Pid, (x) => { name = Marshal.PtrToStringAuto(x); });
            return name;
        }

        public static List<WinProcessModel> DefProcessEnum()
        {
            var Result = new List<WinProcessModel>();
            ProcessEnum((x) =>
            {
                uint pid    = x;
                string name = DefGetProcessName(pid);

                var process = new WinProcessModel()
                {
                    ProcessId   = pid,
                    Name        = name,
                };
                if (name != null)
                {
                    Result.Add(process);
                }

                return false;
            });
            return Result;
        }
        #endregion
    }
}