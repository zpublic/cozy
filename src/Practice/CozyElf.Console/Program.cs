using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyElf.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = @"g:\kingsoft\kassistant\kphone_3.1.0_rb\src\src_android\cmcm_infoc_tester\libs\armeabi\libcmcm_support.so";
            var elf = ELFSharp.ELF.ELFReader.Load(file);
            System.Console.WriteLine(file);
            System.Console.WriteLine(elf.Endianess);
            System.Console.WriteLine(elf.Machine);
        }
    }
}
