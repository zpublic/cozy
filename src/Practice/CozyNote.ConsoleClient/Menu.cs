using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.ConsoleClient
{
    public class Menu
    {
        private List<MenuItem> MenuContent = new List<MenuItem>();

        public void Add(MenuItem item)
        {
            MenuContent.Add(item);
        }

        public MenuItem this[int index]
        {
            get
            {
                return MenuContent[index];
            }
        }

        public static void Print(Menu menu)
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("欢迎使用CozyNote，您可以输入以下指令:");
            Console.WriteLine();

            for (int i = 0; i < menu.MenuContent.Count; ++i)
            {
                Console.WriteLine("{0}. {1}", i, menu[i].Text);
            }

            Console.WriteLine();
            Console.WriteLine("-------------------------------------");
        }

        public void Input()
        {
            Console.WriteLine("请输入命令ID");
            int n = 0;
            if(int.TryParse(Console.ReadLine().Trim(), out n))
            {
                if (n >= 0 && n < MenuContent.Count)
                {
                    if(MenuContent[n].Command != null)
                    {
                        MenuContent[n].Command();
                    }
                }
                else
                {
                    Console.WriteLine("index超出范围");
                }
            }
            else
            {
                Console.WriteLine("输入错误");
            }
        }
    }
}
