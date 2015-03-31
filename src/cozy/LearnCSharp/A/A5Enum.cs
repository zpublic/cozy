using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.A
{
    // 定义枚举类型
    public enum TimeOfDay
    {
        Morning = 0,
        Afternoon = 1,
        Evening = 2,
    }

    class A5Enum
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            // 声明一个TimeOfDay变量并利用这个变量调用其他函数
            TimeOfDay time = TimeOfDay.Morning;
            useEnum(time);
            Convertion(time);

        }

        public static void useEnum(TimeOfDay time)
        {
            // 利用switch结构选择time的值
            switch(time)
            {
                case TimeOfDay.Morning:
                    Console.WriteLine("Good Morning");
                    break;
                case TimeOfDay.Afternoon:
                    Console.WriteLine("Good Afternoon");
                    break;
                case TimeOfDay.Evening:
                    Console.WriteLine("Good Evening");
                    break;
                default:
                    break;
            }
        }

        public static void Convertion(TimeOfDay time)
        {
            // 将TimeOfDay转换为string
            string time_to_str = time.ToString();
            Console.WriteLine(time_to_str);

            // 将string转换为TimeOfDay
            string str_to_time = "Evening";
            TimeOfDay time_type = (TimeOfDay)Enum.Parse(typeof(TimeOfDay), str_to_time, true);

        }
    }

}
