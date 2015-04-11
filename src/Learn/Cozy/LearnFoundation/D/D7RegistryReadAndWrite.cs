using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Cozy.LearnFoundation.D
{
    class D7RegistryReadAndWrite
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Registry_Sapmle();
            War3_Resolution_Change(1366,768);
        }

        public static void Registry_Sapmle()
        {
            // 打开配置单元
            RegistryKey hklm = Registry.CurrentUser;

            // 打开Software子键
            RegistryKey hkSoftWare = hklm.OpenSubKey("Software", true);

            // 创建或打开MineKey子键
            RegistryKey hkMine = hkSoftWare.CreateSubKey("MineKey");

            // 添加或修改数据
            hkMine.SetValue("string_value", "hello world");
            hkMine.SetValue("int_value", 42);

            // 删除数据
            hkMine.DeleteValue("string_value");
            hkMine.DeleteValue("int_value");

            // 删除键
            hkSoftWare.DeleteSubKey("MineKey");
        }

        public static void War3_Resolution_Change(int width, int height)
        {
            RegistryKey hklm = Registry.CurrentUser;
            RegistryKey hkSoftWare = hklm.OpenSubKey("Software");
            RegistryKey hkBlizzard_Entertainment = hkSoftWare.OpenSubKey("Blizzard Entertainment");
            RegistryKey hkWar3 = hkBlizzard_Entertainment.OpenSubKey("Warcraft III");
            RegistryKey hkVideo = hkWar3.OpenSubKey("Video", true);
            hkVideo.SetValue("reswidth", width);
            hkVideo.SetValue("resheight", height);
        }
    }
}
