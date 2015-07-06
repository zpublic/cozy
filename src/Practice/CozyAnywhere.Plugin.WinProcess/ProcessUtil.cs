using System;
using System.Runtime.InteropServices;

namespace CozyAnywhere.Plugin.WinProcess
{
    public static class ProcessUtil
    {
        // bool(CALLBACK*)(DWORD dwProcessId);
        public delegate bool ProcessEnumFunc(uint pid);

        // void(CALLBACK*)(LPTSTR lpProcName);
        public delegate void GetProcessNameFunc(IntPtr ptr);

        // bool ProcessEnum(PROCESSENUMPROC lpEnumFunc);
        [DllImport(@"../../ProcessUtilCpp/Debug/ProcessUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern uint ProcessEnum(ProcessEnumFunc func);

        // bool ProcessTerminate(DWORD dwProcessId);
        [DllImport(@"../../ProcessUtilCpp/Debug/ProcessUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern bool GetProcessName(uint Pid, GetProcessNameFunc func);

        // bool ProcessTerminateWithTimeOut(DWORD dwProcessId, DWORD dwTimeOut);
        [DllImport(@"../../ProcessUtilCpp/Debug/ProcessUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern bool ProcessTerminate(uint Pid);

        // bool ProcessTerminateWithTimeOut(DWORD dwProcessId, DWORD dwTimeOut);
        [DllImport(@"../../ProcessUtilCpp/Debug/ProcessUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern bool ProcessTerminateWithTimeOut(uint dwProcessId, uint dwTimeOut);

        // bool ProcessCreate(LPTSTR lpPath);
        [DllImport(@"../../ProcessUtilCpp/Debug/ProcessUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern bool ProcessCreate(string lpPath);
    }
}