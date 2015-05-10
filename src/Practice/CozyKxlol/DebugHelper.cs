using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

public static class DebugHelper
{
    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern void OutputDebugString(string message);

    [ConditionalAttribute("DEBUG")]
    public static void Print(string message)
    {
        OutputDebugString(message);
    }

    [ConditionalAttribute("DEBUG")]
    public static void ShowDebugPrint()
    {
        String message = "CozyDebugOutput ShowDebugPrint";
        OutputDebugString(message);
    }

    [ConditionalAttribute("DEBUG")]
    public static void HideDebugPrint()
    {
        String message = "CozyDebugOutput HideDebugPrint";
        OutputDebugString(message);
    }
}
