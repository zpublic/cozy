using CozyAnywhere.Plugin.WinProcess;
using CozyAnywhere.Plugin.WinProcess.Model;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ConsoleProcessTester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var processList = new List<WinProcess>();
            ProcessUtil.ProcessEnum((pid) =>
            {
                string name = null;
                ProcessUtil.GetProcessName(pid, (ptr) =>
                {
                    name = Marshal.PtrToStringAuto(ptr);
                });
                if (name != null)
                {
                    var process = new WinProcess()
                    {
                        ProcessId   = pid,
                        Name        = name,
                    };
                    processList.Add(process);
                }
                return false;
            });

            Console.WriteLine("---------------------------------Process:---------------------------------");
            foreach (var obj in processList)
            {
                Console.WriteLine("Pid: {0}\tName: {1}", obj.ProcessId, obj.Name);
            }
            Console.WriteLine("---------------------------------End:---------------------------------");
            Console.ReadKey();
        }
    }
}