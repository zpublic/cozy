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
            var processList = ProcessUtil.DefProcessEnum();

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