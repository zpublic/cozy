using System;
using System.Runtime.InteropServices;

namespace CozyLauncher.Core.Plugin
{
    public class CppPluginInterop
    {
        [DllImport(@"CozyLauncher.CppPluginLoader.dll",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetPluginCount();

        [DllImport(@"CozyLauncher.CppPluginLoader.dll",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Init(int id);

        [DllImport(@"CozyLauncher.CppPluginLoader.dll",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Query(int id, IntPtr query);

        [DllImport(@"CozyLauncher.CppPluginLoader.dll",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void ShowPanel(int id, IntPtr command);

        [DllImport(@"CozyLauncher.CppPluginLoader.dll",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void RunCommand(int id, IntPtr command);
    }
}
